using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebAppRabbitMQ.Util;


namespace WebAppRabbitMQ.Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly ServerRabbitMQSettings _appSettings;

        public ConsumerController(IOptions<ServerRabbitMQSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet("obter-mensagem")]
        public ActionResult ObterMensagemQueue()
        {
            // Por default a porta utilizada é 5672 
            var factory = new ConnectionFactory() { HostName = _appSettings.Host };
            string mensagem = string.Empty;

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

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        mensagem = Encoding.UTF8.GetString(body);
                    };

                    channel.BasicConsume(queue: _appSettings.Queue,
                                         autoAck: true,
                                         consumer: consumer);
                    
                }
            }

            return Ok($"Mensagem recebida: {mensagem}");
        }
    }
}
