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
        /// ���uRepository
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
        public void GetByName_�Ωm�W���o���u���_���uRepository�Q�I�s�@��()
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
        public void Update_��s���u��ƥ��`_�^��true�PUpdate��k����@��()
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
        public void Update_��s���u��Ƶo�Ͳ��`_�^��false()
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