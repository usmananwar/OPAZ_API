namespace OPAZ_API.Services
{
    public class DataGenerator
    {


        public Dictionary<string, int> PrepareSchoolsPerCityData(List<CollegeUniversity> collegeUniversities)
        {
            Dictionary<string, int> schoolsPerCity = new Dictionary<string, int>();
            foreach (var item in collegeUniversities)
            {
                if (schoolsPerCity.ContainsKey(item.City!))
                {
                    schoolsPerCity[item.City!] = schoolsPerCity[item.City!] + 1;
                }
                else
                {
                    schoolsPerCity[item.City!] = 1;
                }

            }
            return schoolsPerCity;
        }

        public Dictionary<string, int> PrepareStudentsPerCityData(List<CollegeUniversity> collegeUniversities)
        {
            Dictionary<string, int> studentsPerCity = new Dictionary<string, int>();
            foreach (var item in collegeUniversities)
            {
                if (studentsPerCity.ContainsKey(item.City!))
                {
                    studentsPerCity[item.City!] = studentsPerCity[item.City!] + (int)item.TotalEnroll!;
                }
                else
                {
                    studentsPerCity[item.City!] = (int)item.TotalEnroll!;
                }

            }
            return studentsPerCity;
        }


        public Dictionary<string, int> PrepareSchoolsPerStateData(List<CollegeUniversity> collegeUniversities)
        {
            Dictionary<string, int> schoolsPerState = new Dictionary<string, int>();
            foreach (var item in collegeUniversities)
            {
                if (schoolsPerState.ContainsKey(item.State!))
                {
                    schoolsPerState[item.State!] = schoolsPerState[item.State!] + 1;
                }
                else
                {
                    schoolsPerState[item.State!] = 1;
                }

            }
            return schoolsPerState;
        }


        public Dictionary<string, int> PrepareStudentsPerStateData(List<CollegeUniversity> collegeUniversities)
        {
            Dictionary<string, int> studentsPerState = new Dictionary<string, int>();
            foreach (var item in collegeUniversities)
            {
                if (studentsPerState.ContainsKey(item.State!))
                {
                    studentsPerState[item.State!] = studentsPerState[item.State!] + (int)item.TotalEnroll!;
                }
                else
                {
                    studentsPerState[item.State!] = (int)item.TotalEnroll!;
                }

            }
            return studentsPerState;
        }
    }
}
