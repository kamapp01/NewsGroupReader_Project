using MVVM_ICOM_INOTIFY.Entities; // Antag at ConnectionCredentials er defineret her
using System;
using System.Diagnostics;
using MVVM_ICOM_INOTIFY.InterfaceAdapter;

namespace MVVM_ICOM_INOTIFY.Application
{
    public class GetConnection_US1
    {
        private IGetConnection _connection;
        private ConnectionCredentials _credentials;

        public GetConnection_US1(IGetConnection connection, ConnectionCredentials connectionCredentials)
        {
            this._connection = connection;
            this._credentials = connectionCredentials;
        }
        
        
        
    }
}