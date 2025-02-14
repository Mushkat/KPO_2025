namespace Zoo_dz1
{
public class Table : Thing
    {
        public string Material { get; } // Материал стола

        public Table(int number, string name, string material)
            : base(number, name)
        {
            Material = material;
        }
    }
}
