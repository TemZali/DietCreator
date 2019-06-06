using System;
using System.Collections.Generic;
using System.Text;

namespace FoodLibrary
{
    public class LinkItem
    {
        public LinkItem(string name, string icon, string link)
        {
            Name = name;
            Icon = icon;
            Link = link;
        }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string Link { get; set; }
    }
}
