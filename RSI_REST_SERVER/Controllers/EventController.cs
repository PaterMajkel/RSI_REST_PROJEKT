using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RSI_REST_SERVER.Models;
using RSI_REST_SERVER.Services;
using Spire.Pdf.Graphics;
using Spire.Pdf;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.Diagnostics;

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

    [HttpPost("[action]")]
    public IActionResult GetPdf([FromBody] List<int> ids)
    {
        return Ok(_eventSrv.GetPdf(ids));
    }
    [HttpPost("GenerateReport")]
    public IActionResult GetReport([FromBody] ListDto list) {
        var eventList = list.SentList;
        var reportList = _eventSrv.GetAllEvents(Request).Where(p=>eventList.Contains(p.Id)).ToList();

        PdfDocument pdfDocument = new PdfDocument();
        PdfPageBase page = pdfDocument.Pages.Add();

        // Set font
        PdfFont font = new(PdfFontFamily.Helvetica, 11);

        // Add title
        page.Canvas.DrawString("Eventy Białostockie:", font, PdfBrushes.Black, new PointF(50, 50));

        // Format date
        int yPosition = 70;
        foreach (var eventItem in reportList)
        {
            // Add event details
            page.Canvas.DrawString($"Event nr: {(eventItem.Id)}", font, PdfBrushes.Black, new PointF(50, yPosition));
            page.Canvas.DrawString($"Nazwa: {eventItem.Name}", font, PdfBrushes.Black, new PointF(50, yPosition += 10));
            page.Canvas.DrawString($"Typ: {eventItem.Details.Type}", font, PdfBrushes.Black, new PointF(50, yPosition += 10));
            page.Canvas.DrawString($"Data wydarzenia: {eventItem.Details.Date.ToString("dd-MM-yyyy")}", font, PdfBrushes.Black, new PointF(50, yPosition += 10));
            page.Canvas.DrawString($"Opis: {eventItem.Details.Description}", font, PdfBrushes.Black, new PointF(50, yPosition += 10));
            page.Canvas.DrawString(new string('-', 200), font, PdfBrushes.Black, new PointF(50, yPosition += 20));
            yPosition += 30; // Adjust position for next event
        }

        // Save PDF to MemoryStream
        MemoryStream ms = new MemoryStream();
        pdfDocument.SaveToStream(ms, FileFormat.PDF);
        return File(ms.ToArray(), "application/pdf", "report.pdf");
    }

}

public class ListDto
{
    public List<int> SentList { get; set; }
}