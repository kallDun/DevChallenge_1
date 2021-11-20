using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Service
{
    public static class CSV_ToListService
    {
        public static List<Portfolio> GetPortfoliosFromFile(string path)
        {
            List<Portfolio> portfolios = new();

            var file = File.ReadAllLines(path);
            for (int i = 1; i < file.Length; i++)
            {
                var line = file[i].Split(',');

                portfolios.Add(new()
                {
                    Id = Guid.NewGuid(),
                    Name = line[0],
                    InstrumentOwner = line[1],
                    Instrument = line[2],
                    Date = new TimeSlot(DateTime.Parse(line[3], CultureInfo.CreateSpecificCulture("fr-FR"))),
                    Price = double.Parse(line[4])
                });
            }
            return portfolios;
        }
    }
}
