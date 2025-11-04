namespace Ordering.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T> ///where T : class -> ako hocemo da T bude referenti tip (npr. klasa) a ne value type (npr. int, bool)
    {
        public T Id { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
    }

}
