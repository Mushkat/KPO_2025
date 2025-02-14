namespace Zoo_dz1
{
    public class Rabbit : Herbo
    {
        public Rabbit(string name, int kindness) : base(name, kindness) { }
        public override int Food => 1;
    }
}
