
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using MyPlannerApp;  
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;


namespace MyPlanner.Pages
{
    // Definition of the PlannerModel class, which represents the code-behind for the Planner page
    public class PlannerModel : PageModel
    {
        // Instances of the Module and Hours classes
        Module moduleClass = new Module();
        Hours hoursClass = new Hours();

        // Lists to store information retrieved from the database
        public List<ModuleInfo> moduleInfos = new List<ModuleInfo>();
        public List<HoursInfo> hourInfos = new List<HoursInfo>();
        public List<PopulateDropList> dropListInfos = new List<PopulateDropList>();

        // This method is called when the page is initially requested.
        public void OnGet()
        {
            // Retrieve student number from the query parameters
            StudentNumber num = new StudentNumber();
            num.studentNumber = Request.Query["Parameter"];
            string studentNumber = num.studentNumber;

            // Fill data tables for modules, hours, and drop-down list
            FillModuleDataTables(studentNumber, moduleInfos);
            FillHoursDataTables(studentNumber, hourInfos);
            PopulateDropListBox(studentNumber, dropListInfos);
        }

        // Method to populate the drop-down list with module codes
        public void PopulateDropListBox(string student, List<PopulateDropList> dropListInfos)
        {
            // Database connection
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=LENOVO-IDEAPAD-;Initial Catalog=STUDY_PLANNER;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            // SQL query to retrieve module codes for the specified student
            string sql = $"SELECT module_code FROM dbo.module WHERE (stdNumber = '{student}')";

            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Populate the drop-down list with module codes
                    while (reader.Read())
                    {
                        PopulateDropList info = new PopulateDropList();
                        info.module_Code = reader.GetString(0);
                        dropListInfos.Add(info);
                    }
                }
            }

            // Close the database connection
            cnn.Close();
        }

        // Method to fill the hours data table with information from the database
        public void FillHoursDataTables(string student, List<HoursInfo> hourInfos)
        {
            // Database connection
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=LENOVO-IDEAPAD-;Initial Catalog=STUDY_PLANNER;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            // SQL query to retrieve hours information for the specified student
            string sql = $"SELECT * FROM dbo.hours WHERE (stdNumber = '{student}')";

            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Populate the hours data table with retrieved information
                    while (reader.Read())
                    {
                        HoursInfo info = new HoursInfo();
                        info.stdNumber = reader.GetString(0);
                        info.module_code = reader.GetString(1);
                        info.self_study = reader.GetInt32(2);
                        info.hours_remain = reader.GetInt32(3);
                        hourInfos.Add(info);
                    }
                }
            }

            // Close the database connection
            cnn.Close();
        }

        // Method to fill the modules data table with information from the database
        public static void FillModuleDataTables(string student, List<ModuleInfo> moduleInfos)
        {
            // Database connection
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=LENOVO-IDEAPAD-;Initial Catalog=STUDY_PLANNER;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            // SQL query to retrieve module information for the specified student
            string sql = $"SELECT * FROM dbo.module WHERE (stdNumber = '{student}')";

            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Populate the modules data table with retrieved information
                    while (reader.Read())
                    {
                        ModuleInfo info = new ModuleInfo();
                        info.stdNumber = reader.GetString(0);
                        info.module_code = reader.GetString(1);
                        info.module_name = reader.GetString(2);
                        info.credits = reader.GetInt32(3);
                        info.class_hours = reader.GetInt32(4);
                        info.semester_weeks = reader.GetInt32(5);
                        moduleInfos.Add(info);
                    }
                }
            }

            // Close the database connection
            cnn.Close();
        }

        // This method is called when the page receives a POST request.
        public void OnPost(string button)
        {
            // Retrieve student number from the query parameters
            StudentNumber num = new StudentNumber();
            num.studentNumber = Request.Query["Parameter"];
            string studentNumber = num.studentNumber;
            DateTime dt = new DateTime();

            // Database connection
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=LENOVO-IDEAPAD-;Initial Catalog=STUDY_PLANNER;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            // Button click handling for module submission or hours submission
            if (button == "moduleSubmit")
            {
                // Retrieving form values for module submission
                string code = Request.Form["moduleCode"];
                string moduleName = Request.Form["moduleName"];
                int credits = Convert.ToInt32(Request.Form["moduleCredits"]);
                int hours = Convert.ToInt32(Request.Form["classHours"]);
                int weeks = Convert.ToInt32(Request.Form["semesterWeeks"]);
                string startDate;
                double selfStudy = 0;

                // Convert the start date to the required format
                dt = Convert.ToDateTime(Request.Form["startDate"]);
                startDate = dt.ToString("dd/mm/yyyy");

                // Set module class properties and add the module
                moduleClass.ModuleCode = code;
                moduleClass.ModuleName = moduleName;
                moduleClass.Credits = credits;
                moduleClass.WeekHours = hours;
                moduleClass.SemesterWeeks = weeks;
                moduleClass.StartDate = startDate;
                moduleClass.AddModule();

                // Calculate self-study hours and add module and hours to the database
                selfStudy = moduleClass.CalcSelfStudyHours();
                AddModule(cnn, studentNumber, code, moduleName, credits, hours, weeks, startDate);
                AddHours(cnn, selfStudy, studentNumber, code);
            }
            else if (button == "hoursSubmit")
            {
                // Retrieving form values for hours submission
                int hoursWorked = Convert.ToInt32(Request.Form["hoursWorked"]);
                string moduleWorked = Request.Form["module_code"];
                dt = Convert.ToDateTime(Request.Form["workDate"]);
                string workDate = dt.ToString("dd/mm/yyyy");

                // Add hours to the hours class and submit to the database
                hoursClass.HoursWorked = hoursWorked;
                hoursClass.Date = workDate;
                hoursClass.AddDate();
                SubmitHours(cnn, hoursWorked, moduleWorked, workDate, studentNumber);
            }

            // Close the database connection
            cnn.Close();
        }

        // Method to submit hours to the database
        public void SubmitHours(SqlConnection cnn, int hoursWorked, string moduleWorked, string workDate, string studentNumber)
        {
            // Retrieve self-study hours from the database
            int selfStudy = 0;
            SqlCommand command2;
            SqlDataReader dataReader;
            string sql2, output = "";
            sql2 = $"SELECT self_study FROM dbo.hours WHERE (stdNumber = '{studentNumber}') AND module_code = '{moduleWorked}'";
            command2 = new SqlCommand(sql2, cnn);
            dataReader = command2.ExecuteReader();
            while (dataReader.Read())
            {
                output = output + dataReader.GetInt32(0);
            }
            selfStudy = Convert.ToInt32(output);
            dataReader.Close();
            command2.Dispose();

            // Retrieve remaining hours from the database
            int oldRemain = 0;
            SqlCommand remainCmd;
            SqlDataReader remainData;
            string remainSql, remainOutput = "";
            remainSql = $"SELECT hours_remain FROM dbo.hours WHERE (stdNumber = '{studentNumber}') AND (module_code = '{moduleWorked}')";
            remainCmd = new SqlCommand(remainSql, cnn);
            remainData = remainCmd.ExecuteReader();
            while (remainData.Read())
            {
                remainOutput = remainOutput + remainData.GetInt32(0);
            }
            oldRemain = Convert.ToInt32(remainOutput);
            remainData.Close();
            remainCmd.Dispose();

            // Update self-study and remaining hours in the database
            int newHours = selfStudy + hoursWorked;
            int newRemaining = oldRemain - hoursWorked;
            SqlCommand updateCmd;
            SqlDataAdapter updateAdapter = new SqlDataAdapter();
            string updateSql = "";
            updateSql = $"UPDATE dbo.hours SET self_study = {newHours}, hours_remain = {newRemaining} WHERE stdNumber = '{studentNumber}' AND module_code = '{moduleWorked}'";
            updateCmd = new SqlCommand(updateSql, cnn);
            updateAdapter.UpdateCommand = new SqlCommand(updateSql, cnn);
            updateAdapter.UpdateCommand.ExecuteNonQuery();
            updateCmd.Dispose();

            // Refresh data tables after updating
            FillHoursDataTables(studentNumber, hourInfos);
            FillModuleDataTables(studentNumber, moduleInfos);
            PopulateDropListBox(studentNumber, dropListInfos);
        }

        // Method to add hours to the database
        public void AddHours(SqlConnection cnn, double selfStudy, string studentNumber, string code)
        {
            SqlCommand command2;
            SqlDataAdapter dataAdapter2 = new SqlDataAdapter();
            string sql2 = "";
            int remainingHours = Convert.ToInt32(selfStudy);
            sql2 = $"INSERT INTO dbo.hours VALUES ('{studentNumber}','{code}',{0},{remainingHours})";
            command2 = new SqlCommand(sql2, cnn);
            dataAdapter2.InsertCommand = new SqlCommand(sql2, cnn);
            dataAdapter2.InsertCommand.ExecuteNonQuery();
            command2.Dispose();

            // Refresh data tables after updating
            FillHoursDataTables(studentNumber, hourInfos);
            FillModuleDataTables(studentNumber, moduleInfos);
            PopulateDropListBox(studentNumber, dropListInfos);
        }

        // Method to add a module to the database
        public void AddModule(SqlConnection cnn, string studentNumber, string code, string moduleName, int credits, int hours, int weeks, string startDate)
        {
            SqlCommand command;
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            string sql = "";
            sql = $"INSERT INTO dbo.module VALUES ('{studentNumber}','{code}','{moduleName}',{credits},{hours},{weeks})";
            command = new SqlCommand(sql, cnn);
            dataAdapter.InsertCommand = new SqlCommand(sql, cnn);
            dataAdapter.InsertCommand.ExecuteNonQuery();
            command.Dispose();
        }

        // Definition of the StudentNumber class
        public class StudentNumber
        {
            public string studentNumber { get; set; }
        }

        // Definition of the ModuleInfo class to represent module information
        public class ModuleInfo
        {
            public string stdNumber { get; set; }
            public string module_code { get; set; }
            public string module_name { get; set; }
            public int credits { get; set; }
            public int class_hours { get; set; }
            public int semester_weeks { get; set; }
        }

        // Definition of the HoursInfo class to represent hours information
        public class HoursInfo
        {
            public string stdNumber { get; set; }
            public string module_code { get; set; }
            public int self_study { get; set; }
            public int hours_remain { get; set; }
        }

        // Definition of the PopulateDropList class to represent drop-down list information
        public class PopulateDropList
        {
            public string module_Code { get; set; }
        }
    }
}
