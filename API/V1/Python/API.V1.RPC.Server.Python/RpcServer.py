#!/usr/bin/env python
import pika
import uuid
#from collections import namedtuple

class RpcServer:
    _connection: pika.BlockingConnection
    _channel: pika.channel.Channel

    _hostName: str
    _queueName: str 
    _port: int
    _queueName: str
    _user: str
    _pass: str
    #__handlerReceivedJson

    def __init__(self, 
                aHostName: str = "localhost",
                aVirtualHost: str = "/",
                aPort: int = 5672,
                aQueueName: str = "rpc_queue",
                aUser: str = "guest",
                aPass: str = "guest"):
        # Constructor
        self._hostName = aHostName;
        self._virtualHost = aVirtualHost;
        self._port = aPort;
        self._queueName = aQueueName;
        self._user = aUser;
        self._pass = aPass;

        self._connection, self._channel = self.CreateChannel(
            self._hostName, 
            self._virtualHost,
            self._port,
            self._queueName,
            self._user,
            self._pass)

    def __enter__(self):
        # Prepare `with` context
        return self

    def __exit__(self, exc_type, value, traceback):
        # Close connection after `with` context
        if self._connection.is_open:
            self._connection.close()

    def CreateChannel(self, 
                aHostName: str,
                aVirtualHost: str,
                aPort: int,
                aQueueName: str,
                aUser: str,
                aPass: str
                      ) -> (pika.BlockingConnection, pika.channel.Channel):
        #https://pika.readthedocs.io/en/latest/modules/parameters.html
        usrCredentials = pika.PlainCredentials(aUser, aPass)
        connection = pika.BlockingConnection(
            pika.ConnectionParameters(
                credentials = usrCredentials,
                virtual_host = aVirtualHost,
                host = aHostName,
                port = aPort,
                )
            )
        channel = connection.channel()
        channel.queue_declare(queue = aQueueName)
        return (connection, channel)

    def on_request(self, ch, method, props, body):
        message = str(body.decode("utf-8"))
        try:
            response = self.__handlerReceivedJson(message)
        except:
            #https://stackoverflow.com/questions/2052390/manually-raising-throwing-an-exception-in-python
            #https://docs.python.org/3/tutorial/errors.html#handling-exceptions
            raise ValueError(f'HandlerReceivedJson:{HandlerReceivedJson} wrong, use Handler(message: str)->str, origanal error: {sys.exc_info()[0]}')
        
        ch.basic_publish(exchange='',
                         routing_key=props.reply_to,
                         properties=pika.BasicProperties(correlation_id = \
                                                             props.correlation_id),
                         body=str(response))
        ch.basic_ack(delivery_tag=method.delivery_tag)

    def CreateConsumer(self, aQueueName: str):
        self._channel.basic_qos(prefetch_count=1)
        self._channel.basic_consume(queue=aQueueName, on_message_callback = self.on_request)

    def StartConsuming(self, aHandlerReceivedJson):
        self.__handlerReceivedJson = aHandlerReceivedJson
        self.CreateConsumer(self._queueName)
        self._channel.start_consuming()
