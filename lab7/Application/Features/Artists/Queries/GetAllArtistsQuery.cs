namespace Application.Features.Artists.Queries;

public record GetAllArtistsQuery() : IRequest<IReadOnlyList<Artist>>;