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

    [HttpGet("Events")]
    public IActionResult GetEventsForDay([FromQuery] DateTime date)
    {
        return Ok(_eventSrv.GetEventsForDay(date));
    }
    [HttpGet("EventsForWeek")]
    public IActionResult GetEventsForWeek([FromQuery] DateTime date)
    {
        return Ok(_eventSrv.GetEventsForWeek(date));
    }

    [HttpGet("EventInformation/{eventId}")]
    public IActionResult GetEventInformation(int eventId)
    {
        return Ok(_eventSrv.GetEventInformation(eventId));
    }
    [HttpPost("AddEvent")]
    public IActionResult AddEventFromBody(string name, string type, DateTime date, string description)
    {
        return Ok(_eventSrv.AddEvent(name, type, date, description));
    }

    [HttpPut("ModifyEventInformation/{eventId}")]
    public IActionResult AddEvenModifyEventInformationtModifyEventInformationFromBody(int eventId, string name, string type, DateTime date, string description)
    {
        return Ok(_eventSrv.AddEvent(name, type, date, description));
    }

    [HttpGet("GetAllEvents")]
    public IActionResult GetAllEvents(HttpRequest request)
    {
        return Ok(_eventSrv.GetAllEvents(request));
    }

}
