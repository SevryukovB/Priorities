using Common.Data;
using Common.Database.Data;
using Common.Database.Models;
using Common.Database.Services;
using Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Priority.Abstractions.Services;
using Priority.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Priority.Test
{
    public class TestEnvironment
    {
        public IServiceCollection Services { get; }
        public ServiceProvider ServiceProvider { get; private set; }

        public void Reset()
        {
            ServiceProvider?.Dispose();
            ServiceProvider = Services.BuildServiceProvider();
        }

        public TestEnvironment()
        {
            Services = new ServiceCollection();
            AddServices();
        }

        private void AddServices()
        {
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddSingleton(typeof(IPriorityTaskService), typeof(PriorityTaskService));
            Services.AddScoped(typeof(IRepository<>), typeof(ListRepository<>));
            Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
        }
    }
}
