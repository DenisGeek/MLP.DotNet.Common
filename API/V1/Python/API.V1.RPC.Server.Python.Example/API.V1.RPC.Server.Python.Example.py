#!/usr/bin/env python
from RpcServer import RpcServer
import asyncio
import os #https://stackoverflow.com/questions/4906977/how-to-access-environment-variable-values

def getEnvUserPass(userDef="guest", passDef="guest",)->(str,str):
    theUser = os.getenv('RabbitMQ_User', userDef)
    thePass = os.getenv('RabbitMQ_Pass', passDef)
    return (theUser, thePass)

def Handler(message: str):
    print(f" [.] Received {message})")
    print(f" [x] Sended {message})")
    return message

async def asyncRpcServer():
    theUser, thePass = getEnvUserPass()
    with RpcServer(aUser=theUser, aPass=thePass) as rpcServer:
        print(" [x] Awaiting RPC requests")
        rpcServer.StartConsuming(Handler)

if __name__ == '__main__':
    asyncio.run(asyncRpcServer())