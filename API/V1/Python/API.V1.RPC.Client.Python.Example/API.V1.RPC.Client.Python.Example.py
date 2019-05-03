#!/usr/bin/env python
from RpcClient import RpcClient
import asyncio
import os #https://stackoverflow.com/questions/4906977/how-to-access-environment-variable-values

def getEnvUserPass(userDef="guest", passDef="guest",)->(str,str):
    theUser = os.getenv('RabbitMQ_User', userDef)
    thePass = os.getenv('RabbitMQ_Pass', passDef)
    return (theUser, thePass)

async def asyncRpcClient():
    theUser, thePass = getEnvUserPass()
    with RpcClient(aUser=theUser, aPass=thePass) as rpcClient:
        message = '2'
        print(f" [x] Requesting {message})")
        response = rpcClient.call(message)
        print(f" [.] Got {response}")

if __name__ == '__main__':
    asyncio.run(asyncRpcClient())