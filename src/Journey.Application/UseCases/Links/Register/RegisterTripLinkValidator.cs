using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Links.Register;
public class RegisterTripLinkValidator : AbstractValidator<RequestRegisterLinkJson>
{
    public RegisterTripLinkValidator()
    {
        RuleFor(request => request.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_EMPTY);
        RuleFor(request => request.Url).NotEmpty().WithMessage(ResourceErrorMessages.URL_EMPTY);
    }
}
