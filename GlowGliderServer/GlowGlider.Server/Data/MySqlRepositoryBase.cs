using System;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace GlowGlider.Server.Data
{
    public abstract class MySqlRepositoryBase : IDisposable
    {
        private static readonly ApplicationException DbUnavailableException = 
            new ApplicationException("Database unavailable.");

        protected MySqlConnection Connection { get; }

        protected MySqlRepositoryBase()
        {
            var port = GetPortFromEnvVar();
            
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                AllowUserVariables = true,
                UserID = "azure",
                Password = "6#vWHD_$",
                Database = "localdb",
                Port = port,
                SslMode = MySqlSslMode.None,
            };
            
            var connectionString = builder.GetConnectionString(true);
            Connection = new MySqlConnection(connectionString);
        }

        private static uint GetPortFromEnvVar()
        {
            var connStr = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb");
            if (connStr == null) throw DbUnavailableException;

            var fragments = connStr.Split(';', StringSplitOptions.RemoveEmptyEntries);
            var dataFrag = fragments.FirstOrDefault(frag => frag.StartsWith("Data Source="));
            if (dataFrag == null) throw DbUnavailableException;

            var portStr = dataFrag.Split(':', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            if (portStr == null) throw DbUnavailableException;

            if (!uint.TryParse(portStr, out var port)) throw DbUnavailableException;
            return port;
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