namespace Zoo_dz1
{
    public abstract class Animal : IAlive, IInventory
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public abstract int Food { get; }
        public bool IsHealthy { get; set; }
        protected Animal(string name)
        {
            Number = 0;
            Name = name;
        }
    }
}
