using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Links.Register;
public class RegisterTripLinkUseCase
{
    public ResponseShortLinkJson Execute(Guid tripId, RequestRegisterLinkJson request)
    {
        Validate(request);

        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .FirstOrDefault(trip => trip.Id == tripId);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        var link = new Link
        {
            TripId = tripId,
            Title = request.Title,
            Url = request.Url
        };

        dbContext.Links.Add(link);
        dbContext.SaveChanges();

        return new ResponseShortLinkJson
        {
            LinkId = link.Id
        };
    }

    private void Validate(RequestRegisterLinkJson request)
    {
        var validator = new RegisterTripLinkValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
