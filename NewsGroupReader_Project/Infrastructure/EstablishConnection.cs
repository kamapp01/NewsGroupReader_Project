using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using MVVM_ICOM_INOTIFY.Entities;
using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Infrastructure;

public class EstablishConnection : IGetConnection
{
    private const string CarriageReturnLineFeed = "\r\n";
    
    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private bool _connected = false;
    private bool _authenticated = false;
    
    public bool Connect(ConnectionCredentials credentials)
    {
        try
        {
            _tcpClient = new TcpClient(credentials.ServerInput, Convert.ToInt32(credentials.PortInput));
            _networkStream = _tcpClient.GetStream();
            Debug.WriteLine("Connecting to server...");

            string response = ReadFromServer();
            Debug.WriteLine($"Server: {response}");

            if (response.StartsWith("200"))
            {
                _connected = true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        Debug.WriteLine("Connection established");
        return true;
    }


    public bool AuthenticateUser(ConnectionCredentials credentials)
    {
        try
        {
            SendToServer($"AUTHINFO USER {credentials.UserInput}{CarriageReturnLineFeed}");
            string response = ReadFromServer();
            Debug.WriteLine($"Server: {response}");
            
            if (response.StartsWith("381"))
            {
                SendToServer($"AUTHINFO PASS {credentials.PasswordInput}{CarriageReturnLineFeed}");
                response = ReadFromServer();
                Debug.WriteLine($"Server: {response}");
                
                
                if (response.StartsWith("281"))
                {
                    Debug.WriteLine("Authentication successful!");
                    _authenticated = true;
                }
                else
                {
                    Debug.WriteLine("Authentication failed.");
                }
                
            }
            else
            {
                Debug.WriteLine("Failed to proceed with authentication: Unexpected response.");
            }
            
            return _authenticated;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    
    private void SendToServer(string message)
    {
        byte[] dataFromUser = Encoding.ASCII.GetBytes(message);
        _networkStream.Write(dataFromUser, 0, dataFromUser.Length);
        
        Debug.WriteLine($"User sent: {message}");
    }


    private string ReadFromServer()
    {
        // saves data 
        byte[] dataFromServer = new byte[256];
        int bytesRead = _networkStream.Read(dataFromServer, 0, dataFromServer.Length);
        return Encoding.ASCII.GetString(dataFromServer, 0, bytesRead);
    }
    
    
    
}