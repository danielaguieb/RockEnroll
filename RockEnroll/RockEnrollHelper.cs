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

        public static Student student = new Student("John", "Doe", 30045232);

        private static Terms currentTerm = Terms.FALL2021;
        public static Terms CurrentTerm
        {
            get { return currentTerm; }
            set { currentTerm = value; }
        }
        public static CourseList _coursePage = new CourseList();
        public static AcademicRequirements _reqPage = new AcademicRequirements();
        public static Schedules _schedulePage = new Schedules();
        public static AdvSearchWindow _advsearch;

        public static List<Course> fall21Courses = new List<Course>();
        public static List<Course> winter22Courses = new List<Course>();
        public static List<Course> spring22Courses = new List<Course>();
        public static List<Course> summer22Courses = new List<Course>();

        // all courses list here; create courses first before adding them, to modify prereqs
        public static List<Course> allCourses = new List<Course>();
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
            TimeBlock tbTut1_1 = new TimeBlock(20, "10:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbTut1_2 = new TimeBlock(10, "11:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbTut1_3 = new TimeBlock(20, "12:00", new TimeSpan(0, 0, 50, 0, 0));

            TimeBlock tbTut2_1 = new TimeBlock(20, "14:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbTut2_2 = new TimeBlock(10, "12:00", new TimeSpan(0, 0, 50, 0, 0));
            TimeBlock tbTut2_3 = new TimeBlock(10, "15:00", new TimeSpan(0, 0, 50, 0, 0));

            //Lab Timeblocks
            TimeBlock tbLab1 = new TimeBlock(16, "16:00", new TimeSpan(0, 3, 0, 0, 0));
            TimeBlock tbLab2 = new TimeBlock(8, "16:00", new TimeSpan(0, 3, 0, 0, 0));
            TimeBlock tbLab3 = new TimeBlock(4, "16:00", new TimeSpan(0, 3, 0, 0, 0));
            TimeBlock tbLab4 = new TimeBlock(2, "16:00", new TimeSpan(0, 3, 0, 0, 0));

            // add courses here
            Course soci201 = new Course(201, "Introductory Sociology", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Sociology as a discipline examines how the society in which we live influences our thinking and behaviour. An introduction to sociology through the study of society, social institutions, group behaviour and social change.");
            soci201.lecturesList.Add(new Lecture(soci201, tbLec1_1, "John Smith", Campus.UniversityOfCalgary, "Social Science Rm 109", 50, 200, 0, 30, ""));
            soci201.lecturesList.Add(new Lecture(soci201, tbLec1_2, "John Smith", Campus.UniversityOfCalgary, "Social Science Rm 109", 120, 200, 0, 30, ""));
            soci201.tutorialsList.Add(new Tutorial(soci201, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci201.tutorialsList.Add(new Tutorial(soci201, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci201.tutorialsList.Add(new Tutorial(soci201, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            allCourses.Add(soci201);
            student.coursesTaken.Add(soci201);

            Course soci321 = new Course(321, "Sociology of Health and Illness", Faculty.Arts, Department.SOCI, Course.departmentConsent.OR, 3, "Introduction to social factors influencing health, illness, and medicine. Topics covered may include the organization of medical institutions and occupations, the socialization of medical professionals, the social construction of illness, social determinants of health, and comparative health care systems and policy.");
            soci321.lecturesList.Add(new Lecture(soci321, tbLec1_1, "Jane Doe", Campus.UniversityOfCalgary, "Social Science Rm 18", 50, 200, 0, 30, ""));
            soci321.lecturesList.Add(new Lecture(soci321, tbLec1_2, "Jane Doe", Campus.UniversityOfCalgary, "Social Science Rm 18", 120, 200, 0, 30, ""));
            soci321.tutorialsList.Add(new Tutorial(soci321, tbTut2_1, "", Campus.UniversityOfCalgary, "Social Science Rm 06", 5, 30, 0, 0, ""));
            soci321.tutorialsList.Add(new Tutorial(soci321, tbTut2_2, "", Campus.UniversityOfCalgary, "Social Science Rm 06", 5, 30, 0, 0, ""));
            soci321.tutorialsList.Add(new Tutorial(soci321, tbTut2_3, "", Campus.UniversityOfCalgary, "Social Science Rm 06", 5, 30, 0, 0, ""));
            soci321.prerequisites.Add(soci201);
            soci201.successors.Add(soci321);
            allCourses.Add(soci321);


            Course soci311 = new Course(311, "Introductory Social Statistics I", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Univariate and bivariate statistics for survey data. Topics include cross tabular analysis, the normal distribution, confidence intervals for means, hypothesis testing, Chi-squared and F distributions and bivariate linear regression analysis. In labs statistical software to analyze survey data will be used.");
            soci311.lecturesList.Add(new Lecture(soci311, tbLec3_1, "Mark Curr", Campus.UniversityOfCalgary, "Social Science Rm 109", 100, 200, 0, 30, ""));
            soci311.lecturesList.Add(new Lecture(soci311, tbLec3_2, "John Smith", Campus.UniversityOfCalgary, "Social Science Rm 109", 200, 200, 0, 30, ""));
            soci311.tutorialsList.Add(new Tutorial(soci311, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci311.tutorialsList.Add(new Tutorial(soci311, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci311.tutorialsList.Add(new Tutorial(soci311, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci311.prerequisites.Add(soci201);
            soci201.successors.Add(soci311);
            allCourses.Add(soci311);

            Course soci313 = new Course(313, "Introductory Social Research Methods", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Research processes including problem definition, data collection and analyses; quantitative and qualitative strategies.");
            soci313.lecturesList.Add(new Lecture(soci313, tbLec1_1, "Mark Curr", Campus.UniversityOfCalgary, "Social Science Rm 109", 141, 200, 0, 30, ""));
            soci313.lecturesList.Add(new Lecture(soci313, tbLec1_2, "JaneDoe", Campus.UniversityOfCalgary, "Social Science Rm 109", 15, 200, 0, 30, ""));
            soci313.tutorialsList.Add(new Tutorial(soci313, tbTut2_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci313.tutorialsList.Add(new Tutorial(soci313, tbTut2_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci313.tutorialsList.Add(new Tutorial(soci313, tbTut2_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci313.prerequisites.Add(soci201);
            soci201.successors.Add(soci313);
            allCourses.Add(soci313);

            Course soci315 = new Course(315, "Introductory Social Statistics II", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Multivariate statistics for survey data; and measurement issues in quantitative research. Topics include reliability, multivariate tabular analysis, multiple regression, dummy variable regression, statistical interaction and path analysis. In labs statistical software to test measurement and causal models will be used.");
            soci315.lecturesList.Add(new Lecture(soci315, tbLec2_1, "Mark Curr", Campus.UniversityOfCalgary, "Social Science Rm 109", 59, 200, 0, 30, ""));
            soci315.lecturesList.Add(new Lecture(soci315, tbLec2_2, "JaneDoe", Campus.UniversityOfCalgary, "Social Science Rm 109", 22, 200, 0, 30, ""));
            soci315.tutorialsList.Add(new Tutorial(soci315, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci315.tutorialsList.Add(new Tutorial(soci315, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci315.tutorialsList.Add(new Tutorial(soci315, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci315.prerequisites.Add(soci311);
            soci311.successors.Add(soci315);
            allCourses.Add(soci315);

            Course soci325 = new Course(325, "Introduction to Deviance and Social Control", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "The presentation and analysis of theories of criminality and of non-criminal deviance, methods to uncover the incidence of deviance and criminality, a survey of forms of deviant and criminal behaviours, and the social and institutional responses to them.");
            soci325.lecturesList.Add(new Lecture(soci325, tbLec2_1, "Mark Curr", Campus.UniversityOfCalgary, "Social Science Rm 109", 59, 200, 0, 30, ""));
            soci325.lecturesList.Add(new Lecture(soci325, tbLec2_2, "JaneDoe", Campus.UniversityOfCalgary, "Social Science Rm 109", 22, 200, 0, 30, ""));
            soci325.tutorialsList.Add(new Tutorial(soci325, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci325.tutorialsList.Add(new Tutorial(soci325, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci325.tutorialsList.Add(new Tutorial(soci325, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci325.prerequisites.Add(soci201);
            soci201.successors.Add(soci325);
            allCourses.Add(soci325);

            Course soci327 = new Course(327, "Introduction to Criminal Justice", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Introduction to the field of criminal justice in Canada from a sociological perspective. May include: examination of the definitions of crime; crime measurement; institutional responses to crime by the police, the courts and correctional services; and alternatives to the justice model.");
            soci327.lecturesList.Add(new Lecture(soci327, tbLec3_1, "Mark Curr", Campus.UniversityOfCalgary, "Social Science Rm 109", 59, 200, 0, 30, ""));
            soci327.lecturesList.Add(new Lecture(soci327, tbLec3_2, "JaneDoe", Campus.UniversityOfCalgary, "Social Science Rm 109", 22, 200, 0, 30, ""));
            soci327.tutorialsList.Add(new Tutorial(soci327, tbTut2_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci327.tutorialsList.Add(new Tutorial(soci327, tbTut2_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci327.tutorialsList.Add(new Tutorial(soci327, tbTut2_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci327.prerequisites.Add(soci201);
            soci201.successors.Add(soci327);
            allCourses.Add(soci327);

            Course soci329 = new Course(329, "The Sociology of Law", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "An introduction to sociological problems regarding the origin, impact and definition of law, dispute resolution, and the relationship between law and social change.");
            soci329.lecturesList.Add(new Lecture(soci329, tbLec1_1, "Mark Curr", Campus.UniversityOfCalgary, "Social Science Rm 109", 59, 200, 0, 30, ""));
            soci329.lecturesList.Add(new Lecture(soci329, tbLec1_2, "JaneDoe", Campus.UniversityOfCalgary, "Social Science Rm 109", 22, 200, 0, 30, ""));
            soci329.tutorialsList.Add(new Tutorial(soci329, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci329.tutorialsList.Add(new Tutorial(soci329, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci329.tutorialsList.Add(new Tutorial(soci329, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci329.prerequisites.Add(soci201);
            soci201.successors.Add(soci329);
            allCourses.Add(soci329);

            Course soci331 = new Course(331, "Classical Sociological Theory", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "The development of sociological theory from the nineteenth century to the Second World War.");
            soci315.lecturesList.Add(new Lecture(soci331, tbLec3_1, "Mark Curr", Campus.UniversityOfCalgary, "Social Science Rm 109", 100, 200, 0, 30, ""));
            soci315.lecturesList.Add(new Lecture(soci331, tbLec3_2, "JaneDoe", Campus.UniversityOfCalgary, "Social Science Rm 109", 24, 200, 0, 30, ""));
            soci315.tutorialsList.Add(new Tutorial(soci331, tbTut2_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci315.tutorialsList.Add(new Tutorial(soci331, tbTut2_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci315.tutorialsList.Add(new Tutorial(soci331, tbTut2_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci315.prerequisites.Add(soci201);
            soci201.successors.Add(soci331);
            allCourses.Add(soci331);

            Course soci333 = new Course(331, "Contemporary Sociological Theory", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "The development of sociological theory from the Second World War to the present.");
            soci333.lecturesList.Add(new Lecture(soci333, tbLec1_1, "Mark Curr", Campus.UniversityOfCalgary, "Social Science Rm 109", 100, 200, 0, 30, ""));
            soci333.lecturesList.Add(new Lecture(soci333, tbLec1_2, "JaneDoe", Campus.UniversityOfCalgary, "Social Science Rm 109", 24, 200, 0, 30, ""));
            soci333.tutorialsList.Add(new Tutorial(soci333, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci333.tutorialsList.Add(new Tutorial(soci333, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci333.tutorialsList.Add(new Tutorial(soci333, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci333.prerequisites.Add(soci331);
            soci331.successors.Add(soci333);
            allCourses.Add(soci333);

            Course soci421 = new Course(421, "Topics in Deviance and Criminology", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Advanced study of contemporary issues in the research in deviance and crime.");
            soci421.lecturesList.Add(new Lecture(soci421, tbLec2_1, "Samuel Baker", Campus.UniversityOfCalgary, "Social Science Rm 109", 100, 200, 0, 30, ""));
            soci421.lecturesList.Add(new Lecture(soci421, tbLec2_2, "Samuel Baker", Campus.UniversityOfCalgary, "Social Science Rm 109", 24, 200, 0, 30, ""));
            soci421.tutorialsList.Add(new Tutorial(soci421, tbTut2_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci421.tutorialsList.Add(new Tutorial(soci421, tbTut2_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci421.tutorialsList.Add(new Tutorial(soci421, tbTut2_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci421.prerequisites.Add(soci313);
            soci421.prerequisites.Add(soci315);
            soci421.prerequisites.Add(soci325);
            soci421.prerequisites.Add(soci331);
            soci421.prerequisites.Add(soci333);
            soci313.successors.Add(soci421);
            soci315.successors.Add(soci421);
            soci325.successors.Add(soci421);
            soci331.successors.Add(soci421);
            soci333.successors.Add(soci421);
            allCourses.Add(soci421);

            Course soci423 = new Course(423, "The Sociology of Youth Crime", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Explanations of the criminal activities of young people including an assessment of treatment strategies and legal regimes developed in response to this behaviour.");
            soci423.lecturesList.Add(new Lecture(soci423, tbLec3_1, "Samuel Baker", Campus.UniversityOfCalgary, "Social Science Rm 109", 100, 200, 0, 30, ""));
            soci423.lecturesList.Add(new Lecture(soci423, tbLec3_2, "Samuel Baker", Campus.UniversityOfCalgary, "Social Science Rm 109", 24, 200, 0, 30, ""));
            soci423.tutorialsList.Add(new Tutorial(soci423, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci423.tutorialsList.Add(new Tutorial(soci423, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci423.tutorialsList.Add(new Tutorial(soci423, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci423.prerequisites.Add(soci313);
            soci423.prerequisites.Add(soci315);
            soci423.prerequisites.Add(soci325);
            soci423.prerequisites.Add(soci331);
            soci423.prerequisites.Add(soci333);
            soci313.successors.Add(soci423);
            soci315.successors.Add(soci423);
            soci325.successors.Add(soci423);
            soci331.successors.Add(soci423);
            soci333.successors.Add(soci423);
            allCourses.Add(soci423);


            Course soci425 = new Course(425, "The Sociology of Violence", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "An exploration of violence in a variety of situations and social institutions and more general patterns of victimization in contemporary society.");
            soci425.lecturesList.Add(new Lecture(soci425, tbLec1_1, "Samuel Baker", Campus.UniversityOfCalgary, "Social Science Rm 109", 100, 200, 0, 30, ""));
            soci425.lecturesList.Add(new Lecture(soci425, tbLec1_2, "Samuel Baker", Campus.UniversityOfCalgary, "Social Science Rm 109", 24, 200, 0, 30, ""));
            soci425.tutorialsList.Add(new Tutorial(soci425, tbTut2_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci425.tutorialsList.Add(new Tutorial(soci425, tbTut2_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci425.tutorialsList.Add(new Tutorial(soci425, tbTut2_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci425.prerequisites.Add(soci313);
            soci425.prerequisites.Add(soci315);
            soci425.prerequisites.Add(soci325);
            soci425.prerequisites.Add(soci331);
            soci425.prerequisites.Add(soci333);
            soci313.successors.Add(soci425);
            soci315.successors.Add(soci425);
            soci325.successors.Add(soci425);
            soci331.successors.Add(soci425);
            soci333.successors.Add(soci425);
            allCourses.Add(soci425);

            Course soci427 = new Course(427, "The Social Organization of Criminal Justice", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Comparative social organization of the criminal justice system from a sociological perspective; special attention to and analysis of the structure of the Canadian criminal justice system.");
            soci427.lecturesList.Add(new Lecture(soci427, tbLec2_1, "Samuel Baker", Campus.UniversityOfCalgary, "Social Science Rm 109", 100, 200, 0, 30, ""));
            soci427.lecturesList.Add(new Lecture(soci427, tbLec2_2, "Samuel Baker", Campus.UniversityOfCalgary, "Social Science Rm 109", 24, 200, 0, 30, ""));
            soci427.tutorialsList.Add(new Tutorial(soci427, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci427.tutorialsList.Add(new Tutorial(soci427, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci427.tutorialsList.Add(new Tutorial(soci427, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            soci427.prerequisites.Add(soci313);
            soci427.prerequisites.Add(soci315);
            soci427.prerequisites.Add(soci325);
            soci427.prerequisites.Add(soci331);
            soci427.prerequisites.Add(soci333);
            soci313.successors.Add(soci427);
            soci315.successors.Add(soci427);
            soci325.successors.Add(soci427);
            soci331.successors.Add(soci427);
            soci333.successors.Add(soci427);
            allCourses.Add(soci427);

            Course chem201 = new Course(201, "General Chemistry: Structure and Bonding", Faculty.Science, Department.CHEM, Course.departmentConsent.NONE, 3, "An introduction to university chemistry from theoretical and practical perspectives, that focuses on an exploration of the fundamental links between electronic structure, chemical bonding, molecular structure and the interactions of molecules using inorganic and organic examples.");
            chem201.lecturesList.Add(new Lecture(chem201, tbLec1_1, "Miranda Frizzle", Campus.UniversityOfCalgary, "Science Theatres 140", 100, 200, 0, 30, ""));
            chem201.lecturesList.Add(new Lecture(chem201, tbLec1_2, "Miranda Frizzle", Campus.UniversityOfCalgary, "Science Theatres 140", 24, 200, 0, 30, ""));
            chem201.tutorialsList.Add(new Tutorial(chem201, tbTut2_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            chem201.tutorialsList.Add(new Tutorial(chem201, tbTut2_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            chem201.tutorialsList.Add(new Tutorial(chem201, tbTut2_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            chem201.labsList.Add(new Lab(chem201, tbLab1, "", Campus.UniversityOfCalgary, "EEEL 200", 5, 30, 0, 0, ""));
            chem201.labsList.Add(new Lab(chem201, tbLab3, "", Campus.UniversityOfCalgary, "EEEL 201", 5, 30, 0, 0, ""));
            student.coursesTaken.Add(chem201);
            allCourses.Add(chem201);

            Course cpsc217 = new Course(217, "Introduction to Computer Science for Multidisciplinary Studies I", Faculty.Science, Department.CPSC, Course.departmentConsent.NONE, 3, "Introduction to problem solving, analysis and design of small-scale computational systems and implementation using a procedural programming language. For students wishing to combine studies in computer science with studies in other disciplines.");
            cpsc217.lecturesList.Add(new Lecture(cpsc217, tbLec2_1, "Miranda Frizzle", Campus.WebBased, "", 100, 200, 0, 30, ""));
            cpsc217.lecturesList.Add(new Lecture(cpsc217, tbLec2_2, "Miranda Frizzle", Campus.WebBased, "", 24, 200, 0, 30, ""));
            cpsc217.tutorialsList.Add(new Tutorial(cpsc217, tbTut1_1, "", Campus.WebBased, "", 5, 30, 0, 0, ""));
            cpsc217.tutorialsList.Add(new Tutorial(cpsc217, tbTut1_2, "", Campus.WebBased, "", 5, 30, 0, 0, ""));
            cpsc217.tutorialsList.Add(new Tutorial(cpsc217, tbTut1_3, "", Campus.WebBased, "", 5, 30, 0, 0, ""));
            student.coursesTaken.Add(cpsc217);
            allCourses.Add(cpsc217);


            Course test211 = new Course(211, "Test Course", Faculty.Arts, Department.SOCI, Course.departmentConsent.NONE, 3, "Test Course for wait list testing");
            test211.lecturesList.Add(new Lecture(test211, tbLec1_1, "Kylie Sicat", Campus.UniversityOfCalgary, "Social Science Rm 109", 200, 200, 0, 30, ""));
            test211.lecturesList.Add(new Lecture(test211, tbLec1_2, "Kylie Kylie", Campus.UniversityOfCalgary, "Social Science Rm 109", 199, 200, 0, 30, ""));
            test211.tutorialsList.Add(new Tutorial(test211, tbTut1_1, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            test211.tutorialsList.Add(new Tutorial(test211, tbTut1_2, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            test211.tutorialsList.Add(new Tutorial(test211, tbTut1_3, "", Campus.UniversityOfCalgary, "Science Theatres Rm 139", 5, 30, 0, 0, ""));
            allCourses.Add(test211);


        }

        public static bool checkClassCapacity(int courseId)
        {
            //TODO - we need the lecture too
            // get the Course based on the courseId
            if (courseId == 211) // forcing to show wait listed only for 1 course
                return false;
            else
                return true;
        }

        public static List<(ClassInstance, int, int, int)> FindConflict(ClassInstance cor)
        {
            List<(ClassInstance,int,int,int)> conflicts = new List<(ClassInstance,int,int,int)>();
          
            bool conflict = false;
            foreach (ClassInstance c in student.currentSchedule)
            {
                if(c == cor)
                {
                    continue;
                }
                int lec1 = -1, tut1 = -1, lab1 = -1;
                conflict = false;
                if ((c.lecturesList.Count() != 0))
                {
                    if (c.lecturesList[c.lectureNum].time.StartTime.Equals(cor.lecturesList[cor.lectureNum].time.StartTime))
                    {
                        conflict = true;
                        lec1 = c.lectureNum;
                    }
                }
                if ((c.tutorialsList.Count() != 0))
                {
                    if (c.tutorialsList[c.tutorialNum].time.StartTime.Equals(cor.tutorialsList[cor.tutorialNum].time.StartTime))
                    {
                        conflict = true;
                        tut1 = c.tutorialNum;
                    }
                }
                if ((c.labsList.Count() != 0))
                {
                    if (c.labsList[c.labNum].time.StartTime.Equals(c.labsList[cor.labNum].time.StartTime))
                    {
                        conflict = true;
                        lab1 = c.labNum;
                    }
                }

                if (conflict)
                {
                    conflicts.Add((c, lec1, tut1, lab1));
                }
            }

            return conflicts;
        }

        //Naive solution for finding available section
        public static int FindAvailableSection<T>(List<T> times) where T : Assignable
        {
            //Need to add conflict functionality
            //int result = -1;
            int result = 0;
            for (int i = 0; i < times.Count(); i++)
            {
                bool conflict = false;
                foreach (ClassInstance c in student.currentSchedule)
                {
                    if ((c.lecturesList.Count() != 0))
                    {
                        if (c.lecturesList[c.lectureNum].time.StartTime.Equals(times[i].time.StartTime))
                        {
                            conflict = true;
                        }
                    }
                    if ((c.tutorialsList.Count() != 0))
                    {
                        if (c.tutorialsList[c.tutorialNum].time.StartTime.Equals(times[i].time.StartTime))
                        {
                            conflict = true;
                        }
                    }
                    if ((c.labsList.Count() != 0))
                    {
                        if (c.labsList[c.labNum].time.StartTime.Equals(times[i].time.StartTime)) 
                        {
                            conflict = true;
                        }
                    }
                }
                if (!conflict)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public static void PopulateSchedule()
        {
            List<ClassInstance> mondayLectures = new List<ClassInstance>();
            for (int i = 0; i < student.currentSchedule.Count; i++)
            {
                int lecNum = student.currentSchedule[i].lectureNum;
                int tutNum = student.currentSchedule[i].tutorialNum;

                TimeBlock lecTime = student.currentSchedule[i].lecturesList[lecNum].time;
                TimeBlock tutTime = student.currentSchedule[i].tutorialsList[tutNum].time;

                if (lecTime.monday)
                {
                    mondayLectures.Add(student.currentSchedule[i]);
                }

            }

            // The default comparer doesn't work here
            mondayLectures.Sort();

        }

        public static void AddCourse(Course course, bool view = false)
        {
            int lecNum = 0;
            int tutNum = 0;
            int labNum = 0;
            if (!view)
            {
                //find which lecture and tutorial time fit
                lecNum = FindAvailableSection(course.lecturesList);
                tutNum = FindAvailableSection(course.tutorialsList);
                labNum = FindAvailableSection(course.labsList);
            }

            ClassInstance c = new ClassInstance(course, CurrentTerm, lecNum, tutNum, labNum);
            student.currentSchedule.Add(c);
            _coursePage.AddClass(c);
        }

        public static void AddCourse(ClassInstance c)
        {
            if (student.currentSchedule.Contains(c))
            {
                return;
            }
            student.currentSchedule.Add(c);
            _coursePage.AddClass(c);
        }

        public static void updateCoursePage()
        {
            _coursePage = new CourseList();
            for (int i = 0; i < student.currentSchedule.Count(); i++)
            {
                ClassInstance c = (ClassInstance)RockEnrollHelper.student.currentSchedule[i];
                _coursePage.AddClass(c);
            }
        }

        public static void RemoveCourse(ClassInstance c)
        {
            student.currentSchedule.Remove(c);
        }

        public static void SwapSection(ClassInstance c, int lecNum, int tutNum, int labNum)
        {
            c.lectureNum = lecNum;
            c.tutorialNum = tutNum;
            c.labNum = labNum;
            _coursePage.updateSections();
        }

    }
    public enum Terms
    {
        NONE, FALL2021, WINTER2022, SPRING2022, SUMMER2022
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
        public Course(int courseID, string courseTitle, Faculty faculty, Department department, departmentConsent departmentConsentRequirement, int courseUnits, string courseDescription)
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
            this.courseUnits = courseUnits;

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
            this.labsList = course.labsList;
            this.courseDescription = course.courseDescription;
            this.courseUnits = 6;
        }

    }

    public class ClassInstance : Course
    {
        public int lectureNum { get; set; }
        public int tutorialNum { get; set; }
        public int labNum { get; set; }
        public Terms term { get; set; }

        public bool enrolled { get; set; }
        public bool swapped { get; set; }
        public bool dropped { get; set; }
        public bool waitListed { get; set; }

        public ClassInstance(int courseID, string courseTitle, Faculty faculty, Department department, departmentConsent departmentConsentRequirement, int units, string courseDescription, Terms term, int lecture, int tutorial = 0, int lab = 0) :
            base(courseID, courseTitle, faculty, department, departmentConsentRequirement, units, courseDescription)
        {
            this.lectureNum = lecture;
            this.tutorialNum = tutorial;
            this.labNum = lab;
            this.term = term;
            this.enrolled = false;
            this.swapped = false;
            this.dropped = false;
            this.waitListed = false;
        }

        public ClassInstance(Course course, Terms term, int lecture, int tutorial = 0, int lab = 0) : base(course)
        {
            this.lectureNum = lecture;
            this.tutorialNum = tutorial;
            this.labNum = lab;
            this.term = term;
            this.enrolled = false;
            this.swapped = false;
            this.dropped = false;
            this.waitListed = false;
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
        public string EndTime { get { return endTime; } set { endTime = value; } }

        //Monday = 16, Tuesday = 8, Wednesday = 4, Thursday = 2, Friday = 1
        //example: Monday, Wednesday, Friday class, days = 16 + 4 + 1= 21
        //example: Tuesday, Thursday class, days = 8 + 2 = 10
        public int days
        {
            set
            {
                if (value > 31)
                {
                    Console.WriteLine("WARNING: days value invalid");
                }

                if ((value ^ 0b1) == value - 1)
                {
                    friday = true;
                }

                if ((value ^ 0b10) == value - 2)
                {
                    thursday = true;
                }

                if ((value ^ 0b100) == value - 4)
                {
                    wednesday = true;
                }

                if ((value ^ 0b1000) == value - 8)
                {
                    tuesday = true;
                }

                if ((value ^ 0b10000) == value - 16)
                {
                    monday = true;
                }
            }
        }

        public TimeBlock(int days, string startTime, TimeSpan time)
        {
            this.days = days;
            this.startTime = startTime;
            this.endTime = TimeSpan.Parse(startTime).Add(time).ToString(@"hh\:mm");
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
            this.course = course;
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
            this.course = course;
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

        public Lab(Course course, TimeBlock time, string instructor, Campus campus, string room, int currentStudents, int maxStudents, int currentWaitlist, int maxWaitlist, string note)
        {
            this.course = course;
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
