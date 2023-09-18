using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolsConnectionString"].ToString();
                IDbConnection dbConnection = new SqlConnection(connectionString);

                VeriCek(dbConnection);

                VeriEkle(dbConnection);

                VeriGuncelle(dbConnection);

                VeriSil(dbConnection);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void VeriSil(IDbConnection dbConnection)
        {
            dbConnection.Execute("DELETE FROM Student WHERE Id = @Id", new { Id = 2 });
        }

        private static void VeriGuncelle(IDbConnection dbConnection)
        {
            dbConnection.Execute("UPDATE Student SET Age = @Age WHERE Name = @Name", new { Age = 33, Name = "Kudret" });
        }

        private static void VeriEkle(IDbConnection dbConnection)
        {
            var newStudent = new Student { Name = "Kudret", SurName="Aslan", Age = 32, InsertedDate=DateTime.Now };
            dbConnection.Execute("INSERT INTO Student (Name,Surname,Age,InsertedDate) VALUES (@Name,@Surname,@Age,@InsertedDate)", newStudent);
        }

        private static void VeriCek(IDbConnection dbConnection)
        {
            var students = dbConnection.Query<Student>("SELECT * FROM Student");
        }

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string SurName { get; set; }
            public int Age { get; set; }
            public DateTime? InsertedDate { get; set; }
        }
    }
}