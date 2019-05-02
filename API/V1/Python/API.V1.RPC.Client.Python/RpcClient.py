#!/usr/bin/env python
import pika
import uuid

class RpcClient:
    _connection: pika.BlockingConnection
    _channel: pika.channel.Channel

    _hostName: str
    _queueName: str 

    def __init__(self, aHostName: str = "localhost", aQueueName: str = "rpc_queue"):
        # Constructor
        self._hostName = aHostName
        self._queueName = aQueueName

        self._connection, self._channel = self.CreateChannel(self._hostName)
        self.CreateConsumer()

    def __enter__(self):
        # Prepare `with` context
        return self

    def __exit__(self, exc_type, value, traceback):
        # Close connection after `with` context
        if self._connection.is_open:
            self._connection.close()

    def CreateChannel(self, aHostName: str) -> (pika.BlockingConnection, pika.channel.Channel):
        connection = pika.BlockingConnection(
            pika.ConnectionParameters(host = aHostName))
        channel = connection.channel()
        return (connection, channel)

    def on_response(self, ch, method, props, body):
        if self._corr_id == props.correlation_id:
            self._response = body

    def CreateConsumer(self):
        self._queue = self._channel.queue_declare('', exclusive=True)
        self._callback_queue = self._queue.method.queue
        self._channel.basic_consume(
            queue=self._callback_queue,
            on_message_callback=self.on_response,
            auto_ack=True)

    def call(self, message:str):
        self._response = None
        self._corr_id = str(uuid.uuid4())
        self._channel.basic_publish(
            exchange='',
            routing_key = self._queueName,
            properties=pika.BasicProperties(
                reply_to=self._callback_queue,
                correlation_id=self._corr_id,
            ),
            body = message)
        while self._response is None:
            self._connection.process_data_events()
        return str(self._response.decode("utf-8"))