using Microsoft.AspNetCore.Mvc;
using RSI_REST_SERVER.Models;
using RSI_REST_SERVER.Services;

namespace RSI_REST_SERVER.Controllers;
[Route("[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventSrv _eventSrv;
    public EventController(IEventSrv eventSrv)
    {
        _eventSrv = eventSrv;
    }

    [HttpGet("EventsForDay")]
    public IActionResult GetEventsForDay([FromQuery] DateTime date)
    {
        return Ok(_eventSrv.GetEventsForDay(date));
    }
    [HttpGet("EventsForWeek")]
    public IActionResult GetEventsForWeek([FromQuery] DateTime date)
    {
        return Ok(_eventSrv.GetEventsForWeek(date));
    }

    [HttpGet("Events/{eventId}/Information")]
    public IActionResult GetEventInformation(int eventId)
    {
        return Ok(_eventSrv.GetEventInformation(eventId));
    }
    [HttpPost("AddEvent")]
    public IActionResult AddEventFromBody([FromBody] EventDto eventDto)
    {
        return Ok(_eventSrv.AddEvent(eventDto.Name, eventDto.Type, eventDto.Date.AddDays(1), eventDto.Description));
    }

    [HttpPut("ModifyEventInformation/{eventId}")]
    public IActionResult ModifyEventInformation(int eventId, [FromBody] EventDto eventDto)
    {
        _eventSrv.ModifyEventInformation(eventId, eventDto.Name, eventDto.Type, eventDto.Date.AddDays(1), eventDto.Description);
        return Ok();
    }

    [HttpDelete("{eventId}")]
    public IActionResult DeleteEvent(int eventId)
    {
        _eventSrv.RemoveEvent(eventId);
        return Ok();
    }

    [HttpGet("Events")]
    public IActionResult GetAllEvents()
    {
        return Ok(_eventSrv.GetAllEvents(Request));
    }

}
