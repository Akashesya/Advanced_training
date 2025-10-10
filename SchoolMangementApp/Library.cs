using System;
using Microsoft.Data.SqlClient;

namespace collagemanagement
{
    internal class Library
    {
        public int LibraryId { get; set; }
        public string BookTitle { get; set; }
        public int BorrowerId { get; set; }
        public string BorrowerRole { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }

        public Library(string bookTitle, int borrowerId, string borrowerRole)
        {
            BookTitle = bookTitle;
            BorrowerId = borrowerId;
            BorrowerRole = borrowerRole;

            BorrowDate = DateTime.UtcNow;
            DueDate = borrowerRole.ToLower() == "staff"
                ? BorrowDate.AddDays(14)
                : BorrowDate.AddDays(7);
        }

        // Borrow a new book
        public void BorrowBook(SqlConnection conn)
        {
            //  Check how many books already borrowed (not yet returned)
            string checkQuery = "SELECT COUNT(*) FROM Library WHERE BorrowerId = @id AND ReturnDate IS NULL";
            SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@id", BorrowerId);

            object result = checkCmd.ExecuteScalar();
            int count = (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;

            int limit = (BorrowerRole.ToLower() == "staff") ? 5 : 3;
            int days = (BorrowerRole.ToLower() == "staff") ? 14 : 7;

            if (count >= limit)
            {
                Console.WriteLine($"\n⚠️ You have already borrowed {count} books. Limit is {limit}.");
                return;
            }

            DateTime borrowDate = DateTime.UtcNow;
            DateTime dueDate = borrowDate.AddDays(days);

            string query = @"INSERT INTO Library 
                            (BookTitle, BorrowerId, BorrowerRole, BorrowDate, DueDate)
                             VALUES (@title, @id, @role, @borrowDate, @dueDate)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@title", BookTitle);
            cmd.Parameters.AddWithValue("@id", BorrowerId);
            cmd.Parameters.AddWithValue("@role", BorrowerRole);
            cmd.Parameters.AddWithValue("@borrowDate", borrowDate);
            cmd.Parameters.AddWithValue("@dueDate", dueDate);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0
                ? $"\n✅ '{BookTitle}' borrowed successfully! Due by {dueDate.ToShortDateString()}."
                : "\n❌ Failed to borrow book.");
        }

        // Return a borrowed book
        public static void ReturnBook(SqlConnection conn, int libraryId)
        {
            string query = "UPDATE Library SET ReturnDate = @returnDate WHERE LibraryId = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@returnDate", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@id", libraryId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? " Book returned successfully!" : " Failed to return book.");
        }

        // Display all borrowed books
        public static void DisplayBorrowedBooks(SqlConnection conn)
        {
            string query = "SELECT * FROM Library";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Borrowed Books ---");
            while (reader.Read())
            {
                Console.WriteLine($"Library ID: {reader["LibraryId"]}");
                Console.WriteLine($"Book Title: {reader["BookTitle"]}");
                Console.WriteLine($"Borrower ID: {reader["BorrowerId"]}");
                Console.WriteLine($"Role: {reader["BorrowerRole"]}");
                Console.WriteLine($"Borrow Date: {reader["BorrowDate"]}");
                Console.WriteLine($"Due Date: {reader["DueDate"]}");
                Console.WriteLine($"Returned: {(reader["ReturnDate"] == DBNull.Value ? "No" : reader["ReturnDate"])}");
                Console.WriteLine("--------------------------");
            }
            reader.Close();
        }
    }
}
