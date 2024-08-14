namespace Pawz.Domain.Interfaces
{
    /// <summary>
    /// Represents a generic entity with an identifier of type <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the entity's unique identifier.</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        TKey Id { get; set; }
    }
}
