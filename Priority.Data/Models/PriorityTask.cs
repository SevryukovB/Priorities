using Common.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Priority.Data.Models
{
    public class PriorityTask : BaseEntity
    {
        public string Name { get; set; }

        public int? NextPriorityTask { get; set; }
    }
}
