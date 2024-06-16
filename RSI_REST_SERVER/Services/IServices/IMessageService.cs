using Microsoft.AspNetCore.Mvc;
using RSI_REST_SERVER.Models;

namespace RSI_REST_SERVER.Services.IServices
{
    public interface IMessageService
    {
        public List<Message> GetMessages(HttpRequest request);
        public Message GetMessage(int id, HttpRequest request);
        public List<string> GetMessageComments(int id, HttpRequest request);
        public Message CreateMessage([FromBody] Message message);
        public Message EditMessage([FromBody] Message newMessage);
        public bool DeleteMessage([FromRoute] int id);
    }
}
