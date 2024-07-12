using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.GetById;
public class GetTripByIdUseCase
{
    public ResponseTripJson Execute(Guid tripId)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .FirstOrDefault(trip => trip.Id == tripId);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        return new ResponseTripJson
        {
            Id = trip.Id,
            Destination = trip.Destination,
            StartsAt = trip.StartsAt,
            EndsAt = trip.EndsAt                
        };
    }
}
