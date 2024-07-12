using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Confirm;
public class ConfirmTripUseCase
{
    public void Execute(Guid tripId)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .Include(trip => trip.Participants.Where(participant => !participant.IsOwner))
            .FirstOrDefault(trip => trip.Id == tripId);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        if (trip.IsConfirmed)
        {
            //TODO: Talvez retornar BadRequest avisando que a viagem já está confirmada e os emails nao serão enviados
            return;
        }

        trip.IsConfirmed = true;

        dbContext.Trips.Update(trip);
        dbContext.SaveChanges();

        //TODO: Enviar email para todos os participantes confirmarem presença na viagem
    }
}
