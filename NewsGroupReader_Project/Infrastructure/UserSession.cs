namespace MVVM_ICOM_INOTIFY.Infrastructure;

public static class UserSession
{
    // properties to save information between views (no user memory load)
    public static string UserName { get; set; }
    public static string Server  { get; set; }
    public static string Port { get; set; }
    
}