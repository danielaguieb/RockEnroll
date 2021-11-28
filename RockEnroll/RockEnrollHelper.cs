using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockEnroll
{
    class RockEnrollHelper
    {

        public Student student = new Student("John", "Doe", 30045232);

        public enum terms
        {
            FALL2021, WINTER2022, SPRING2022, SUMMER2022
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
            Course soci201 = new Course("SOCI 321", "Introduction to Sociology", Course.departmentConsent.NONE);
            allCourses.Add(soci201);

            Course soci321 = new Course("SOCI 321", "Sociology of Health and Illness", Course.departmentConsent.OR);
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
        /// <summary>
        /// Course prerequisites. Use different lists for alternative requirements.
        /// </summary>
        public List<List<Course>> prerequisites { get; }
        public List<Course> antirequisites { get; }
        public departmentConsent departmentConsentRequirement { get; }

        /// <summary>
        /// Create an instance of a Course.
        /// </summary>
        /// <param name="courseID">ID of course. e.g. "CPSC 481"</param>
        /// <param name="courseTitle">Title of course. e.g. "Human-Computer Interaction I"</param>
        /// <param name="departmentConsentRequirement">NONE for no requirements, OR and AND otherwise.</param>
        public Course(string courseID, string courseTitle, departmentConsent departmentConsentRequirement)
        {

            this.courseID = courseID;
            this.courseTitle = courseTitle;
            this.departmentConsentRequirement = departmentConsentRequirement;

        }

    }
}
