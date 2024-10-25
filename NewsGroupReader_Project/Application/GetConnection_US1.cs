using MVVM_ICOM_INOTIFY.Entities; // Antag at ConnectionCredentials er defineret her
using System;
using System.Diagnostics;
using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Application
{
    public class GetConnection_US1
    {
        private IGetConnection _connection;
        
        public GetConnection_US1(IGetConnection connection)
        {
            this._connection = connection;
        }
        
        
        public bool Connect(ConnectionCredentials credentials)
        {
            bool connected = _connection.Connect(credentials);

            if (connected)
            {
                return true;
            }

            return false;
        }


        public bool Authenticate(ConnectionCredentials credentials)
        {
            bool authenticated = _connection.AuthenticateUser(credentials);

            if (authenticated)
            {
                return true;
            }

            return false;
        }

        

    }
}