using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register;
public class RegisterTripUseCase
{
    public ResponseShortTripJson Execute(RequestRegisterTripJson request)
    {
        Validate(request);

        var dbContext = new JourneyDbContext();

        var trip = new Trip
        {
            Destination = request.Destination,
            StartsAt = request.StartsAt,
            EndsAt = request.EndsAt,
            Participants = [
                new Participant
                {
                    Name = request.OwnerName,
                    Email = request.OwnerEmail,
                    IsOwner = true,
                    IsConfirmed = true
                }
            ]
        };

        foreach (var email in request.EmailsToInvite)
        {
            var participant = new Participant
            {
                Email = email,
                IsOwner = false,
                IsConfirmed = false,
                Trip = trip
            };

            trip.Participants.Add(participant);
        }

        dbContext.Trips.Add(trip);
        dbContext.SaveChanges();

        //TODO: Enviar email para OwnerEmail solicitando confirmação da viagem

        return new ResponseShortTripJson
        {
            TripId = trip.Id
        };
    }

    private void Validate(RequestRegisterTripJson request)
    {
        var validator = new RegisterTripValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
