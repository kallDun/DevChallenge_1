using SC.DevChallenge.Api.Models;
using System;
using System.Collections.Generic;
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
                portfolios.Add(new()
                {
                    Id = Guid.NewGuid(),
                    Name = line[0].ToString(),
                    InstrumentOwner = line[1].ToString(),
                    Instrument = line[2].ToString(),
                    Date = new TimeSlot(DateTime.Parse(line[3].ToString())),
                    Price = double.Parse(line[4].ToString())
                });
            }
        }

    }
}
