#!/usr/bin/env python
from RpcClient import RpcClient
import asyncio

async def asyncRpcClient():
    with RpcClient() as rpcClient:
        message = '2'
        print(f" [x] Requesting {message})")
        response = rpcClient.call(message)
        print(f" [.] Got {response}")

if __name__ == '__main__':
    asyncio.run(asyncRpcClient())