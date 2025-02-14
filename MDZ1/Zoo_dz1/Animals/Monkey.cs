namespace Zoo_dz1
{
    public class Monkey : Herbo
    {
        public Monkey(string name, int kindness) : base(name, kindness) { }
        public override int Food => 2;
    }
}
