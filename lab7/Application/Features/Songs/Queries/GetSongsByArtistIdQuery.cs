namespace Application.Features.Songs.Queries;

public record GetSongsByArtistIdQuery(int ArtistId) : IRequest<IReadOnlyCollection<Song>>;