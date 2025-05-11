namespace Application.Features.Songs.Commands;

public record UpdateSongCommand(int Id, string Name, TimeSpan Length, int Rating, int ArtistId) : IRequest<Song>;