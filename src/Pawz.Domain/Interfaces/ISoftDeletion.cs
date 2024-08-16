using System;

namespace Pawz.Domain.Interfaces
{
    /// <summary>
    /// Interface representing an entity that supports soft deletion.
    /// </summary>
    public interface ISoftDeletion
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is soft-deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was soft-deleted.
        /// If the entity is not deleted, this value is null.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }

        /// <summary>
        /// Restores the entity by undoing the soft deletion.
        /// This method sets <see cref="IsDeleted"/> to false and clears the <see cref="DeletedAt"/> timestamp.
        /// </summary>
        public void Undo()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
