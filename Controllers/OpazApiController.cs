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

        private DataGenerator dataGenerator;

        public OpazApiController(ILogger<OpazApiController> logger)
        {
            _logger = logger;
            featureServerService = new FeatureServerService();
            dataGenerator = new DataGenerator();
        }

        [HttpGet(Name = "CollegesUniversities")]
        public Dictionary<string, Dictionary<string, int>> Get(String minX, String minY, String maxX, String maxY)
        {
            Dictionary<string, Dictionary<string, int>> result = new Dictionary<string, Dictionary<string, int>>();

            List<CollegeUniversity> collegeUniversities = featureServerService.GetFeatureServer( minX, minY, maxX, maxY).GetAwaiter().GetResult();           

            Dictionary<string, int> schoolsPerCity = dataGenerator.PrepareSchoolsPerCityData(collegeUniversities);
            result.Add("SCHOOLS_PER_CITY", schoolsPerCity);

            Dictionary<string, int> studentsPerCity = dataGenerator.PrepareStudentsPerCityData(collegeUniversities);
            result.Add("STUDENTS_PER_CITY", studentsPerCity);

            Dictionary<string, int> schoolsPerState= dataGenerator.PrepareSchoolsPerStateData(collegeUniversities);
            result.Add("SCHOOLS_PER_STATE", schoolsPerState);

            Dictionary<string, int> studentsPerState = dataGenerator.PrepareStudentsPerStateData(collegeUniversities);
            result.Add("STUDENTS_PER_STATE", studentsPerState);

            return result;
        }



    }
}