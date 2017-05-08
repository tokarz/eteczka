using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eteczka.DB.Connection
{
    public class ConnectionDetails : IConnectionDetails
    {
        private string User;
        private string Password;
        private string Host;
        private string Port;
        private string Name;

        public ConnectionDetails(string user, string password, string host, string port, string name)
        {
            this.User = user;
            this.Password = password;
            this.Host = host;
            this.Port = port;
            this.Name = name;
        }

        public string getUser()
        {
            return this.User;
        }

        public string getPassword()
        {
            return this.Password;
        }

        public string getHost()
        {
            return this.Host;
        }

        public string getPort()
        {
            return this.Port;
        }

        public string getDbName()
        {
            return this.Name;
        }

    }
}
