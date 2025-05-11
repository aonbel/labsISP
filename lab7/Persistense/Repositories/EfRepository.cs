using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class EfRepository<T>(ApplicationDbContext context) : IRepository<T>
    where T : BaseEntity
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includesProperties)
    {
        var query = _dbSet.AsQueryable();

        query = includesProperties
            .Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

        query = query.Where(e => e.Id == id);

        if (!(await query.AnyAsync(cancellationToken)))
        {
            throw new ArgumentException("The specified identifier does not exist.");
        }

        return await query.FirstAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includesProperties)
    {
        var query = _dbSet.AsQueryable();

        query = includesProperties
            .Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

        query = query.Where(filter);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Entry(entity).State = EntityState.Deleted;

        return Task.CompletedTask;
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        query = query.Where(filter);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}