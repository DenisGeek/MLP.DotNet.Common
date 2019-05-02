#!/usr/bin/env python
from RpcClient import RpcClient

with RpcClient() as rpcClient:
    print(" [x] Requesting fib(30)")
    response = rpcClient.call("2")
    print(" [.] Got %r" % response)
