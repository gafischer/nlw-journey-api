using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.GetTripActivities;
public class GetTripActivitiesUseCase
{
    public IList<ResponseActivityJson> Execute(Guid tripId)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .Include(trip => trip.Activities.OrderBy(activity => activity.OccursAt))
            .FirstOrDefault(trip => trip.Id == tripId);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        var activities = new List<ResponseActivityJson>();

        for (var currentDate = trip.StartsAt.Date; currentDate <= trip.EndsAt.Date; currentDate = currentDate.AddDays(1))
        {
            var activitiesForDay = trip.Activities
                .Where(a => a.OccursAt.Date == currentDate)
                .Select(a => new ResponseActivityItemJson
                {
                    Id = a.Id,
                    Title = a.Title,
                    OccursAt = a.OccursAt
                })
                .ToList();

            activities.Add(new ResponseActivityJson
            {
                Date = currentDate,
                Activities = activitiesForDay
            });
        }

        return activities;
    }
}
