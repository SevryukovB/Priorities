using System;
using System.Collections.Generic;
using System.Text;

namespace Priority.Data.Dto
{
    public class PriorityTaskModifyDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? NextPriorityTask { get; set; }
    }
}
