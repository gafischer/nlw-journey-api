using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Participants.Invites;
public class InviteTripValidator : AbstractValidator<RequestInviteTripJson>
{
    public InviteTripValidator()
    {
        RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL);
    }
}
