using Application.Features.Songs.Queries;

namespace Application.Features.Songs.Handlers;

public class GetSongsByArtistIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetSongsByArtistIdQuery, IReadOnlyCollection<Song>>
{
    public async Task<IReadOnlyCollection<Song>> Handle(GetSongsByArtistIdQuery request,
        CancellationToken cancellationToken)
    {
        var songs = await unitOfWork.Songs.ListAsync(song => song.ArtistId == request.ArtistId, cancellationToken);

        return songs;
    }
}