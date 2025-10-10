



//using System;
//using Microsoft.Data.SqlClient;

//namespace collagemanagement
//{
//    internal class Student1
//    {
//        public int StudentId { get; set; }
//        public int UserId { get; set; }
//        public string StudentName { get; set; }
//        public string StudentAddress { get; set; }
//        public bool Attendance { get; set; }

//        public Student1() { }

//        public Student1(string name, string address, bool attendance, int userId)
//        {
//            StudentName = name;
//            StudentAddress = address;
//            Attendance = attendance;
//            UserId = userId;
//        }

//        // Create
//        public void InsertStudent(SqlConnection conn)
//        {
//            string query = "INSERT INTO Student1 (student_name, student_address, attendance, user_id) " +
//                           "VALUES (@student_name, @student_address, @attendance, @user_id)";
//            SqlCommand cmd = new SqlCommand(query, conn);
//            cmd.Parameters.AddWithValue("@student_name", StudentName);
//            cmd.Parameters.AddWithValue("@student_address", StudentAddress);
//            cmd.Parameters.AddWithValue("@attendance", Attendance);
//            cmd.Parameters.AddWithValue("@user_id", UserId);

//            int rows = cmd.ExecuteNonQuery();
//            Console.WriteLine(rows > 0 ? "✅ Student record inserted successfully!" : "❌ Failed to insert student record.");
//        }

//        // Read
//        public static void DisplayStudents(SqlConnection conn)
//        {
//            string query = "SELECT s.student_id, s.student_name, s.student_address, s.attendance, u.username " +
//                           "FROM Student1 s JOIN Users1 u ON s.user_id = u.user_id";
//            SqlCommand cmd = new SqlCommand(query, conn);

//            SqlDataReader reader = cmd.ExecuteReader();

//            Console.WriteLine("\n--- Student Records ---");
//            while (reader.Read())
//            {
//                Console.WriteLine($"ID: {reader["student_id"]}");
//                Console.WriteLine($"Name: {reader["student_name"]}");
//                Console.WriteLine($"Address: {reader["student_address"]}");
//                Console.WriteLine($"Attendance: {(Convert.ToBoolean(reader["attendance"]) ? "Present" : "Absent")}");
//                Console.WriteLine($"Added By User: {reader["username"]}");
//                Console.WriteLine("----------------------");
//            }
//            reader.Close();
//        }

//        // Update
//        public static void UpdateStudent(SqlConnection conn, int studentId, string name, string address, bool attendance)
//        {
//            string query = "UPDATE Student1 SET student_name=@name, student_address=@address, attendance=@attendance " +
//                           "WHERE student_id=@id";
//            SqlCommand cmd = new SqlCommand(query, conn);
//            cmd.Parameters.AddWithValue("@name", name);
//            cmd.Parameters.AddWithValue("@address", address);
//            cmd.Parameters.AddWithValue("@attendance", attendance);
//            cmd.Parameters.AddWithValue("@id", studentId);

//            int rows = cmd.ExecuteNonQuery();
//            Console.WriteLine(rows > 0 ? "✅ Student updated successfully!" : "❌ Student not found.");
//        }

//        // Delete
//        public static void DeleteStudent(SqlConnection conn, int studentId)
//        {
//            string query = "DELETE FROM Student1 WHERE student_id=@id";
//            SqlCommand cmd = new SqlCommand(query, conn);
//            cmd.Parameters.AddWithValue("@id", studentId);

//            int rows = cmd.ExecuteNonQuery();
//            Console.WriteLine(rows > 0 ? "✅ Student deleted successfully!" : "❌ Student not found.");
//        }

//        // Optional: Get Student by ID
//        public static Student1 GetStudentById(SqlConnection conn, int studentId)
//        {
//            string query = "SELECT * FROM Student1 WHERE student_id=@id";
//            SqlCommand cmd = new SqlCommand(query, conn);
//            cmd.Parameters.AddWithValue("@id", studentId);

//            SqlDataReader reader = cmd.ExecuteReader();
//            if (reader.Read())
//            {
//                Student1 student = new Student1
//                {
//                    StudentId = studentId,
//                    StudentName = reader["student_name"].ToString(),
//                    StudentAddress = reader["student_address"].ToString(),
//                    Attendance = Convert.ToBoolean(reader["attendance"]),
//                    UserId = Convert.ToInt32(reader["user_id"])
//                };
//                reader.Close();
//                return student;
//            }

//            reader.Close();
//            return null;
//        }
//    }
//}
using System;
using Microsoft.Data.SqlClient;

namespace collagemanagement
{
    internal class Student1
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public bool Attendance { get; set; }

        public Student1() { }

        public Student1(string name, string address, bool attendance, int userId)
        {
            StudentName = name;
            StudentAddress = address;
            Attendance = attendance;
            UserId = userId;
        }

        public void InsertStudent(SqlConnection conn)
        {
            string query = "INSERT INTO Student1 (student_name, student_address, attendance, user_id) " +
                           "VALUES (@name, @address, @attendance, @user_id)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", StudentName);
            cmd.Parameters.AddWithValue("@address", StudentAddress);
            cmd.Parameters.AddWithValue("@attendance", Attendance);
            cmd.Parameters.AddWithValue("@user_id", UserId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "✅ Student inserted!" : "❌ Failed.");
        }

        public static void DisplayStudents(SqlConnection conn)
        {
            string query = "SELECT s.student_id, s.student_name, s.student_address, s.attendance, u.username " +
                           "FROM Student1 s JOIN Users1 u ON s.user_id = u.user_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Student Records ---");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["student_id"]}");
                Console.WriteLine($"Name: {reader["student_name"]}");
                Console.WriteLine($"Address: {reader["student_address"]}");
                Console.WriteLine($"Attendance: {(Convert.ToBoolean(reader["attendance"]) ? "Present" : "Absent")}");
                Console.WriteLine($"Added By User: {reader["username"]}");
                Console.WriteLine("----------------------");
            }
            reader.Close();
        }

        public static void UpdateStudent(SqlConnection conn, int studentId)
        {
            Console.Write("Enter new name: ");
            string name = Console.ReadLine();
            Console.Write("Enter new address: ");
            string address = Console.ReadLine();
            Console.Write("Is present? (true/false): ");
            bool attendance = bool.Parse(Console.ReadLine());

            string query = "UPDATE Student1 SET student_name=@name, student_address=@address, attendance=@attendance " +
                           "WHERE student_id=@id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@attendance", attendance);
            cmd.Parameters.AddWithValue("@id", studentId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "✅ Student updated!" : "❌ Failed.");
        }

        public static void DeleteStudent(SqlConnection conn, int studentId)
        {
            string query = "DELETE FROM Student1 WHERE student_id=@id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", studentId);
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "✅ Student deleted!" : "❌ Failed.");
        }
    }
}
