using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Connection string for the SQL Server database
        string connectionString = "Data Source=Amaze\\SQLEXPRESS;Initial Catalog=things_to_do;Integrated Security=True";

        // Encryption passphrase (should match the one used for encryption)
        string passphrase = "PassWordPhrase";

        // SQL query to decrypt and retrieve data
        string sqlQuery = $"SELECT passwd, CONVERT(NVARCHAR(MAX), DecryptByPassPhrase(@Passphrase, passwd)) AS DecryptedData FROM tbl_user;";

        // Create a SqlConnection and execute the query
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                // Add parameter to the query
                command.Parameters.AddWithValue("@Passphrase", passphrase);

                // Execute the query
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Check if there are rows returned
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Encrypted Data and Decrypted Result:");
                        while (reader.Read())
                        {
                            // Access columns using reader
                            string encryptedData = reader.GetString(0);
                            string decryptedData = reader.GetString(1);

                            Console.WriteLine($"Encrypted: {encryptedData}, Decrypted: {decryptedData}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }
            }
        }
    }
}

