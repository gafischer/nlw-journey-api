using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Participants.Invites;
public class InviteTripUseCase
{
    public ResponseShortParticipantJson Execute(Guid tripId, RequestInviteTripJson request)
    {

        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .Include(trip => trip.Participants)
            .FirstOrDefault(trip => trip.Id == tripId);

        Validate(trip, request);

        var participant = new Participant
        {
            TripId = trip!.Id,
            Email = request.Email
        };

        dbContext.Participants.Add(participant);
        dbContext.SaveChanges();

        //TODO: Enviar email para o participante confirmar presença na viagem

        return new ResponseShortParticipantJson
        {
            ParticipantId = participant.Id
        };
    }

    private void Validate(Trip? trip, RequestInviteTripJson request)
    {
        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        var validator = new InviteTripValidator();

        var result = validator.Validate(request);

        var existingParticipant = trip.Participants.Any(participant => participant.Email == request.Email);

        if (existingParticipant)
        {
            result.Errors.Add(new ValidationFailure("ExistingEmail", ResourceErrorMessages.PARTICIPANT_EMAIL_ALREADY_EXISTS_IN_TRIP));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
