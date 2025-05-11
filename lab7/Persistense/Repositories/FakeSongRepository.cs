using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class FakeSongRepository : IRepository<Song>
{
    private readonly List<Song> _songs =
    [
        new Song("Always", new SongData(TimeSpan.FromMinutes(3)), 2, 1)
        {
            Id = 1,
        },
        new Song("In the end", new SongData(TimeSpan.FromMinutes(3)), 1, 2)
        {
            Id = 2,
        },
    ];

    public Task<Song> GetByIdAsync(int id, CancellationToken cancellationToken = default,
        params Expression<Func<Song, object>>[] includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Song>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<Song>>(_songs);
    }

    public Task<IReadOnlyList<Song>> ListAsync(Expression<Func<Song, bool>> filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<Song, object>>[] includesProperties)
    {
        return Task.FromResult<IReadOnlyList<Song>>(_songs.Where(filter.Compile()).ToList());
    }

    public Task AddAsync(Song entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Song entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Song entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Song?> FirstOrDefaultAsync(Expression<Func<Song, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}