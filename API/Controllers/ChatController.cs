using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly StoreContext _context;

        public ChatController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("getUserChat")]
        public async Task<ActionResult<List<NotificationDTO>>> GetAllChatByUser(int userId)
        {
            var chats = await _context.Chats
                .Where(n => n.UserId == userId)
                .ToListAsync();

            if (chats.Count > 0)
            {
                var chatsDTO = chats.Select(toChatDTO).ToList();
                return Ok(chatsDTO);
            }

            return NotFound();
        }


        [HttpPost("SendChat")]
        public async Task<ActionResult<ChatDTO>> SendChatMessage(int senderId, string message, int recieverId)
        {
            var sender = await _context.Users.FirstOrDefaultAsync(user => user.UserId == senderId);

            if (sender == null)
            {
                return NotFound("Sender not found");
            }

            if (sender.RoleId == 1)
            {
                var staffUsers = await _context.Users.Where(user => user.RoleId == 2).ToListAsync();

                if (staffUsers.Count == 0)
                {
                    return NotFound("No users found");
                }

                foreach (var user in staffUsers)
                {
                    var chat = new Chat
                    {
                        UserId = user.UserId,
                        SenderId = sender.UserId,
                        Sender = sender.Name,
                        Content = message,
                        CreatedDate = DateTime.Now,
                        IsRead = false
                    };

                    _context.Chats.Add(chat);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var users = await _context.Users.Where(user => user.UserId == recieverId).ToListAsync();

                if (users.Count == 0)
                {
                    return NotFound("No users found");
                }

                foreach (var user in users)
                {

                    var chat = new Chat
                    {
                        UserId = user.UserId,
                        SenderId = sender.UserId,
                        Sender = sender.Name,
                        Content = message,
                        CreatedDate = DateTime.Now,
                        IsRead = false
                    };
                    _context.Chats.Add(chat);
                    await _context.SaveChangesAsync();
                }
            }
            return Ok();
        }


        [HttpGet("UpdateIsRead")]
        public async Task<ActionResult<List<ChatDTO>>> UpdateIsReadChat(int userId)
        {
            var chats = await _context.Chats
                .Where(n => n.UserId == userId)
                .ToListAsync();

            if (chats.Count > 0)
            {
                foreach (var chat in chats)
                {
                    chat.IsRead = true;
                }

                await _context.SaveChangesAsync();

                var chatsDTO = chats.Select(toChatDTO).ToList();
                return Ok(chatsDTO);
            }

            return NotFound();
        }




        public static ChatDTO toChatDTO(Chat? chat)
        {
            ChatDTO chatDTO = new ChatDTO();

            chatDTO.UserId = chat.UserId;
            chatDTO.SenderId = chat.SenderId;
            chatDTO.Sender = chat.Sender;
            chatDTO.Content = chat.Content;
            chatDTO.CreatedDate = chat.CreatedDate;
            chatDTO.IsRead = chat.IsRead;

            return chatDTO;
        }
    }
}
