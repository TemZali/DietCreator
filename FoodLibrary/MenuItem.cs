using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FoodLibrary
{
    public class MenuItem
    {
        public MenuItem(string icon, string name)
        {
            Icon = icon;
            Name = name;
        }

        public MenuItem()
        {

        }

        public string Icon { get; set; }

        public string Name { get; set; }
    }
}
