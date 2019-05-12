#!/usr/bin/env python
import json

class Node():pass
class Node():
    def __init__(self, 
                 aData=None):
        self.Data = aData
        self.Children = list()

    def AddChild(self, aData) -> Node:
        self.Children.append(Node(aData))
        return self.Children[-1]

    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=False, indent=4)

#example
if __name__ == '__main__':
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
    print(j)