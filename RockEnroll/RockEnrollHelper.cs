using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockEnroll
{
    public class RockEnrollHelper
    {

        public Student student = new Student("John", "Doe", 30045232);

        public enum Terms
        {
           NONE, FALL2021, WINTER2022, SPRING2022, SUMMER2022
        }

        public static List<Course> fall21Courses = new List<Course>();
        public static List<Course> winter22Courses = new List<Course>();
        public static List<Course> spring22Courses = new List<Course>();
        public static List<Course> summer22Courses = new List<Course>();

        // all courses list here; create courses first before adding them, to modify prereqs
        public static List<Course> allCourses = new List<Course>();
        public static List<Course> pickedCourses = new List<Course>();

        public static void InitializeCourses()
        {
            //Lecture Timeblocks 
            TimeBlock tbLec1_1 = new TimeBlock(21, "09:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbLec1_2 = new TimeBlock(21, "13:00", new TimeSpan(0, 0, 50, 0, 0));

            TimeBlock tbLec2_1 = new TimeBlock(21, "11:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbLec2_2 = new TimeBlock(10, "13:00", new TimeSpan(0, 1, 15, 0, 0));

            TimeBlock tbLec3_1 = new TimeBlock(21, "15:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbLec3_2 = new TimeBlock(10, "9:00", new TimeSpan(0, 1, 15, 0, 0));

            //Tutorial Timeblocks
            TimeBlock tbTut1_1 = new TimeBlock(5, "10:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbTut1_2 = new TimeBlock(10, "11:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbTut1_3 = new TimeBlock(5, "12:00", new TimeSpan(0, 0, 50, 0, 0));

            TimeBlock tbTut2_1 = new TimeBlock(5, "14:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbTut2_2 = new TimeBlock(10, "12:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbTut2_3 = new TimeBlock(10, "15:00", new TimeSpan(0, 0, 50, 0, 0));

            //Lab Timeblocks
            TimeBlock tbLab1 = new TimeBlock(1, "16:00", new TimeSpan(0, 3, 0, 0, 0));
            TimeBlock tbLab2 = new TimeBlock(2, "16:00", new TimeSpan(0, 3, 0, 0, 0));
            TimeBlock tbLab3 = new TimeBlock(4, "16:00", new TimeSpan(0, 3, 0, 0, 0));
            TimeBlock tbLab4 = new TimeBlock(8, "16:00", new TimeSpan(0, 3, 0, 0, 0));

            // add courses here
            Course soci201 = new Course(201, "Introductory Sociology", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, "Sociology as a discipline examines how the society in which we live influences our thinking and behaviour. An introduction to sociology through the study of society, social institutions, group behaviour and social change.");
            soci201.lecturesList.Add(new Lecture(soci201, tbLec1_1, "John Smith", Campus.UniversityOfCalgary,"Social Science Rm 109", 50, 200, 0, 30, ""));
            soci201.lecturesList.Add(new Lecture(soci201, tbLec1_2, "John Smith", Campus.UniversityOfCalgary, "Social Science Rm 109", 120, 200, 0, 30, ""));
            soci201.tutorialsList.Add(new Tutorial(soci201, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0,0,""));
            soci201.tutorialsList.Add(new Tutorial(soci201, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci201.tutorialsList.Add(new Tutorial(soci201, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            allCourses.Add(soci201);

            Course soci321 = new Course(321, "Sociology of Health and Illness", Faculty.Arts, Department.SOCI,  Course.departmentConsent.OR, null);
            soci321.lecturesList.Add(new Lecture(soci321,tbLec1_1, "Jane Doe", Campus.UniversityOfCalgary, "Social Science Rm 18", 50, 200, 0, 30, ""));
            soci321.lecturesList.Add(new Lecture(soci321,tbLec1_2, "Jane Doe", Campus.UniversityOfCalgary, "Social Science Rm 18", 120, 200, 0, 30, ""));
            soci321.tutorialsList.Add(new Tutorial(soci321,tbTut2_1, "", Campus.UniversityOfCalgary, "Social Science Rm 06", 5, 30, 0, 0, ""));
            soci321.tutorialsList.Add(new Tutorial(soci321,tbTut2_2, "", Campus.UniversityOfCalgary, "Social Science Rm 06", 5, 30, 0, 0, ""));
            soci321.tutorialsList.Add(new Tutorial(soci321,tbTut2_3, "", Campus.UniversityOfCalgary, "Social Science Rm 06", 5, 30, 0, 0, ""));
            soci321.prerequisites.Add(soci201);
            soci201.successors.Add(soci321);
            allCourses.Add(soci321);

            // mock up call -Kylie
            AddToPickedCourses(soci201);
            AddToPickedCourses(soci321);
        }

        public static void AddToPickedCourses(Course course)
        {
            // Expecting this will be called when a course is picked from the course list/adding - so when going to enrollment checkout we can get from the instance pickedCourses
            // Code below is mock up like initCourses ^
            
            pickedCourses.Add(course);
        }

    }

    public class Student
    {
        public string firstName { get; }
        public string lastName { get; }
        public int studentID { get; }
        /// <summary>
        /// List of courses the student has taken.
        /// </summary>
        public List<Course> coursesTaken { get; }
        /// <summary>
        /// Courses the student is currently taking.
        /// </summary>
        public List<ClassInstance> currentSchedule { get; }

        public Student(string firstName, string lastName, int studentID)
        {

            this.firstName = firstName;
            this.lastName = lastName;
            this.studentID = studentID;
            this.coursesTaken = new List<Course>();
            this.currentSchedule = new List<ClassInstance>();

        }

    }

    public enum Faculty
    {
        NONE, Arts, Medicine, Architecture, GraduateStudies, Business, Kinesiology, Law, Nursing, Engineering, Science, SocialWork, Veterinary, Education
    }


    public enum Department
    {
        NONE,
        [Description("Art")] ART,
        [Description("Biology")] BIOL,
        [Description("Chemistry")] CHEM,
        [Description("Computer Science")] CPSC,
        [Description("Economics")] ECON,
        [Description("Engineering")] ENGG,
        [Description("English")] ENGL,
        [Description("Linguistics")] LING,
        [Description("Mathematics")] MATH,
        [Description("Music")] MUSI,
        [Description("Philosophy")] PHIL,
        [Description("Physics")] PHYS,
        [Description("Psychology")] PSYC,
        [Description("Sociology")] SOCI,
        [Description("Software Engineering")] SENG,
        [Description("Statistics")] STAT
    }

    
    public class Course
    {

        // use OR for "or consent of department"; AND for "and consent of department"
        public enum departmentConsent
        {
            NONE, OR, AND
        }

        public int courseID { get; set; }
        public string courseTitle { get; set; }
        public string courseDescription { get; set; }

        public Faculty faculty { get; set; }

        public Department department { get; set; }

        //add units

        /// <summary>
        /// Course prerequisites. Use different lists for alternative requirements.
        /// </summary>
        public List<Course> prerequisites { get; }
        public List<Course> antirequisites { get; }

        //Courses that require the current one as a prerequisite
        public List<Course> successors { get; }
        public departmentConsent departmentConsentRequirement { get; }
        public List<Lecture> lecturesList { get; }
        public List<Tutorial> tutorialsList { get; }
        public List<Lab> labsList { get; }
        public int courseUnits { get; set; }
        /// <summary>
        /// Create an instance of a Course.
        /// </summary>
        /// <param name="courseID">ID of course. e.g. "CPSC 481"</param>
        /// <param name="courseTitle">Title of course. e.g. "Human-Computer Interaction I"</param>
        /// <param name="departmentConsentRequirement">NONE for no requirements, OR and AND otherwise.</param>
        public Course(int courseID, string courseTitle, Faculty faculty, Department department, departmentConsent departmentConsentRequirement, string courseDescription)
        {

            this.courseID = courseID;
            this.courseTitle = courseTitle;
            this.faculty = faculty;
            this.department = department;
            this.departmentConsentRequirement = departmentConsentRequirement;
            this.prerequisites = new List<Course>();
            this.antirequisites = new List<Course>();
            this.successors = new List<Course>();
            this.lecturesList = new List<Lecture>();
            this.tutorialsList = new List<Tutorial>();
            this.labsList = new List<Lab>();
            this.courseDescription = courseDescription;
            this.courseUnits = 6;

        }

        public Course(Course course)
        {
            this.courseID = course.courseID;
            this.courseTitle = course.courseTitle;
            this.faculty = course.faculty;
            this.department = course.department;
            this.departmentConsentRequirement = course.departmentConsentRequirement;
            this.prerequisites = course.prerequisites;
            this.antirequisites = course.antirequisites;
            this.successors = course.successors;
            this.lecturesList = course.lecturesList;
            this.tutorialsList = course.tutorialsList;
            this.labsList =course.labsList;
            this.courseDescription = course.courseDescription;
            this.courseUnits = 6;
        }

    }

    public class ClassInstance : Course
    {
        public int lectureNum { get; set; }
        public int tutorialNum { get; set; }
        
        //add semester 

        public int labNum { get; set; }
        public ClassInstance(int courseID, string courseTitle, Faculty faculty, Department department, departmentConsent departmentConsentRequirement, string courseDescription, int lecture, int tutorial = 0, int lab = 0) :
            base(courseID, courseTitle, faculty, department, departmentConsentRequirement, courseDescription)
        {
            this.lectureNum = lecture;
            this.tutorialNum = tutorial;
            this.labNum = lab;
        }

        public ClassInstance(Course course, int lecture, int tutorial = 0, int lab = 0) : base(course)
        {
            this.lectureNum = lecture;
            this.tutorialNum = tutorial;
            this.labNum = lab;
        }
    }


    public enum Campus
    {
        NONE, UniversityOfCalgary, WebBased, RedDeer
    }

    public class TimeBlock
    {
        private string startTime;
        public string StartTime
        {
            get { return startTime; }
            set
            {
                TimeSpan dummyOutput;
                if (!TimeSpan.TryParse(value, out dummyOutput))
                {
                    Console.WriteLine("WARNING: start time value invalid");
                    startTime = "00:00:00";
                };
            }
        }
        private string endTime;
        public string EndTime{ get { return endTime; } set { endTime = value; } }

        //Monday = 16, Tuesday = 8, Wednesday = 4, Thursday = 2, Friday = 1
        //example: Monday, Wednesday, Friday class, days = 17 + 5 + 0= 22
        //example: Tuesday, Thursday class, days = 8 + 2 = 10
        public int days
        {
            set
            {
                if (value > 31)
                {
                    Console.WriteLine("WARNING: days value invalid");
                }

                if ((value & 0b1) == 1)
                {
                    friday = true;
                }

                if ((value & 0b10) == 1)
                {
                    thursday = true;
                }

                if ((value & 0b100) == 1)
                {
                    wednesday = true;
                }

                if ((value & 0b1000) == 1)
                {
                    tuesday = true;
                }

                if ((value & 0b10000) == 1)
                {
                    monday = true;
                }
            }
        }

            public TimeBlock(int days, string startTime, TimeSpan time)
            {
            this.days = days;
            this.startTime = startTime;
            this.endTime = TimeSpan.Parse(startTime).Add(time).ToString();
            }

        

        public bool monday { get; set; }
        public bool tuesday { get; set; }
        public bool wednesday { get; set; }
        public bool thursday { get; set; }
        public bool friday { get; set; }
    }

    public abstract class Assignable
    {
        public Course course { get; set; }
        public TimeBlock time { get; set; }
        public string instructor { get; set; }
        public Campus campus { get; set; }

        public string room { get; set; }
        public int currentWaitlist { get; set; }
        public int maxWaitlist { get; set; }
        public int maxStudents { get; set; }
        public int currentStudents { get; set; }

        public string notes { get; set; }
      
    }

    public class Lecture : Assignable
    {
        public Tutorial tutorial { get; set; }

        public Lecture(Course course, TimeBlock time, string instructor, Campus campus, string room, int currentStudents, int maxStudents, int currentWaitlist, int maxWaitlist, string note)
        {
            this.time = time;
            this.instructor = instructor;
            this.campus = campus;
            this.room = room;
            this.currentStudents = currentStudents;
            this.maxStudents = maxStudents;
            this.currentWaitlist = currentWaitlist;
            this.maxWaitlist = maxWaitlist;
            this.notes = notes;
        }

    }

    public class Tutorial : Assignable
    {

        public Tutorial(Course course, TimeBlock time, string instructor, Campus campus, string room, int currentStudents, int maxStudents, int currentWaitlist, int maxWaitlist, string note)
        {
            this.time = time;
            this.instructor = instructor;
            this.campus = campus;
            this.room = room;
            this.currentStudents = currentStudents;
            this.maxStudents = maxStudents;
            this.currentWaitlist = currentWaitlist;
            this.maxWaitlist = maxWaitlist;
            this.notes = note;
        }

    }

    public class Lab : Assignable
    {

        public Lab(Course course, TimeBlock time, string instructor, Campus campus,  string room, int currentStudents, int maxStudents, int currentWaitlist, int maxWaitlist, string note)
        {
            this.time = time;
            this.instructor = instructor;
            this.campus = campus;
            this.room = room;
            this.currentStudents = currentStudents;
            this.maxStudents = maxStudents;
            this.currentWaitlist = currentWaitlist;
            this.maxWaitlist = maxWaitlist;
            this.notes = note;
        }

    }

    public class BlockWeekClass
    {
        // needed?
    }
}
