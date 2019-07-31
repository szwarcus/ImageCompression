using Microsoft.Extensions.Configuration;

namespace ImageCompression.Helpers
{
    public static class ProjectConfigurationHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            string result = "";


            result = configuration.GetConnectionString("DefaultDbConnectionString");


           return result;
        }
    }
}