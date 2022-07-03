# RabbitMQ Library

Some of the backend microservices use RabbitMQ as a message bus to talk to each other.
To run a single RabbitMQ docker container:

```powershell
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

You will find the RabbitMQ dashboard at http://localhost:15672.

The username and password are both: "guest".
