using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.DevChallenge.Api.Data
{
    public static class DataContextSingleton
    {
        private static DataContext dataContext;

        public static DataContext GetInstance()
        {
            if (dataContext is null) dataContext = new();
            return dataContext;
        }
    }
}
