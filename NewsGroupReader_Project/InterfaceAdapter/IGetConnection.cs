using MVVM_ICOM_INOTIFY.Entities;

namespace MVVM_ICOM_INOTIFY.InterfaceAdapter;

public interface IGetConnection
{
    
    bool Connect(ConnectionCredentials credentials);
    bool AuthenticateUser(ConnectionCredentials credentials);
    
}