namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerId> // CustomerId is strongly typed identifier
    {
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public static Customer Create(CustomerId id, string name, string email) // Rich domain 'Entity' implement Create static method
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(email);

            var customer = new Customer
            {
                Id = id,
                Name = name,
                Email = email
            };

            return customer;
        }
    }
}
