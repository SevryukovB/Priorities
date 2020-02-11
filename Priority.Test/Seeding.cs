using Common.Data;
using Priority.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Priority.Test
{
    public static class Seeding
    {
        public static void Seed()
        {
            ListHelper<PriorityTask>.ContextList.Add(
                new PriorityTask 
                {
                    Id = 0,
                    Name = "Ivan1",
                    NextPriorityTask = 1
                }
            );

            ListHelper<PriorityTask>.ContextList.Add(
                new PriorityTask
                {
                    Id = 1,
                    Name = "Ivan2",
                    NextPriorityTask = null
                }
            ); 
        }
    }
}
