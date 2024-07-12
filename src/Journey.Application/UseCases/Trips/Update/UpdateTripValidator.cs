using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Trips.Update
{
    public class UpdateTripValidator : AbstractValidator<RequestUpdateTripJson>
    {
        public UpdateTripValidator()
        {
            RuleFor(request => request.Destination).NotEmpty().WithMessage(ResourceErrorMessages.DESTINATION_EMPTY);

            RuleFor(request => request.StartsAt.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);

            RuleFor(request => request)
                .Must(request => request.EndsAt.Date >= request.StartsAt.Date)
                .WithMessage(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
        }
    }
}
