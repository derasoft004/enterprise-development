namespace Polyclinic.Domain.Interfaces;

/// <summary>
/// Generic repository interface for basic CRUD operations
/// </summary>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Create new entity
    /// </summary>
    public TKey Create(TEntity entity);

    /// <summary>
    /// Read all entities
    /// </summary>
    public List<TEntity> ReadAll();

    /// <summary>
    /// Read entity by id
    /// </summary>
    public TEntity? Read(TKey id);

    /// <summary>
    /// Update entity by id
    /// </summary>
    public TEntity? Update(TKey id, TEntity entity);

    /// <summary>
    /// Delete entity by id
    /// </summary>
    public bool Delete(TKey id);
}