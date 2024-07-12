using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Participants.GetTripParticipants;
public class GetTripParticipantsUseCase
{
    public IList<ResponseParticipantJson> Execute(Guid tripId)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .Include(trip => trip.Participants.OrderByDescending(participant => participant.IsOwner))
            .FirstOrDefault(trip => trip.Id == tripId);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        var participants = trip.Participants
            .Select(participant => new ResponseParticipantJson
            {
                Id = participant.Id,
                Name = participant.Name,
                Email = participant.Email,
                IsConfirmed = participant.IsConfirmed
            })
            .ToList();

        return participants;
    }
}
