﻿namespace Zoo_dz1
{
    public class Thing : IInventory
    {
        public int Number { get; set; }
        public string Name { get; set; }

        public Thing(int number, string name)
        {
            Number = number;
            Name = name;
        }
    }
}
