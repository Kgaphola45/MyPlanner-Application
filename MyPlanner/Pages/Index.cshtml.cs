// Importing necessary namespaces for Azure, ASP.NET, and SQL Server
using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

// Namespace declaration for the MyPlanner.Pages namespace
namespace MyPlanner.Pages
{
    // Definition of the IndexModel class, which represents the code-behind for the Index page
    public class IndexModel : PageModel
    {
        // Private field to store the logger instance
        private readonly ILogger<IndexModel> _logger;

        // Property to store and retrieve messages to be displayed on the page
        public string message { get; set; }

        // Constructor for the IndexModel class
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // This method is called when the page is initially requested.
        public void OnGet()
        {
            // Initialize the message property to an empty string
            message = "";
        }

        // This method is called when the page receives a POST request.
        public void OnPost(string button)
        {
            // Retrieve values from the form
            string stdNumber = Request.Form["stdNumber"];
            string password = Request.Form["password"];
            string hash = HashPassword(password);

            // Check which button was clicked (Register or Login)
            if (button == "register")
            {
                // Registration logic
                string connectionString;
                SqlConnection cnn;

                // Connection string for SQL Server
                connectionString = @"Data Source=LENOVO-IDEAPAD-;Initial Catalog=STUDY_PLANNER;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                SqlCommand command;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                string sql = "";

                // SQL query to insert a new student into the database
                sql = $"INSERT INTO dbo.student VALUES ('{stdNumber}','{hash}')";

                command = new SqlCommand(sql, cnn);

                dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
                dataAdapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();

                // Set a message for the user
                message = "Registration completed successfully. Kindly re-enter your details to proceed with the login.";

            }
            else if (button == "login")
            {
                // Login logic
                string connectionString;
                SqlConnection cnn;

                // Connection string for SQL Server
                connectionString = @"Data Source=LENOVO-IDEAPAD-;Initial Catalog=STUDY_PLANNER;Integrated Security=True";
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                SqlCommand command;
                SqlDataReader dataReader;
                string sql, output = "";

                // SQL query to retrieve the stored password for the entered student number
                sql = $"SELECT password FROM dbo.student WHERE stdNumber = '{stdNumber}'";

                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    output = output + dataReader.GetString(0);
                }

                // Compare the hashed password from the database with the entered password
                if (output.Equals(hash))
                {
                    // Successful login
                    message = "Logged In";
                    Response.Redirect($"Planner?Parameter={stdNumber}");
                }
                else
                {
                    // Failed login attempt
                    message = "Apologies, but the login attempt has failed. Please verify your credentials and try again";
                }

                dataReader.Close();
                command.Dispose();
                cnn.Close();
            }
        }

        // This method hashes the provided password using SHA256
        public static string HashPassword(string password)
        {
            // Create an instance of SHA256 hashing algorithm
            var sha = SHA256.Create();

            // Convert the password to a byte array
            var asByteArray = Encoding.Default.GetBytes(password);

            // Compute the hash of the password
            var hashPassword = sha.ComputeHash(asByteArray);

            // Convert the hashed password to a base64 string
            return Convert.ToBase64String(hashPassword);
        }
    }
}
