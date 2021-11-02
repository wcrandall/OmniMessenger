using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace OmniMessengerAPI.Data
{
    public static class Connection
    {
        private static readonly string connectionString = "Data Source=contact.db";

        public static int ExecuteNonQuery(string commandText,
                                          Dictionary<string, object> parameters = null,
                                          int timeout = 30)
        {

            SqliteConnection sqliteConnection = new SqliteConnection(connectionString);
            SqliteCommand command = sqliteConnection.CreateCommand();
            command.CommandTimeout = timeout;
            command.CommandText = commandText;
            
            if(parameters != null){
                foreach(KeyValuePair<string, object> parameter in parameters){
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }

            return command.ExecuteNonQuery();
        }

        public static T ExecuteScalar<T>(string commandText, int timeout = 30)
        {

            SqliteConnection sqliteConnection = new SqliteConnection(connectionString);
            SqliteCommand command = sqliteConnection.CreateCommand();
            command.CommandText = commandText;
            command.CommandTimeout = timeout;

            return Connection.ExecuteScalar<T>(commandText, timeout);
        }

        public static DbDataReader ExecuteReader(string commandText)
        {
            SqliteConnection sqliteConnection = new SqliteConnection(connectionString);
            SqliteCommand command = sqliteConnection.CreateCommand();
            command.CommandText = commandText;
            return command.ExecuteReader();
        }
    }
}