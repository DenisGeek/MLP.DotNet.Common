#!/usr/bin/env python
import pika
import uuid
#from collections import namedtuple

class RpcServer:
    _connection: pika.BlockingConnection
    _channel: pika.channel.Channel

    _hostName: str
    _queueName: str 
    #_handlerReceivedJson

    def __init__(self, aHostName: str = "localhost", aQueueName: str = "rpc_queue"):
        # Constructor
        self._hostName = aHostName
        self._queueName = aQueueName

        self._connection, self._channel = self.CreateChannel(self._hostName, self._queueName)

    def __enter__(self):
        # Prepare `with` context
        return self

    def __exit__(self, exc_type, value, traceback):
        # Close connection after `with` context
        if self._connection.is_open:
            self._connection.close()

    def CreateChannel(self, aHostName: str, aQueueName: str) -> (pika.BlockingConnection, pika.channel.Channel):
        connection = pika.BlockingConnection(
            pika.ConnectionParameters(host = aHostName))
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
