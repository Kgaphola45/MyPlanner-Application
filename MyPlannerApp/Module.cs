using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace declaration for the MyPlannerApp application
namespace MyPlannerApp
{
    // Definition of the Module class
    public class Module
    {
        // Private field to store a list of Module objects
        private List<Module> moduleList = new List<Module>();

        // Private fields to store module information
        private string moduleCode;
        private string moduleName;
        private int credits;
        private int weekHours;
        private int semesterWeeks;
        private string startDate;
        private double selfStudyHours;

        // Default constructor for the Module class
        public Module()
        {
            // Initialize module information to default values
            moduleCode = "";
            moduleName = "";
            credits = 0;
            weekHours = 0;
            semesterWeeks = 0;
            startDate = "";
            selfStudyHours = 0;
        }

        // Parameterized constructor for the Module class
        public Module(string moduleCode, string moduleName, int credits, int weekHours, int semesterWeeks, string startDate, double selfStudyHours)
        {
            // Initialize module information with provided values
            this.moduleCode = moduleCode;
            this.moduleName = moduleName;
            this.credits = credits;
            this.weekHours = weekHours;
            this.semesterWeeks = semesterWeeks;
            this.startDate = startDate;
            this.selfStudyHours = selfStudyHours;
        }

        // Property to get or set the module code
        public string ModuleCode
        {
            get { return moduleCode; }
            set { moduleCode = value; }
        }

        // Property to get or set the module name
        public string ModuleName
        {
            get { return moduleName; }
            set { moduleName = value; }
        }

        // Property to get or set the credits associated with the module
        public int Credits
        {
            get { return credits; }
            set { credits = value; }
        }

        // Property to get or set the weekly hours dedicated to the module
        public int WeekHours
        {
            get { return weekHours; }
            set { weekHours = value; }
        }

        // Property to get or set the start date of the module
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        // Property to get or set the number of weeks in the semester
        public int SemesterWeeks
        {
            get { return semesterWeeks; }
            set { semesterWeeks = value; }
        }

        // Property to get or set the calculated self-study hours for the module
        public double SelfStudyHours
        {
            get { return selfStudyHours; }
            set { selfStudyHours = value; }
        }

        // Method to calculate and return the self-study hours for the module
        public double CalcSelfStudyHours()
        {
            double study;
            study = (((Credits * 10) / SemesterWeeks) - WeekHours);
            return study;
        }

        // Property to get or set the list of Module objects
        public List<Module> ListModules
        {
            get { return moduleList; }
            set { moduleList = value; }
        }

        // Method to add a new module entry to the list of modules
        public void AddModule()
        {
            // Retrieve module information from the current instance
            string code = ModuleCode;
            string name = ModuleName;
            int credits = Credits;
            int hours = WeekHours;
            int weeks = SemesterWeeks;
            string date = StartDate;
            double selfStudy = SelfStudyHours;

            // Create a new Module object with the retrieved values and add it to the list
            moduleList.Add(new Module(code, name, credits, hours, weeks, date, selfStudy));
        }
    }

    // Placeholder class (Class1) without any defined functionality
    public class Class1
    {
    }
}
