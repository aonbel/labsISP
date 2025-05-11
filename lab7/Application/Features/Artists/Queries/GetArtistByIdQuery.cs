namespace Application.Features.Artists.Queries;

public record GetArtistByIdQuery(int Id) : IRequest<Artist>;