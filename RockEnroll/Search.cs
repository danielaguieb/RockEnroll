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
        public string subjectname;
        public int courseNumber = -1;
        public Compares courseCompare = Compares.NONE;
        public int unitsNumber = -1;
        public Compares unitsCompare = Compares.NONE;
        public bool prereq;
        public bool nonConflict;
        public bool open;
        public bool waitListable;
        public bool otherSemester;

        public Search(string search)
        {
            this.searchstring = search;
        }

        public Search(string search, Campus campus, RockEnrollHelper.Terms enrollmentterm, string subjectname, int courseNumber,
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

        public List<Course> Proccess()
        {
            List<Course> results = new();

            if (searchstring.Length != 0)
            {
                // string search
            }

            if (campus != Campus.NONE)
            {
                //filter campus
            }

            if (enrollmentterm != RockEnrollHelper.Terms.NONE)
            {
                //filter session
            }

            if (subjectname.Length != 0)
            {

            }

            if (courseNumber != -1)
            {

            }

            if (courseCompare != Compares.NONE)
            {

            }

            if (unitsNumber != -1)
            {

            }

            if (unitsCompare != Compares.NONE)
            {

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

            if (otherSemester)
            {

            }

            return results;
        }

    }
}
