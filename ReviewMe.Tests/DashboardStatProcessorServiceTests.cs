using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewMe.DAL;
using ReviewMe.Exceptions;
using ReviewMe.Services;

namespace ReviewMe.Tests
{
    [TestClass]
    public class DashboardStatProcessorServiceTests
    {
        [TestInitialize]
        public void Setup()
        {
            InitializeDB.Init();
        }

        IDashboardStatProcessorService CreateNewService()
        {
            return new DashboardStatProcessorService(new DashboardStatProcessorDAL(new ApplicationDbContext()));
        }

        [TestMethod]
        public async Task AddHumanVisitorsTestMethod()
        {
            var service = CreateNewService();
            await service.AddHumanVisitors("Petya", 1);
            var count = await service.GetVisitorsCount("Petya");

            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void GetVisitorsExceptionTestMethod()
        {
            var service = CreateNewService();

            Assert.ThrowsExceptionAsync<VisitorNotFoundException>(async () => { await service.GetVisitorsCount("Petya"); });
        }

        [TestMethod]
        public async Task CheckCombineAllMethodsTestMethod()
        {
            var service = CreateNewService();
            await service.AddHumanVisitors("Petya", 1);
            await service.DeleteVisitorsCount("Petya");
            var count = await service.GetVisitorsCount("Petya");

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public async Task MultiThreadAddUsersTestMethod()
        {
            var service = CreateNewService();

            var threads = Enumerable.Range(0, 100).Select((x) => new Thread(
                  () =>
                  {
                      service.AddHumanVisitors("Petya", 1).ConfigureAwait(false).GetAwaiter().GetResult();
                  }
                 )).ToArray();

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            var count = await service.GetVisitorsCount("Petya");

            Assert.AreEqual(100, count);

        }
    }
}
