using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace Lab.SqliteTest
{
    public class TypeTest
    {
        private readonly static string _dbPath = "sqlite_type_test.db";
        private static SQLiteConnection connection;

        public static void Run()
        {
            File.Delete(_dbPath);

            connection = new SQLiteConnection(string.Concat("Data Source =", _dbPath));
            connection.Open();

            {
                var command = connection.CreateCommand();
                command.CommandText = @"CREATE TABLE [test] (
                    [id] INTEGER,
                    [d] REAL,
                    [str] TEXT
                )";
                command.ExecuteNonQuery();
                command.Dispose();
            }

            {
                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO [test] VALUES(@id, @d ,@str)";
                command.Parameters.Add("@id", DbType.Object).Value = 111111111;
                command.Parameters.Add("@d", DbType.Object).Value = 56.3333;
                command.Parameters.Add("@str", DbType.Object).Value = BitConverter.GetBytes(2060578.0243988037);
                command.ExecuteNonQuery();
                command.Dispose();
            }

            {
                var dataSet = new DataSet();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM [test]";
                var adapter = new SQLiteDataAdapter(command);
                adapter.Fill(dataSet);
                adapter.Dispose();
                command.Dispose();

                var id = dataSet.Tables[0].Rows[0][0];
                var d = dataSet.Tables[0].Rows[0][1];
                var str = dataSet.Tables[0].Rows[0][2];
            }

        }
    }
}
