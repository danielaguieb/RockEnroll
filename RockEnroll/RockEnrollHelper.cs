using System;
using System.Collections.Generic;
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

        public static void InitializeCourses()
        {
            // add courses here
            Course soci201 = new Course("SOCI 321", "Introduction to Sociology", Course.departmentConsent.NONE, "Sociology as a discipline examines how the society in which we live influences our thinking and behaviour. An introduction to sociology through the study of society, social institutions, group behaviour and social change.");
            allCourses.Add(soci201);

            Course soci321 = new Course("SOCI 321", "Sociology of Health and Illness", Course.departmentConsent.OR, null);
            soci321.prerequisites.Add(new List<Course> { soci201 });
            allCourses.Add(soci321);
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
        public List<Course> currentSchedule { get; }

        public Student(string firstName, string lastName, int studentID)
        {

            this.firstName = firstName;
            this.lastName = lastName;
            this.studentID = studentID;
            this.coursesTaken = new List<Course>();
            this.currentSchedule = new List<Course>();

        }

    }

    public class Course
    {

        // use OR for "or consent of department"; AND for "and consent of department"
        public enum departmentConsent
        {
            NONE, OR, AND
        }

        public string courseID { get; set; }
        public string courseTitle { get; set; }
        public string courseDescription { get; set; }
        /// <summary>
        /// Course prerequisites. Use different lists for alternative requirements.
        /// </summary>
        public List<List<Course>> prerequisites { get; }
        public List<Course> antirequisites { get; }
        public departmentConsent departmentConsentRequirement { get; }
        public List<Lecture> lecturesList { get; }
        public List<Tutorial> tutorialsList { get; }
        public List<Lab> labsList { get; }

        /// <summary>
        /// Create an instance of a Course.
        /// </summary>
        /// <param name="courseID">ID of course. e.g. "CPSC 481"</param>
        /// <param name="courseTitle">Title of course. e.g. "Human-Computer Interaction I"</param>
        /// <param name="departmentConsentRequirement">NONE for no requirements, OR and AND otherwise.</param>
        public Course(string courseID, string courseTitle, departmentConsent departmentConsentRequirement, string courseDescription)
        {

            this.courseID = courseID;
            this.courseTitle = courseTitle;
            this.departmentConsentRequirement = departmentConsentRequirement;
            this.prerequisites = new List<List<Course>>();
            this.antirequisites = new List<Course>();
            this.lecturesList = new List<Lecture>();
            this.tutorialsList = new List<Tutorial>();
            this.labsList = new List<Lab>();
            this.courseDescription = courseDescription;

        }

    }

    public enum Campus
    {
        NONE, UniversityOfCalgary, WebBased, RedDeer
    }

    public enum Faculty
    {
        NONE, Arts, Medicine, Architecture, GraduateStudies, Business, Kinesiology, Law, Nursing, Engineering, Science, SocialWork, Veterinary, Education
    }

    public abstract class Assignable
    {
        public TimeSpan time { get; set; }
        public string instructor { get; set; }
        public Campus campus { get; set; }
        public Faculty faculty { get; set; }
        public int currentWaitlist { get; set; }
        public int maxWaitlist { get; set; }
        public int maxStudents { get; set; }
        public int currentStudents { get; set; }
        public int days
        {
            set
            {
                if (value > 31)
                {
                    Console.WriteLine("WARNING: days value invalid");
                }

                if ((value ^ 0b1) == 1)
                {
                    friday = true;
                }

                if ((value ^ 0b10) == 1)
                {
                    thursday = true;
                }

                if ((value ^ 0b100) == 1)
                {
                    wednesday = true;
                }

                if ((value ^ 0b1000) == 1)
                {
                    tuesday = true;
                }

                if ((value ^ 0b10000) == 1)
                {
                    monday = true;
                }
            }
        }
        public bool monday { get; set; }
        public bool tuesday { get; set; }
        public bool wednesday { get; set; }
        public bool thursday { get; set; }
        public bool friday { get; set; }
    }

    public class Lecture : Assignable
    {
        public Tutorial tutorial { get; set; }

        public Lecture(int days, TimeSpan time, string instructor, Campus campus, Faculty faculty, int currentStudents, int maxStudents, int currentWaitlist, int maxWaitlist)
        {
            this.time = time;
            this.instructor = instructor;
            this.days = days;
            this.campus = campus;
            this.faculty = faculty;
            this.currentStudents = currentStudents;
            this.maxStudents = maxStudents;
            this.currentWaitlist = currentWaitlist;
            this.maxWaitlist = maxWaitlist;
        }

    }

    public class Tutorial : Assignable
    {

        public Tutorial(TimeSpan time, string instructor, Campus campus, Faculty faculty, int currentStudents, int maxStudents, int currentWaitlist, int maxWaitlist)
        {
            this.time = time;
            this.instructor = instructor;
            this.campus = campus;
            this.faculty = faculty;
            this.currentStudents = currentStudents;
            this.maxStudents = maxStudents;
            this.currentWaitlist = currentWaitlist;
            this.maxWaitlist = maxWaitlist;
        }

    }

    public class Lab : Assignable
    {

        public Lab(TimeSpan time, string instructor, Campus campus, Faculty faculty, int currentStudents, int maxStudents, int currentWaitlist, int maxWaitlist)
        {
            this.time = time;
            this.instructor = instructor;
            this.campus = campus;
            this.faculty = faculty;
            this.currentStudents = currentStudents;
            this.maxStudents = maxStudents;
            this.currentWaitlist = currentWaitlist;
            this.maxWaitlist = maxWaitlist;
        }

    }

    public class BlockWeekClass
    {
        // needed?
    }
}
