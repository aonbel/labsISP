namespace Application.Features.Artists.Commands;

public sealed record AddArtistCommand(string Name, int Age) : IRequest<Artist>;