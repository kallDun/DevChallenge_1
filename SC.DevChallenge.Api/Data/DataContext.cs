using SC.DevChallenge.Api.Service;
using System.IO;
using System.Reflection;

namespace SC.DevChallenge.Api.Data
{
    public class DataContext : AbstractDataContext
    {

        public DataContext()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Input/data.csv");
            portfolios = CSV_ToListService.GetPortfoliosFromFile(path);
        }

    }
}
