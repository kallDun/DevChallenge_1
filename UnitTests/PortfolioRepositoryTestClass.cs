using Microsoft.VisualStudio.TestTools.UnitTesting;
using SC.DevChallenge.Api.Data;
using SC.DevChallenge.Api.Dto;
using SC.DevChallenge.Api.Models;
using SC.DevChallenge.Api.Repository;
using System;
using System.Globalization;

namespace UnitTests
{
    [TestClass]
    public class PortfolioRepositoryTestClass
    {
        private IPortfolioRepository repository;
        public PortfolioRepositoryTestClass()
        {
            var test_data_context = new TestDataContext();
            repository = new PortfolioRepository(test_data_context);
        }


        [TestMethod]
        public void TestPlainAverage()
        {
            var dto_1 = new AveragePricePortfolioDto { Name = "Fannie Mae" };
            var selection_1 = repository.GetSelection(dto_1);
            var average_1 = repository.GetAverageCostBySelection(selection_1);

            Assert.AreEqual(string.Format("{0:0.00}", average_1), "218.39");


            var dto_2 = new AveragePricePortfolioDto { Name = "State Bank of India", Instrument = "CDS" };
            var selection_2 = repository.GetSelection(dto_2);
            var average_2 = repository.GetAverageCostBySelection(selection_2);

            Assert.AreEqual(string.Format("{0:0.00}", average_2), "124.23");


            var dto_3 = new AveragePricePortfolioDto { Name = "UOB" };
            var selection_3 = repository.GetSelection(dto_3);
            var average_3 = repository.GetAverageCostBySelection(selection_3);

            Assert.AreEqual(string.Format("{0:0.00}", average_3), "468.56");
        }

        [TestMethod]
        public void TestFindQuartilesSelection()
        {
            var (portfolio_1, date_1) = ("Fannie Mae", "19/06/2018 22:42:53");
            var dto_1 = new BenchmarkPortfolioDto { Name = portfolio_1, Date = new(DateTime.Parse(date_1, CultureInfo.CreateSpecificCulture("fr-FR"))) };
            var selection_1 = repository.GetSelection(dto_1);
            var price_1 = string.Format("{0:0.00}", repository.GetAverageCostBySelection(selection_1));

            Assert.AreEqual(price_1, "344.95");


            var (portfolio_2, date_2) = ("Bank of America", "17/08/2018 19:42:02");
            var dto_2 = new BenchmarkPortfolioDto { Name = portfolio_2, Date = new(DateTime.Parse(date_2, CultureInfo.CreateSpecificCulture("fr-FR"))) };
            var selection_2 = repository.GetSelection(dto_2);
            var price_2 = string.Format("{0:0.00}", repository.GetAverageCostBySelection(selection_2));

            Assert.AreEqual(price_2, "46.33");
        }
    }
}
