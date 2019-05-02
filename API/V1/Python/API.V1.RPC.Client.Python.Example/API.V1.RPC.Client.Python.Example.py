#!/usr/bin/env python
from RpcClient import RpcClient

rpcClient = RpcClient()
print(" [x] Requesting fib(30)")
response = rpcClient.call("2")
print(" [.] Got %r" % response)
