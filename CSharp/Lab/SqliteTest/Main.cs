using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Lab.SqliteTest
{
    public class Main
    {
        private static readonly string _dbPath = "sqlite_test.db";
        private static SQLiteConnection connection;

        public static void Run()
        {
            //TypeTest.Run();

            Init();

            //Insert(0, 100);

            //多线程并行插入数据
            //Task.WaitAll(Task.Run(() => Insert(0, 3000)), Task.Run(() => Insert(3000, 3000)));

            //Task.WaitAll(Task.Run(ThrowEx), Task.Run(ThrowEx), Task.Run(ThrowEx), Task.Run(ThrowEx));

            Console.WriteLine("End");
        }

        private static void Init()
        {
            File.Delete(_dbPath);

            var connectionBuilder = new SQLiteConnectionStringBuilder
            {
                DataSource = _dbPath,
                Password = "123456",
                Version = 3
            };
            connection = new SQLiteConnection(connectionBuilder.ToString());
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE [test] (
                [id] INT,
                [str] TEXT
            )";
            command.ExecuteNonQuery();
            command.Dispose();
        }

        private static void Insert(int startId, int count)
        {
            var transaction = connection.BeginTransaction();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO [test] VALUES (@id,@str)";
            command.Transaction = transaction;
            for (var i = startId; i < startId + count; i++)
            {
                command.Parameters.Add("@id", DbType.Int32).Value = i;
                command.Parameters.Add("@str", DbType.String).Value = i.ToString();
                command.ExecuteNonQuery();
            }
            transaction.Rollback();
            command.Dispose();
        }

        /// <summary>
        /// 抛出事务异常
        /// </summary>
        private static void ThrowEx()
        {
            var transaction1 = connection.BeginTransaction();
            var transaction2 = connection.BeginTransaction();
            var command1 = connection.CreateCommand();
            var command2 = connection.CreateCommand();
            command1.CommandText = "INSERT INTO [test] VALUES (@id,@str)";
            command2.CommandText = "INSERT INTO [test] VALUES (@id,@str)";
            command1.Transaction = transaction1;
            command2.Transaction = transaction2;

            for (var i = 0; i < 100; i++)
            {
                if (i % 3 == 0)
                {
                    command1.Parameters.Add("@id", DbType.Int32).Value = i;
                    command1.Parameters.Add("@str", DbType.String).Value = $"A:{i}";
                    command1.ExecuteNonQuery();
                }
                else
                {
                    command2.Parameters.Add("@id", DbType.Int32).Value = i;
                    command2.Parameters.Add("@str", DbType.String).Value = $"B:{i}";
                    command2.ExecuteNonQuery();
                }
            }

            //transaction1.Commit();
            transaction1.Dispose();
            command1.Dispose();

            //transaction2.Commit();
            transaction2.Dispose();
            command2.Dispose();
        }
    }
}
