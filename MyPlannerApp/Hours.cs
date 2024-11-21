
namespace MyPlannerApp
{
    // Definition of the Hours class
    public class Hours
    {
        // Private field to store a list of Hours objects
        private List<Hours> hoursList = new List<Hours>();

        // Private fields to store hours worked and date
        private int hoursWorked;
        private string date;

        // Default constructor for the Hours class
        public Hours()
        {
            // Initialize hoursWorked to 0 and date to an empty string
            hoursWorked = 0;
            date = "";
        }

        // Parameterized constructor for the Hours class
        public Hours(int hoursWorked, string date)
        {
            // Initialize hoursWorked and date with provided values
            this.hoursWorked = hoursWorked;
            this.date = date;
        }

        // Property to get or set the hours worked
        public int HoursWorked
        {
            get { return hoursWorked; }
            set { hoursWorked = value; }
        }

        // Property to get or set the date
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        // Property to get or set the list of Hours objects
        public List<Hours> ListHours
        {
            get { return hoursList; }
            set { hoursList = value; }
        }

        // Method to add a new entry to the list of hours with a specified date
        public void AddDate()
        {
            // Retrieve hours and date from the current instance
            int hours = HoursWorked;
            string date1 = Date;

            // Create a new Hours object with the retrieved values and add it to the list
            hoursList.Add(new Hours(hours, date1));
        }
    }
}
