namespace Application.Features.Songs.Commands;

public sealed record AddSongCommand(string Name, TimeSpan Length, int Rating, int ArtistId) : IRequest<Song>;