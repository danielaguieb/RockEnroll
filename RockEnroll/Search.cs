using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockEnroll
{
    public class Search
    {
        public enum Compares
        {
            NONE, EQ, GT, LT, CONTAINS
        }

        public string searchstring = "";
        public Campus campus = Campus.NONE;
        public Terms enrollmentterm = Terms.NONE;
        public Department subjectname = Department.NONE;
        public Faculty faculty = Faculty.NONE;
        public int courseNumber = -1;
        public Compares courseCompare = Compares.NONE;
        public int unitsNumber = -1;
        public Compares unitsCompare = Compares.NONE;
        public bool prereq = false;
        public bool nonConflict = false;
        public bool open = false;
        public bool waitListable = false;
        public bool otherSemester = false;

        public Dictionary<Department, List<Course>> results = new();
        private List<Course> tempcourses;

        public Search(string search)
        {
            this.searchstring = search;
        }

        public Search(string search, Campus campus, Terms enrollmentterm, Department subjectname, Faculty faculty, int courseNumber,
            Compares courseCompare, int unitsNumber, Compares unitsCompare, bool prereq, bool nonConflict, bool open, bool waitListable,
            bool otherSemester)
        {
            this.searchstring = search;
            this.campus = campus;
            this.enrollmentterm = enrollmentterm;
            this.subjectname = subjectname;
            this.faculty = faculty;
            this.courseNumber = courseNumber;
            this.courseCompare = courseCompare;
            this.unitsNumber = unitsNumber;
            this.unitsCompare = unitsCompare;
            this.prereq = prereq;
            this.nonConflict = nonConflict;
            this.open = open;
            this.waitListable = waitListable;
            this.otherSemester = otherSemester;
        }

        public void Reset()
        {
        searchstring = "";
        campus = Campus.NONE;
        enrollmentterm = Terms.NONE;
        subjectname = Department.NONE;
        faculty = Faculty.NONE;
        courseNumber = -1;
        courseCompare = Compares.NONE;
        unitsNumber = -1;
        unitsCompare = Compares.NONE;
        prereq = false;
        nonConflict = false;
        open = false;
        waitListable = false;
        otherSemester = false;
    }

        public void Proccess()
        {
            

            List<Course> complist;
            switch (enrollmentterm)
            {
                case Terms.FALL2021:
                    complist = RockEnrollHelper.fall21Courses;
                    break;

                case Terms.WINTER2022:
                    complist = RockEnrollHelper.winter22Courses;
                    break;

                case Terms.SPRING2022:
                    complist = RockEnrollHelper.spring22Courses;
                    break;

                case Terms.SUMMER2022:
                    complist = RockEnrollHelper.summer22Courses;
                    break;
                default:
                    complist = RockEnrollHelper.allCourses;
                    break;
            }

            if (otherSemester) complist = RockEnrollHelper.allCourses;

            results.Clear();
            foreach (Course i in complist)
            {
                if (searchstring.Length != 0)
                {
                    string compare = String.Format("{0} {1} {2}{3} {2} {3}", i.courseTitle, i.courseDescription, Enum.GetName(typeof(Department), i.department), i.courseID);
                    if (!compare.Contains(searchstring))
                    {
                        continue;
                    }
                }

                if (campus != Campus.NONE)
                {
                    //filter campus

                    /*if (campus != i.campus)
                    {
                        continue;
                    }*/
                }

                if (subjectname != Department.NONE)
                {
                    if (i.department == this.subjectname) continue;
                }

                if (faculty != Faculty.NONE)
                {
                    if (i.faculty == this.faculty) continue;
                }

                if (courseCompare != Compares.NONE)
                {
                    if (courseNumber != -1)
                    {
                        bool coursenummatch = false;
                        switch (courseCompare)
                        {
                            case Compares.CONTAINS:
                                if (i.courseID.ToString().Contains(courseNumber.ToString())) coursenummatch = true;
                                break;
                            case Compares.EQ:
                                if (i.courseID == courseNumber) coursenummatch = true;
                                break;
                            case Compares.LT:
                                if (i.courseID < courseNumber) coursenummatch = true;
                                break;
                            case Compares.GT:
                                if (i.courseID > courseNumber) coursenummatch = true;
                                break;
                        }

                        if (!coursenummatch) continue;
                    }
                }

                if (unitsCompare != Compares.NONE)
                {
                    if (unitsNumber != -1)
                    {
                        bool unitsnummatch = false;
                        switch (unitsCompare)
                        {
                            case Compares.CONTAINS:
                                if (i.courseUnits.ToString().Contains(unitsNumber.ToString())) unitsnummatch = true;
                                break;
                            case Compares.EQ:
                                if (i.courseUnits == unitsNumber) unitsnummatch = true;
                                break;
                            case Compares.LT:
                                if (i.courseUnits < unitsNumber) unitsnummatch = true;
                                break;
                            case Compares.GT:
                                if (i.courseUnits > unitsNumber) unitsnummatch = true;
                                break;
                        }

                        if (!unitsnummatch) continue;
                    }
                }

                if (prereq)
                {

                }

                if (nonConflict)
                {

                }

                if (open)
                {

                }

                if (waitListable)
                {

                }

                if (!results.ContainsKey(i.department)) results.Add(i.department, new List<Course>());

                results[i.department].Add(i);    
            }

            
        }

    }
}
