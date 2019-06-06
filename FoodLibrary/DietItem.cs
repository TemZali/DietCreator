using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary
{
    public class DietItem
    {
        public DietItem(int id, string info)
        {
            Id = id;
            Info = info;
        }

        public int Id { get; set; }

        public string Info { get; set; }
    }
}
