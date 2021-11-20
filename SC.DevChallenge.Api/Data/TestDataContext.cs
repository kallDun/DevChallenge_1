using SC.DevChallenge.Api.Service;
using System.IO;
using System.Reflection;

namespace SC.DevChallenge.Api.Data
{
    public class TestDataContext : AbstractDataContext
    {
        public TestDataContext()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Input/my_data_for_test.csv");
            portfolios = CSV_ToListService.GetPortfoliosFromFile(path);
        }
    }
}
