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
        /// ������ ��������� �� �������
        /// </summary>
        /// <param name="rcpt">������������� ����������</param>
        /// <returns>������ ���������, ������������ ����������</returns>
        /// <response code="200">�������� ���������</response>
        /// <response code="204">��������� ��� ���������� �� �������</response>
        /// <response code="400">������ ��� ��������� ���������</response>
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

        /// <summary>�������� ���������</summary>
        /// <param name="messages">���������</param>
        /// <remarks> ������ ���������:
        /// {
        ///     "subject": "string",
        ///     "body": "string",
        ///     "recipients": [
        ///         0
        ///     ]
        /// }
        /// </remarks>
        /// <returns>��������� �������� ���������</returns>
        /// <response code="201">��������� �������</response>
        /// <response code="204">������ ��������� ����</response>
        /// <response code="400">������ ��� ��������� ���������</response>
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
