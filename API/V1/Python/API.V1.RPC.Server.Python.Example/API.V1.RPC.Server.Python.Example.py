#!/usr/bin/env python
from RpcServer import RpcServer

def Handler(message: str):
    print(f" [.] Received {message})")
    print(f" [x] Sended {message})")
    return message

with RpcServer() as rpcServer:
    print(" [x] Awaiting RPC requests")
    rpcServer.StartConsuming(Handler)