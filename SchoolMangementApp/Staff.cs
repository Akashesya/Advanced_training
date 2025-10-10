
using System;
using Microsoft.Data.SqlClient;

namespace collagemanagement
{
    internal class Staff1
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }
        public int UserId { get; set; }

        public Staff1() { }

        public Staff1(string name, string designation, decimal salary, string department, int userId)
        {
            StaffName = name;
            Designation = designation;
            Salary = salary;
            Department = department;
            UserId = userId;
        }

        // Create
        public void InsertStaff(SqlConnection conn)
        {
            string query = "INSERT INTO Staff1 (staff_name, designation, salary, department, user_id) " +
                           "VALUES (@staff_name, @designation, @salary, @department, @user_id)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@staff_name", StaffName);
            cmd.Parameters.AddWithValue("@designation", Designation);
            cmd.Parameters.AddWithValue("@salary", Salary);
            cmd.Parameters.AddWithValue("@department", Department);
            cmd.Parameters.AddWithValue("@user_id", UserId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? " Staff record inserted successfully!" : " Failed to insert staff record.");
        }

        // Read
        public static void DisplayAllStaff(SqlConnection conn)
        {
            string query = "SELECT s.staff_id, s.staff_name, s.designation, s.salary, s.department, u.username " +
                           "FROM Staff1 s JOIN Users1 u ON s.user_id = u.user_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Staff Records ---");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["staff_id"]}");
                Console.WriteLine($"Name: {reader["staff_name"]}");
                Console.WriteLine($"Designation: {reader["designation"]}");
                Console.WriteLine($"Salary: {reader["salary"]}");
                Console.WriteLine($"Department: {reader["department"]}");
                Console.WriteLine($"Added By User: {reader["username"]}");
                Console.WriteLine("----------------------");
            }
            reader.Close();
        }

        // Update
        public static void UpdateStaff(SqlConnection conn, int staffId, string name, string designation, decimal salary, string department)
        {
            string query = "UPDATE Staff1 SET staff_name=@name, designation=@designation, salary=@salary, department=@department WHERE staff_id=@id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@designation", designation);
            cmd.Parameters.AddWithValue("@salary", salary);
            cmd.Parameters.AddWithValue("@department", department);
            cmd.Parameters.AddWithValue("@id", staffId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? " Staff updated successfully!" : " Staff not found.");
        }

        // Delete
        public static void DeleteStaff(SqlConnection conn, int staffId)
        {
            string query = "DELETE FROM Staff1 WHERE staff_id=@id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", staffId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? " Staff deleted successfully!" : " Staff not found.");
        }

        // Optional: Get Staff by ID
        public static Staff1 GetStaffById(SqlConnection conn, int staffId)
        {
            string query = "SELECT * FROM Staff1 WHERE staff_id=@id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", staffId);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Staff1 staff = new Staff1
                {
                    StaffId = staffId,
                    StaffName = reader["staff_name"].ToString(),
                    Designation = reader["designation"].ToString(),
                    Salary = Convert.ToDecimal(reader["salary"]),
                    Department = reader["department"].ToString(),
                    UserId = Convert.ToInt32(reader["user_id"])
                };
                reader.Close();
                return staff;
            }

            reader.Close();
            return null;
        }
    }
}
