using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SystemBase.Repository.Interfaces;
using SystemBase.Repository.Models;
using SystemBase.Service.Services;

namespace SystemBase.Test
{
    [TestFixture]
    public class StaffServiceTest
    {
        /// <summary>
        /// 員工Repository
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
        public void GetByName_用姓名取得員工資料_員工Repository被呼叫一次()
        {
            // Arrange
            var staff = new List<Staff>();
            this.Repository.Find(Arg.Any<Expression<Func<Staff, bool>>>()).Returns(staff);

            // Act
            this.Service.GetByName("snow");

            // Assert
            this.Repository.Received(1).Find(Arg.Any<Expression<Func<Staff, bool>>>());
        }

        [Test]
        public void Update_更新員工資料正常_回傳true與Update方法執行一次()
        {
            // Arrange
            var staff = new Staff();
            
            // Act
            var actual = this.Service.Update(staff);

            // Assert
            Assert.IsTrue(actual);
            this.Repository.Received(1).Update(Arg.Any<Staff>());
        }

        [Test]
        public void Update_更新員工資料發生異常_回傳false()
        {
            // Arrange
            var staff = new Staff();
            this.Repository.When(fake => fake.Update(Arg.Any<Staff>())).Do(call => { throw new Exception("Any Exception"); });
            
            // Act
            var actual = this.Service.Update(staff);

            // Assert
            Assert.IsFalse(actual);
        }
    }
}