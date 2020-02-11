using Common.Database.Services;
using Priority.Data.Dto;
using Priority.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Priority.Abstractions.Services
{
    public interface IPriorityTaskService : IEntityService<PriorityTask>
    {
        List<PriorityTaskViewDto> GetAllWithNumber();
    }
}
