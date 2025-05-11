using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class FakeArtistRepository : IRepository<Artist>
{
    private readonly List<Artist> _artists = 
    [
        new Artist("bon jovi", 5)
        {
            Id = 1
        },
        new Artist("linkin park", 5)
        {
            Id = 2
        }
    ];
    
    public Task<Artist> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Artist, object>>[] includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Artist>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Artist>>(_artists);
    }

    public Task<IReadOnlyList<Artist>> ListAsync(Expression<Func<Artist, bool>> filter, CancellationToken cancellationToken = default,
        params Expression<Func<Artist, object>>[] includesProperties)
    {
        
        return Task.FromResult<IReadOnlyList<Artist>>(_artists.Where(filter.Compile()).ToList());
    }

    public Task AddAsync(Artist entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Artist entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Artist entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Artist?> FirstOrDefaultAsync(Expression<Func<Artist, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}