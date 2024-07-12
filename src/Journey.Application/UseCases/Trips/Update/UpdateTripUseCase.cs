using Journey.Communication.Requests;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.Update;
public class UpdateTripUseCase
{
    public void Execute(Guid tripId, RequestUpdateTripJson request)
    {
        Validate(request);

        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .FirstOrDefault(trip => trip.Id == tripId);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        trip.Destination = request.Destination;
        trip.StartsAt = request.StartsAt;
        trip.EndsAt = request.EndsAt;

        dbContext.Update(trip);
        dbContext.SaveChanges();
    }

    private void Validate(RequestUpdateTripJson request)
    {
        var validator = new UpdateTripValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
