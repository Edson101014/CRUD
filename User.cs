using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CRUD.Form1;
using System.Data.SqlClient;
using Dapper;

namespace CRUD
{
    internal class User
    {
        public int recid { get; set; }
        public string Name { get; set; }
        public decimal Age { get; set; }
        public DateTime Birth { get; set; }



        internal static List<User> UserShow()
        {
            var sql = "SELECT * FROM dbo.UserTest ORDER BY recid DESC";

            using (var connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                connection.Open();
                return connection.Query<User>(sql).AsList();
            }




        }

        internal static bool AddUser(User user)
        {
            var sql = "INSERT INTO dbo.UserTest (name, age, birth) " +
                      "VALUES (@Name, @Age, @Birth)";

            using (var connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                connection.Open();
                var rowsAffected = connection.Execute(sql, new
                {
                    Name = user.Name,
                    Age = user.Age,
                    Birth = user.Birth,
                });
                return rowsAffected > 0;
            }
        }

        internal static bool EditUser(User user)
        {
            var sql = "UPDATE dbo.UserTest " +
                      "SET name = @Name, age = @Age, birth = @Birth " +
                      "WHERE recid = @recid";

            using (var connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                connection.Open();
                var rowsAffected = connection.Execute(sql, new
                {
                    Name = user.Name,
                    Age = user.Age,
                    Birth = user.Birth,
                    recid = user.recid
                });
                return rowsAffected > 0;
            }
        }

    }
}
