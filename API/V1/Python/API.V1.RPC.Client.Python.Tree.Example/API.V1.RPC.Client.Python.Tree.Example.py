#!/usr/bin/env python
from RpcClient import RpcClient
from Node import Node
import asyncio
import os #https://stackoverflow.com/questions/4906977/how-to-access-environment-variable-values

def getEnv(userDef="guest", passDef="guest",)->(str,str,str,int):
    theUser = os.getenv('RabbitMQ_User', userDef)
    thePass = os.getenv('RabbitMQ_Pass', passDef)
    theHost = os.getenv('RabbitMQ_Host', "localhost")
    thePort = int(os.getenv('RabbitMQ_Port', "5672"))
    return (theUser, thePass, theHost, thePort)

async def asyncRpcClient():
    class DiscordNode:
        def __init__(self, 
                    aNodePName:str =None, 
                    aNodePKind:str =None,
                    aNodePDescr:str =None):
            self.NodePName = aNodePName
            self.NodePKind = aNodePKind
            self.NodePDescr = aNodePDescr

    tree = Node()
    tree.Data = DiscordNode("MLP Discord","root")
    child = tree.AddChild(DiscordNode("cat 1","category"))
    child.AddChild(DiscordNode("chan 1","category"))
    tree.AddChild(DiscordNode("cat 2","category"))

    j = tree.toJSON()

    theUser, thePass, theHost, thePort = getEnv()
    with RpcClient(aHostName=theHost,
                   aPort=thePort,
                   aQueueName="Step.Convert.DiscordTree2PlantUml",
                   aUser=theUser,
                   aPass=thePass) as rpcClient:
        message = j
        print(f" [x] Requesting {message})")
        response = rpcClient.call(message)
        print(f" [.] Got {response}")

if __name__ == '__main__':
    asyncio.run(asyncRpcClient())
