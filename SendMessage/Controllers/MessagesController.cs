using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SendMessage.Dto;
using SendMessage.Models;
using SendMessage.Services.MessageService;

namespace SendMessage.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class MessagesController : ControllerBase
    {
        private IMessageService _messageService;
        private IMapper _mapper;

        public MessagesController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Чтение сообщений из очереди
        /// </summary>
        /// <param name="rcpt">Идентификатор получателя</param>
        /// <returns>Список сообщений, адресованный получателю</returns>
        /// <response code="200">Получает сообщения</response>
        /// <response code="204">Сообщения для получателя не найдены</response>
        /// <response code="400">Ошибка при получении сообщения</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MessageDto>> Get(int rcpt)
        {
            try
            {
                var messages = _mapper.Map<IEnumerable<MessageDto>>(_messageService.GetMessages(rcpt));

                if (messages.Count() == 0)
                    return NoContent();

                return Ok(messages);
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>Создание сообщений</summary>
        /// <param name="messages">Сообщения</param>
        /// <remarks> Пример сообщения:
        /// {
        ///     "subject": "string",
        ///     "body": "string",
        ///     "recipients": [
        ///         0
        ///     ]
        /// }
        /// </remarks>
        /// <returns>Результат создания сообщения</returns>
        /// <response code="201">Сообщение создано</response>
        /// <response code="204">Список сообщений пуст</response>
        /// <response code="400">Ошибка при получении сообщения</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] IEnumerable<Message> messages)
        {
            try
            {
                if (messages.Count() == 0)
                    return NoContent();

                _messageService.AddRange(messages);
                return new StatusCodeResult(201);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
