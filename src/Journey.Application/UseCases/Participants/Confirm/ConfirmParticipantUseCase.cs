using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Participants.Confirm;
public class ConfirmParticipantUseCase
{
    public void Execute(Guid participantId)
    {
        var dbContext = new JourneyDbContext();

        var participant = dbContext.Participants
            .FirstOrDefault(participant => participant.Id == participantId);

        if (participant is null)
        {
            throw new NotFoundException(ResourceErrorMessages.PARTICIPANT_NOT_FOUND);
        }

        participant.IsConfirmed = true;

        dbContext.Update(participant);
        dbContext.SaveChanges();
    }
}
