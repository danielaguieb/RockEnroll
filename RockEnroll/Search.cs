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

        public string searchstring;
        public Campus campus;
        public RockEnrollHelper.Terms enrollmentterm;
        public Department subjectname;
        public int courseNumber = -1;
        public Compares courseCompare = Compares.NONE;
        public int unitsNumber = -1;
        public Compares unitsCompare = Compares.NONE;
        public bool prereq;
        public bool nonConflict;
        public bool open;
        public bool waitListable;
        public bool otherSemester;

        public List<Course> results = new();

        public Search(string search)
        {
            this.searchstring = search;
        }

        public Search(string search, Campus campus, RockEnrollHelper.Terms enrollmentterm, Department subjectname, int courseNumber,
            Compares courseCompare, int unitsNumber, Compares unitsCompare, bool prereq, bool nonConflict, bool open, bool waitListable,
            bool otherSemester)
        {
            this.searchstring = search;
            this.campus = campus;
            this.enrollmentterm = enrollmentterm;
            this.subjectname = subjectname;
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

        public void Proccess()
        {
            

            List<Course> complist;
            switch (enrollmentterm)
            {
                case RockEnrollHelper.Terms.FALL2021:
                    complist = RockEnrollHelper.fall21Courses;
                    break;

                case RockEnrollHelper.Terms.WINTER2022:
                    complist = RockEnrollHelper.winter22Courses;
                    break;

                case RockEnrollHelper.Terms.SPRING2022:
                    complist = RockEnrollHelper.spring22Courses;
                    break;

                case RockEnrollHelper.Terms.SUMMER2022:
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

                results.Add(i);        
            }

            
        }

    }
}
