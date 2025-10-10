


//using System;
//using Microsoft.Data.SqlClient;
//using collagemanagement;

//namespace DB_demo
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            string connString = @"Data Source=DESKTOP-4PKPHIJ\SQLEXPRESS;Initial Catalog=COLLAGEMANAGEMENT;Trusted_Connection=True;TrustServerCertificate=True;";
//            using SqlConnection conn = new SqlConnection(connString);

//            try
//            {
//                conn.Open();
//                Console.WriteLine(" Connection Successful!\n");

//                while (true)
//                {
//                    Console.WriteLine("===== Main Menu =====");
//                    Console.WriteLine("1. Register User");
//                    Console.WriteLine("2. Login");
//                    Console.WriteLine("3. Exit");
//                    Console.Write("Enter choice: ");
//                    int mainChoice = int.Parse(Console.ReadLine());

//                    if (mainChoice == 1)
//                    {
//                        // Register
//                        Console.WriteLine("\n--- Register New User ---");
//                        Console.Write("Enter username: ");
//                        string uname = Console.ReadLine();

//                        Console.Write("Enter password: ");
//                        string pwd = Console.ReadLine();

//                        Console.Write("Enter role (student/staff): ");
//                        string role = Console.ReadLine();

//                        Users1 user = new Users1(uname, pwd, role);
//                        user.RegisterUser(conn);
//                    }
//                    else if (mainChoice == 2)
//                    {
//                        // Login
//                        int userId = Users1.Login(conn, out string role);

//                        if (userId != -1)
//                        {
//                            Console.WriteLine($"\n Login Successful! Role: {role}\n");

//                            if (role.ToLower() == "student")
//                                StudentMenu(conn, userId);
//                            else if (role.ToLower() == "staff")
//                                StaffMenu(conn, userId);
//                            else
//                                Console.WriteLine("Unknown role.");
//                        }
//                    }
//                    else if (mainChoice == 3)
//                    {
//                        Console.WriteLine("Exiting...");
//                        break;
//                    }
//                    else
//                    {
//                        Console.WriteLine("Invalid choice!");
//                    }

//                    Console.WriteLine();
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("❌ Error: " + ex.Message);
//            }
//        }

//        // Menu for Student operations
//        static void StudentMenu(SqlConnection conn, int userId)
//        {
//            while (true)
//            {
//                Console.WriteLine("\n--- Student Operations ---");
//                Console.WriteLine("1. Insert Student");
//                Console.WriteLine("2. Display All Students");
//                Console.WriteLine("3. Update Student");
//                Console.WriteLine("4. Delete Student");
//                Console.WriteLine("5. Logout");
//                Console.Write("Enter choice: ");
//                int choice = int.Parse(Console.ReadLine());

//                switch (choice)
//                {
//                    case 1:
//                        Console.Write("Enter Student Name: ");
//                        string name = Console.ReadLine();
//                        Console.Write("Enter Student Address: ");
//                        string address = Console.ReadLine();
//                        Console.Write("Is Present? (true/false): ");
//                        bool attendance = bool.Parse(Console.ReadLine());

//                        Student1 student = new Student1(name, address, attendance, userId);
//                        student.InsertStudent(conn);
//                        break;

//                    case 2:
//                        Student1.DisplayStudents(conn);
//                        break;

//                    case 3:
//                        Console.Write("Enter Student ID to update: ");
//                        int updateId = int.Parse(Console.ReadLine());
//                        Console.Write("Enter New Name: ");
//                        string newName = Console.ReadLine();
//                        Console.Write("Enter New Address: ");
//                        string newAddress = Console.ReadLine();
//                        Console.Write("Is Present? (true/false): ");
//                        bool newAttendance = bool.Parse(Console.ReadLine());

//                        Student1.UpdateStudent(conn, updateId, newName, newAddress, newAttendance);
//                        break;

//                    case 4:
//                        Console.Write("Enter Student ID to delete: ");
//                        int deleteId = int.Parse(Console.ReadLine());
//                        Student1.DeleteStudent(conn, deleteId);
//                        break;

//                    case 5:
//                        return; // Logout

//                    default:
//                        Console.WriteLine("❌ Invalid choice!");
//                        break;
//                }
//            }
//        }

//        // Menu for Staff operations
//        static void StaffMenu(SqlConnection conn, int userId)
//        {
//            while (true)
//            {
//                Console.WriteLine("\n--- Staff Operations ---");
//                Console.WriteLine("1. Insert Staff");
//                Console.WriteLine("2. Display All Staff");
//                Console.WriteLine("3. Update Staff");
//                Console.WriteLine("4. Delete Staff");
//                Console.WriteLine("5. Logout");
//                Console.Write("Enter choice: ");
//                int choice = int.Parse(Console.ReadLine());

//                switch (choice)
//                {
//                    case 1:
//                        Console.Write("Enter Staff Name: ");
//                        string name = Console.ReadLine();
//                        Console.Write("Enter Designation: ");
//                        string designation = Console.ReadLine();
//                        Console.Write("Enter Salary: ");
//                        decimal salary = decimal.Parse(Console.ReadLine());
//                        Console.Write("Enter Department: ");
//                        string department = Console.ReadLine();

//                        Staff1 staff = new Staff1(name, designation, salary, department, userId);
//                        staff.InsertStaff(conn);
//                        break;

//                    case 2:
//                        Staff1.DisplayAllStaff(conn);
//                        break;

//                    case 3:
//                        Console.Write("Enter Staff ID to update: ");
//                        int updateId = int.Parse(Console.ReadLine());
//                        Console.Write("Enter New Name: ");
//                        string newName = Console.ReadLine();
//                        Console.Write("Enter New Designation: ");
//                        string newDesignation = Console.ReadLine();
//                        Console.Write("Enter New Salary: ");
//                        decimal newSalary = decimal.Parse(Console.ReadLine());
//                        Console.Write("Enter New Department: ");
//                        string newDepartment = Console.ReadLine();

//                        Staff1.UpdateStaff(conn, updateId, newName, newDesignation, newSalary, newDepartment);
//                        break;

//                    case 4:
//                        Console.Write("Enter Staff ID to delete: ");
//                        int deleteId = int.Parse(Console.ReadLine());
//                        Staff1.DeleteStaff(conn, deleteId);
//                        break;

//                    case 5:
//                        return; // Logout

//                    default:
//                        Console.WriteLine("❌ Invalid choice!");
//                        break;
//                }
//            }
//        }
//    }
//}


using System;
using Microsoft.Data.SqlClient;

namespace collagemanagement
{
    internal class Program
    {

        static void Main(string[] args)
        {

            string connString = @"Data Source=DESKTOP-4PKPHIJ\SQLEXPRESS;Initial Catalog=COLLAGEMANAGEMENT;Trusted_Connection=True;TrustServerCertificate=True;";
            using SqlConnection conn = new SqlConnection(connString);
            try {
                conn.Open();
                Console.WriteLine("=== Welcome to College Management System ===\n");

                while (true)
                {
                    Console.WriteLine("\n1. Register");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Exit");
                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        Register(conn);
                    }
                    else if (choice == "2")
                    {
                        int userId = Users1.Login(conn, out string role);
                        if (userId != -1)
                        {
                            Console.WriteLine($"\n✅ Logged in successfully as {role}!");
                            if (role.ToLower() == "staff")
                                StaffMenu(conn, userId, role);
                            else if (role.ToLower() == "student")
                                StudentMenu(conn, userId, role);
                            else
                                Console.WriteLine("❌ Invalid role detected.");
                        }
                    }
                    else if (choice == "3")
                    {
                        Console.WriteLine("Exiting... Goodbye!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("❌ Invalid choice. Try again.");
                    }
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine("❌ Error: " + ex.Message);
            }
        }
        




            // ---------------- REGISTER ----------------
            static void Register(SqlConnection conn)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter role (staff/student): ");
            string role = Console.ReadLine();

            Users1 user = new Users1(username, password, role);
            user.RegisterUser(conn);
        }

        // ---------------- STAFF MENU ----------------
        static void StaffMenu(SqlConnection conn, int userId, string role)
        {
            while (true)
            {
                Console.WriteLine("\n--- STAFF MENU ---");
                Console.WriteLine("1. Add Staff");
                Console.WriteLine("2. View All Staff");
                Console.WriteLine("3. Update Staff");
                Console.WriteLine("4. Delete Staff");
                Console.WriteLine("5. Library Menu");
                Console.WriteLine("6. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter designation: ");
                    string designation = Console.ReadLine();
                    Console.Write("Enter salary: ");
                    decimal salary = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Enter department: ");
                    string department = Console.ReadLine();

                    Staff1 staff = new Staff1(name, designation, salary, department, userId);
                    staff.InsertStaff(conn);
                }
                else if (choice == "2")
                {
                    Staff1.DisplayAllStaff(conn);
                }
                else if (choice == "3")
                {
                    Console.Write("Enter Staff ID to update: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter new name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter new designation: ");
                    string designation = Console.ReadLine();
                    Console.Write("Enter new salary: ");
                    decimal salary = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Enter new department: ");
                    string department = Console.ReadLine();
                    Staff1.UpdateStaff(conn, id, name, designation, salary, department);
                }
                else if (choice == "4")
                {
                    Console.Write("Enter Staff ID to delete: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Staff1.DeleteStaff(conn, id);
                }
                else if (choice == "5")
                {
                    LibraryMenu(conn, userId, role);
                }
                else if (choice == "6")
                {
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Invalid choice.");
                }
            }
        }

        // ---------------- STUDENT MENU ----------------
        static void StudentMenu(SqlConnection conn, int userId, string role)
        {
            while (true)
            {
                Console.WriteLine("\n--- STUDENT MENU ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Library Menu");
                Console.WriteLine("6. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter address: ");
                    string address = Console.ReadLine();
                    Console.Write("Is present? (true/false): ");
                    bool attendance = Convert.ToBoolean(Console.ReadLine());

                    Student1 student = new Student1(name, address, attendance, userId);
                    student.InsertStudent(conn);
                }
                else if (choice == "2")
                {
                    Student1.DisplayStudents(conn);
                }
                else if (choice == "3")
                {
                    Console.Write("Enter Student ID to update: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Student1.UpdateStudent(conn, id);
                }
                else if (choice == "4")
                {
                    Console.Write("Enter Student ID to delete: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Student1.DeleteStudent(conn, id);
                }
                else if (choice == "5")
                {
                    LibraryMenu(conn, userId, role);
                }
                else if (choice == "6")
                {
                    Console.WriteLine("Logging out...");
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Invalid choice.");
                }
            }
        }

        // ---------------- LIBRARY MENU ----------------
        static void LibraryMenu(SqlConnection conn, int userId, string role)
        {
            while (true)
            {
                Console.WriteLine("\n--- LIBRARY MENU ---");
                Console.WriteLine("1. Borrow Book");
                Console.WriteLine("2. Return Book");
                Console.WriteLine("3. View Borrowed Books");
                Console.WriteLine("4. Go Back");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter Book Title: ");
                    string title = Console.ReadLine();

                    Library lib = new Library(title, userId, role);
                    lib.BorrowBook(conn);
                }
                else if (choice == "2")
                {
                    Console.Write("Enter Library ID to return: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Library.ReturnBook(conn, id);
                }
                else if (choice == "3")
                {
                    Library.DisplayBorrowedBooks(conn);
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Invalid choice.");
                }
            }
        }
    }
}

