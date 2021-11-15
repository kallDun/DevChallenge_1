using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace SC.DevChallenge.Api.Data
{
    public class DataContext
    {
        private List<Portfolio> portfolios;
        public List<Portfolio> Portfolios => portfolios;

        public DataContext()
        {


            portfolios = new();

            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Input/data.csv");
            var file = File.ReadAllLines(path);
            for (int i = 1; i < file.Length; i++)
            {
                var line = file[i].Split(',');
                var datetime = DateTime.Parse(line[3], CultureInfo.CreateSpecificCulture("fr-FR"));

                portfolios.Add(new()
                {
                    Id = Guid.NewGuid(),
                    Name = line[0],
                    InstrumentOwner = line[1],
                    Instrument = line[2],
                    Date = new TimeSlot(datetime),
                    Price = double.Parse(line[4])
                });
            }
        }

    }
}
