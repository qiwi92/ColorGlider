using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace GlowGlider.Server.Data
{
    public abstract class MySqlRepositoryBase : IDisposable
    {
        protected MySqlConnection Connection { get; }

        protected MySqlRepositoryBase()
        {

            var builder = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                AllowUserVariables = true,
                UserID = "azure",
                Password = "6#vWHD_$",
                Database = "localdb",
                Port = 52361,
                SslMode = MySqlSslMode.None,
            };
            
            var connectionString = builder.GetConnectionString(true);
            Connection = new MySqlConnection(connectionString);
        }

        protected void OpenConnectionIfNeeded()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }
        
        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}