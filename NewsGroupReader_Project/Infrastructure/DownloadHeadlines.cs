using System.Diagnostics;
using MVVM_ICOM_INOTIFY.Infrastructure.Service;
using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Infrastructure;

public class DownloadHeadlines : IDownloadHeadlines
{
    private readonly HandleCommunication _handleCommunication = new HandleCommunication();

    private const string CarriageReturnLineFeed = "\r\n";
    

    public List<string> DownloadNewsHeadlines(string groupName)
    {
        List<string> listOfHeadlines = new List<string>();
            
        try
        {
            Debug.WriteLine("Downloading headlines");
            _handleCommunication.SendToServer($"GROUP {groupName}{CarriageReturnLineFeed}");
            string groupResponse = _handleCommunication.ReadFromServer();
            Debug.WriteLine($"Server (group response): {groupResponse}");

            if (groupResponse.StartsWith("211"))
            {
                _handleCommunication.SendToServer($"XOVER{CarriageReturnLineFeed}");
                string headlineResponse = _handleCommunication.ReadFromServer();
                Debug.WriteLine($"Server (headline response): {headlineResponse}");

                if (headlineResponse.StartsWith("224"))
                {
                    string[] responseParts = headlineResponse.Split(CarriageReturnLineFeed);

                    for (int i = 1; i < responseParts.Length; i++) // int i = 1 to skip string with server response code 215
                    {
                        listOfHeadlines.Add(responseParts[i]);
                    }
                }
                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"[DownlaodHeadlines] - {e}");
        }
            
        return listOfHeadlines;
    }
}
    
    
    
