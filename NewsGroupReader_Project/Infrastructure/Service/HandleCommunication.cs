using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace MVVM_ICOM_INOTIFY.Infrastructure.Service
{
    public class HandleCommunication
    {
        
        public void SendToServer(string command)
        {
            byte[] dataFromUser = Encoding.ASCII.GetBytes(command);
            EstablishConnection.Instance.NetworkStream.Write(dataFromUser, 0, dataFromUser.Length);

            Debug.WriteLine($"User sent: {command}");

        }


        public string ReadFromServer()
        {

            // saves data from server
            byte[] dataFromServer = new byte[4096];
            int bytesRead = EstablishConnection.Instance.NetworkStream.Read(dataFromServer, 0, (dataFromServer.Length));

            return Encoding.ASCII.GetString(dataFromServer, 0, bytesRead);
        }
    }
    
}
