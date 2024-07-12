using Journey.Application.UseCases.Activities.GetTripActivities;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Links.GetLinks;
using Journey.Application.UseCases.Links.Register;
using Journey.Application.UseCases.Participants.GetTripParticipants;
using Journey.Application.UseCases.Participants.Invites;
using Journey.Application.UseCases.Trips.Confirm;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Application.UseCases.Trips.Update;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpGet]
    [Route("{tripId}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid tripId)
    {
        var useCase = new GetTripByIdUseCase();

        var response = useCase.Execute(tripId);

        return Ok(response);
    }    

    [HttpGet]
    [Route("{tripId}/Participants")]
    [ProducesResponseType(typeof(IList<ResponseParticipantJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetTripParticipants([FromRoute] Guid tripId)
    {
        var useCase = new GetTripParticipantsUseCase();

        var response = useCase.Execute(tripId);

        return Ok(response);
    }

    [HttpGet]
    [Route("{tripId}/Activities")]
    [ProducesResponseType(typeof(IList<ResponseActivityJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetTripActivities([FromRoute] Guid tripId)
    {
        var useCase = new GetTripActivitiesUseCase();

        var response = useCase.Execute(tripId);

        return Ok(response);
    }

    [HttpGet]
    [Route("{tripId}/Links")]
    [ProducesResponseType(typeof(IList<ResponseLinkJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetTripLinks([FromRoute] Guid tripId)
    {
        var useCase = new GetTripLinksUseCase();

        var response = useCase.Execute(tripId);

        return Ok(response);
    }

    [HttpPut]
    [Route("{tripId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute] Guid tripId, [FromBody] RequestUpdateTripJson request)
    {
        var useCase = new UpdateTripUseCase();

        useCase.Execute(tripId, request);

        return NoContent();
    }

    [HttpPost]
    [Route("{tripId}/Confirm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Confirm([FromRoute] Guid tripId)
    {
        var useCase = new ConfirmTripUseCase();

        useCase.Execute(tripId);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        var useCase = new RegisterTripUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpPost]
    [Route("{tripId}/Invites")]
    [ProducesResponseType(typeof(ResponseShortParticipantJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Invites([FromRoute] Guid tripId, [FromBody] RequestInviteTripJson request)
    {
        var useCase = new InviteTripUseCase();

        var response = useCase.Execute(tripId, request);

        return Created(string.Empty, response);
    }

    [HttpPost]
    [Route("{tripId}/Activities")]
    [ProducesResponseType(typeof(ResponseShortActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult RegisterTripActivitiy([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request)
    {
        var useCase = new RegisterTripActivityUseCase();

        var response = useCase.Execute(tripId, request);

        return Created(string.Empty, response);
    }

    [HttpPost]
    [Route("{tripId}/Links")]
    [ProducesResponseType(typeof(ResponseShortLinkJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult RegisterTripLink([FromRoute] Guid tripId, [FromBody] RequestRegisterLinkJson request)
    {
        var useCase = new RegisterTripLinkUseCase();

        var response = useCase.Execute(tripId, request);

        return Created(string.Empty, response);
    }
}
