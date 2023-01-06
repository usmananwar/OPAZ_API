using System.Text.Json.Nodes;

namespace OPAZ_API.Services
{
    public class FeatureServerService
    {
        private static string url = "https://services.arcgis.com/V6ZHFr6zdgNZuVG0/arcgis/rest/services/CollegesUniversities/FeatureServer/0/";
        
        //private static string queryString = "query?f=pjson&where=1=1&maxRecordCountFactor=4&outFields=NAME%2C+FID%2C+TOT_ENROLL%2C+CITY%2C+STATE%2C+COUNTRY";


        private static string queryStringTemplate = "query?f=pjson&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&geometry={0},{1},{2},{3}&maxRecordCountFactor=4&outFields=NAME%2C+FID%2C+TOT_ENROLL%2C+CITY%2C+STATE%2C+COUNTRY";

        private HttpClient client = new HttpClient();

        public async Task<List<CollegeUniversity>> GetFeatureServer(String minX, String minY, String maxX, String maxY)
        {

            String queryString = String.Format(queryStringTemplate, minX, minY, maxY, maxY);

            var content = await client.GetStringAsync(url + queryString);
            JsonNode forecastNode = JsonNode.Parse(content)!;
            JsonArray features = (JsonArray)forecastNode!["features"]!;
            Console.WriteLine(features.Count);

            List<CollegeUniversity> collegeUniversities = new List<CollegeUniversity>();

            foreach (var feature in features)
            {
                JsonObject attributes = (JsonObject)feature!["attributes"]!;
                JsonObject geometry = (JsonObject)feature!["geometry"]!;

                CollegeUniversity cu = new CollegeUniversity();
                cu.Fid = ((int)attributes!["FID"]!);
                cu.Name = ((String)attributes!["NAME"]!);
                cu.City = ((String)attributes!["CITY"]!);
                cu.State = ((String)attributes!["STATE"]!);
                cu.TotalEnroll = ((int)attributes!["TOT_ENROLL"]!);
                collegeUniversities.Add(cu);
            }


            return collegeUniversities;
        }
    }

    
}
