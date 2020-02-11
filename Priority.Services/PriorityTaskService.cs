using Common.Database.Data;
using Common.Services;
using Priority.Abstractions.Services;
using Priority.Data.Dto;
using Priority.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Priority.Services
{
    public class PriorityTaskService : EntityService<PriorityTask>, IPriorityTaskService
    {
        public PriorityTaskService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override PriorityTask Create(PriorityTask entity)
        {
            var lastPriority = Repository.FirstOrDefault(x => x.NextPriorityTask == null);

            entity.Id = Repository.Count() + 1;
            var createdEntity = base.Create(entity);

            if (lastPriority != null)
                lastPriority.NextPriorityTask = createdEntity.Id;

            UnitOfWork.SaveChanges();
            return createdEntity;
        }

        public override void Update(PriorityTask entity)
        {
            var dbPriorityTask = GetById(entity.Id);

            if(dbPriorityTask.NextPriorityTask != entity.NextPriorityTask)
            {
                var previousTask = Repository.FirstOrDefault(x => x.NextPriorityTask == dbPriorityTask.Id);
                var nextTask = Repository.FirstOrDefault(x => x.Id == dbPriorityTask.NextPriorityTask);
                if(previousTask != null)
                    previousTask.NextPriorityTask = nextTask?.Id;

                var newNextTask = Repository.FirstOrDefault(x => x.Id == entity.NextPriorityTask);
                var newPreviousTask = Repository.FirstOrDefault(x => x.NextPriorityTask == newNextTask?.Id);

                if(newPreviousTask != null)
                    newPreviousTask.NextPriorityTask = entity.Id;
            }

            base.Update(entity);
        }

        public override void Delete(int entityId)
        {
            var deletableTask = GetById(entityId);

            base.Delete(entityId);

            var previousTask = Repository.FirstOrDefault(x => x.NextPriorityTask == deletableTask.Id);
            var nextTask = Repository.FirstOrDefault(x => x.Id == deletableTask.NextPriorityTask);
            previousTask.NextPriorityTask = nextTask?.Id;

            UnitOfWork.SaveChanges();
        }

        public List<PriorityTaskViewDto> GetAllWithNumber()
        {
            var toDisplay = new List<PriorityTaskViewDto>();
            var list = GetAll().OrderByDescending(x => x.NextPriorityTask.HasValue).
                    ThenBy(x => x.NextPriorityTask).ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                toDisplay.Add(new PriorityTaskViewDto
                {
                    Name = list[i].Name,
                    PriorityNumber = i
                }) ;
            }

            return toDisplay;
        }
    }
}
