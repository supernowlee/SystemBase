using NSubstitute;
using NUnit.Framework;
using SystemBase.Repository.Interfaces;
using SystemBase.Repository.Models;
using SystemBase.Service.Services;

namespace SystemBase.Test
{
    [TestFixture]
    public class Tests
    {
        /// <summary>
        /// 員工 Repository
        /// </summary>
        private IRepository<Staff, int> Repository;

        /// <summary>
        /// 員工服務
        /// </summary>
        private StaffService Service;

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.Repository = Substitute.For<IRepository<Staff, int>>();

            this.Service = new StaffService(this.Repository);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}