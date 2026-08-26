using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract void DisplayInfo();
    }
}
