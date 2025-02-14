namespace Zoo_dz1
{
    public class VeterinaryClinic : IVeterinaryClinic
    {
        public bool CheckHealth(Animal animal)
        {
            int x = new Random().Next(0, 2);

            bool healthy = x == 1;
            animal.IsHealthy = healthy;
            return healthy;
        }
    }
}
