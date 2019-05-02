#!/usr/bin/env python
from RpcClient import RpcClient

with RpcClient() as rpcClient:
    message = '2'
    print(f" [x] Requesting {message})")
    response = rpcClient.call(message)
    print(f" [.] Got {response}")
