using Microsoft.AspNetCore.Mvc;
using OPAZ_API.Services;

namespace OPAZ_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpazApiController : ControllerBase
    {
       private readonly ILogger<OpazApiController> _logger;

        private FeatureServerService featureServerService;

        public OpazApiController(ILogger<OpazApiController> logger)
        {
            _logger = logger;
            featureServerService = new FeatureServerService();
        }

        [HttpGet(Name = "CollegesUniversities")]
        public Dictionary<string, Dictionary<string, int>> Get(String minX, String minY, String maxX, String maxY)
        {
            Dictionary<string, Dictionary<string, int>> result = new Dictionary<string, Dictionary<string, int>>();


            List<CollegeUniversity> collegeUniversities = featureServerService.GetFeatureServer( minX, minY, maxX, maxY).GetAwaiter().GetResult();           

            Dictionary<string, int> schoolsPerCity = PrepareSchoolsPerCityData(collegeUniversities);
            result.Add("SCHOOLS_PER_CITY", schoolsPerCity);

            Dictionary<string, int> studentsPerCity = PrepareStudentsPerCityData(collegeUniversities);
            result.Add("STUDENTS_PER_CITY", studentsPerCity);

            Dictionary<string, int> schoolsPerState= PrepareSchoolsPerStateData(collegeUniversities);
            result.Add("SCHOOLS_PER_STATE", schoolsPerState);

            Dictionary<string, int> studentsPerState = PrepareStudentsPerStateData(collegeUniversities);
            result.Add("STUDENTS_PER_STATE", studentsPerState);

            return result;
        }


        private Dictionary<string, int> PrepareSchoolsPerCityData(List<CollegeUniversity> collegeUniversities)
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

        private Dictionary<string, int> PrepareStudentsPerCityData(List<CollegeUniversity> collegeUniversities)
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


         private Dictionary<string, int> PrepareSchoolsPerStateData(List<CollegeUniversity> collegeUniversities)
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


        private Dictionary<string, int> PrepareStudentsPerStateData(List<CollegeUniversity> collegeUniversities)
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