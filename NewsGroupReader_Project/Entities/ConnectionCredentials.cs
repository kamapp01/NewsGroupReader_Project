namespace MVVM_ICOM_INOTIFY.Entities;

public class ConnectionCredentials
{
    public string ServerInput { get; set; }
    public string PortInput { get; set; }
    public string UserInput { get; set; }
    public string PasswordInput { get; set; }

    public ConnectionCredentials(string serverInput, string portInput, string userInput, string passwordInput)
    {
        this.ServerInput = serverInput;
        this.PortInput = portInput;
        this.UserInput = userInput;
        this.PasswordInput = passwordInput;
    }


    public override string ToString()
    {
        return $"Server: {ServerInput}, Port no: {PortInput}, Username: {UserInput}, Password: {PasswordInput}";
    }
}