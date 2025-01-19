namespace s1
{

    public class CustomerStorage
    {
        private readonly List<Customer> _customers = new();

        public void AddCustomer(Customer customer) => _customers.Add(customer);

        public IEnumerable<Customer> GetCustomers() => _customers;

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _customers);
        }
    }
}