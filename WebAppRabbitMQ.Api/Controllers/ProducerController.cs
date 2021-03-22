using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using WebAppRabbitMQ.Api.Model;
using WebAppRabbitMQ.Util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppRabbitMQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {

        private readonly ServerRabbitMQSettings _appSettings;

        public ProducerController(IOptions<ServerRabbitMQSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpPost("enviar")]
        public ActionResult EnviarMensagem([FromBody] Computador request)
        {
            var factory = new ConnectionFactory() { HostName = _appSettings.Host, Port = 5672 };
            var mensagem = $"O notebook {request.Marca} contém as configurações: {request.Configuracoes}.";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //Usar o código comentado abaixo somente quando for criar uma queue (fila). 

                    //channel.QueueDeclare(queue: _appSettings.Queue,
                    //                     durable: false,
                    //                     exclusive: false,
                    //                     autoDelete: false,
                    //                     arguments: null);

                    var body = Encoding.UTF8.GetBytes(mensagem);

                    channel.BasicPublish(exchange: "",
                                         routingKey: _appSettings.Queue,
                                         basicProperties: null,
                                         body: body);
                    
                }
            }
            

            return Ok($"Mensagem '{mensagem}' enviada com sucesso!");
        }

    }
}
