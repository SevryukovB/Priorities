using Common.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data
{
    public static class ListHelper<T>  where T : BaseEntity
    {
        public static List<T> ContextList;

        static ListHelper()
        {
            ContextList = new List<T>();
        }
    }
}
