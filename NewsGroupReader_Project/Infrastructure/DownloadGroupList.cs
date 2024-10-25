using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using MVVM_ICOM_INOTIFY.Infrastructure.Service;
using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Infrastructure
{
    public class DownloadGroupList : IDownloadList
    {

        private readonly HandleCommunication _handleCommunication = new HandleCommunication();

        private const string CarriageReturnLineFeed = "\r\n";
        private const char Delimiter = ' ';

        
        public List<string> DownloadList()
        {
            List<string> listOfGroups = new List<string>();
            
            try
            {
                _handleCommunication.SendToServer($"LIST{CarriageReturnLineFeed}");
                string response = _handleCommunication.ReadFromServer();
                Debug.WriteLine($"Server: {response}");
                
                // splits the server response at each carriage return and line feed sequence
                string[] responseParts = response.Split(CarriageReturnLineFeed);

                // extracts and saves only the group names from the server response
                string[] onlyNewsgroupName = TrimGroupNames(responseParts);
                
                // adds every (only) group name to listOfGroups
                for (int i = 1; i < 40; i++)                    // int i = 1 to skip string with server response code 215
                {
                    listOfGroups.Add(onlyNewsgroupName[i]);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"[DownlaodList] - {e}");
            }
            
            return listOfGroups;
        }

        private string[] TrimGroupNames(string[] responseParts)
        {
            int cnt = 0;
            string[] groupNames = new string[responseParts.Length];
            
            foreach (string responseLine in responseParts)
            {
                string[] temp = responseLine.Split(Delimiter);
                groupNames[cnt] = temp[0];
                
                cnt++;
            }
            
            return groupNames;
        }
    }
}




        
    
    
