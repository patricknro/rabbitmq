# RabbitMQ
Aplicação para enfileiramento e leitura das mensagens em fila.
- Foi construído o projeto WebAppRabbitMQ.Api para ser o **Producer**
- Foi construído o projeto WebAppRabbitMQ.Consumer para ser o **Consumer**

### Imagem Docker utilizada
- rabbitmq:3.8.9-management

### Comando para construção do Container
`docker run -d --hostname my-rabbit --name my-rabbit -p 15672:15672 -p  5672:5672 rabbitmq:3-management` <br />

**Host** <br />
_localhost:15672_ <br />
*Acessa o management do RabbitMQ via web para visualizar e gerenciar.

**Usuário**: guest <br />
**Senha**: guest


### Bibliotecas Nuget utilizadas
 - https://www.nuget.org/packages/RabbitMQ.Client 
 - Comando no Console Nuget Package para instalar. <br />
 `Install-Package RabbitMQ.Client -Version 6.2.1`

### Links dos tutorias

- https://www.rabbitmq.com/getstarted.html (Cada card tem um link para o tutorial em C#)

<br />

# cURL

### Producer
```
curl --location --request POST 'http://localhost:61963/api/producer/enviar' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Marca": "Dell",
    "Configuracoes": "16gb RAM, 500gb SSD, Intel i9 8th generation"
}'
```

### Consumer
```
curl --location --request GET 'http://localhost:62012/api/consumer/obter-mensagem'
```


