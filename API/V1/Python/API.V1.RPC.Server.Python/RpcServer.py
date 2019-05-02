#!/usr/bin/env python
import pika
import uuid
#from collections import namedtuple

class RpcServer:
    _connection: pika.BlockingConnection
    _channel: pika.channel.Channel

    _hostName: str
    _queueName: str 
    HandlerReceivedJson: 

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

    def CreateChannel(self, aHostName: str, aQueueName: str) -> (pika.BlockingConnection, pika.channel.Channel):
        connection = pika.BlockingConnection(
            pika.ConnectionParameters(host = aHostName))
        channel = connection.channel()
        channel.queue_declare(queue = aQueueName)
        return (connection, channel);
