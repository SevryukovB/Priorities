using Common.Database.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Priority.Abstractions.Services;
using Priority.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Priority.Test
{
    [TestFixture]
    public class PriorityTest
    {
        private readonly TestEnvironment _environment;
        private IPriorityTaskService _priorityTaskService => GetService<IPriorityTaskService>();

        public PriorityTest()
        {
            _environment = new TestEnvironment();
        }

        [SetUp]
        public void Setup()
        {
            _environment.Reset();
            Seeding.Seed();
        }

        private T GetService<T>()
        {
            return _environment.ServiceProvider.GetRequiredService<T>();
        }

        [Test]
        public void Created()
        {
            var priority = new PriorityTask
            {
                Name = "Ivan3"
            };
            var result = _priorityTaskService.Create(priority);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Deleted()
        {
            _priorityTaskService.Delete(1);
            var deletedEntity = _priorityTaskService.FindById(1);
            Assert.IsNull(deletedEntity);
        }

        [Test]
        public void Updated()
        {
            var priority = new PriorityTask
            {
                Id = 1,
                NextPriorityTask = 0
            };

            var valueBeforeUpdate = _priorityTaskService.GetById(priority.Id);
            _priorityTaskService.Update(priority);
            var valueAfterUpdate = _priorityTaskService.GetById(priority.Id);

            Assert.IsTrue(valueBeforeUpdate.NextPriorityTask != valueAfterUpdate.NextPriorityTask);
        }
    }
}
