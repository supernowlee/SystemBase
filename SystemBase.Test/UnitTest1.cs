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
        /// ���u Repository
        /// </summary>
        private IRepository<Staff, int> Repository;

        /// <summary>
        /// ���u�A��
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