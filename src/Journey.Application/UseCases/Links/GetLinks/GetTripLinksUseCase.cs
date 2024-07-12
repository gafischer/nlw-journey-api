using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Links.GetLinks;
public class GetTripLinksUseCase
{
    public IList<ResponseLinkJson> Execute(Guid tripId)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .Include(trip => trip.Links)
            .FirstOrDefault(trip => trip.Id == tripId);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        var links = trip.Links
            .Select(link => new ResponseLinkJson
            {
                Id = link.Id,
                Title = link.Title,
                Url = link.Url
            })
            .ToList();

        return links;
    }
}
