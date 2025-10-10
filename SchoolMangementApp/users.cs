
using System;
using Microsoft.Data.SqlClient;

namespace collagemanagement
{
    internal class Users1
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Users1() { }

        public Users1(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        //  Register a new user
        public void RegisterUser(SqlConnection conn)
        {
            try
            {
                string checkQuery = "SELECT COUNT(*) FROM Users1 WHERE username = @username";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@username", Username);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    Console.WriteLine(" Username already exists! Please choose another one.");
                    return;
                }

                string query = "INSERT INTO Users1 (username, password, role) VALUES (@username, @password, @role)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@password", Password);
                cmd.Parameters.AddWithValue("@role", Role);

                int rows = cmd.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? " User registered successfully!" : "❌ Failed to register user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error during registration: " + ex.Message);
            }
        }

        //  Login and get role (used in Program.cs)
        public static int Login(SqlConnection conn, out string role)
        {
            role = "";
            Console.Write("\nEnter Username: ");
            string uname = Console.ReadLine();

            Console.Write("Enter Password: ");
            string pwd = Console.ReadLine();

            string query = "SELECT user_id, role FROM Users1 WHERE username = @u AND password = @p";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@u", uname);
            cmd.Parameters.AddWithValue("@p", pwd);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int userId = Convert.ToInt32(reader["user_id"]);
                role = reader["role"].ToString();
                reader.Close();
                return userId;
            }
            
            reader.Close();
            Console.WriteLine(" Invalid username or password!");
            return -1;
        }

       
    }
}






