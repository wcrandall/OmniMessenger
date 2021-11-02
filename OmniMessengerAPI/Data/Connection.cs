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

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
            sqliteConnection.Open();

            return command.ExecuteNonQuery();
        }

        public static T ExecuteScalar<T>(string commandText, int timeout = 30)
        {

            SqliteConnection sqliteConnection = new SqliteConnection(connectionString);
            SqliteCommand command = sqliteConnection.CreateCommand();
            command.CommandText = commandText;
            command.CommandTimeout = timeout;
            sqliteConnection.Open();

            return Connection.ExecuteScalar<T>(commandText, timeout);
        }

        public static Dictionary<int, object[]> ExecuteReader(string commandText)
        {
            SqliteConnection sqliteConnection = new SqliteConnection(connectionString);
            SqliteCommand command = sqliteConnection.CreateCommand();
            command.CommandText = commandText;
            sqliteConnection.Open();

            Dictionary<int, object[]> objectStore = new Dictionary<int, object[]>();

            using (DbDataReader reader = command.ExecuteReader())
            {
                object[] objects = new object[reader.FieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objects);
                    if (int.TryParse(objects[0].ToString(), out int id))
                    {
                        objectStore.Add(id, objects);
                    };
                }

                return objectStore;
            }
        }
    }
}