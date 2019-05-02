#!/usr/bin/env python
from RpcServer import RpcServer
import asyncio

def Handler(message: str):
    print(f" [.] Received {message})")
    print(f" [x] Sended {message})")
    return message

async def asyncRpcServer():
    with RpcServer() as rpcServer:
        print(" [x] Awaiting RPC requests")
        rpcServer.StartConsuming(Handler)

if __name__ == '__main__':
    asyncio.run(asyncRpcServer())