#!/usr/bin/env python
import json
import inspect

class NodeP():pass
class ObjectEncoderADP(json.JSONEncoder): pass

class NodeP():
    #https://softwareengineering.stackexchange.com/questions/254576/is-it-a-good-practice-to-declare-instance-variables-as-none-in-a-class-in-python
    #https://dzone.com/articles/python-class-attributes-vs-instance-attributes
    #https://stackoverflow.com/questions/27481116/how-to-declare-a-static-attribute-in-python/27481965
    #don't do it!!!  this like static in CS (class level in Python), just when
    #it called from Class.Prorety
    #Data:Any
    #Parent: NodeP
    #Children : [] = []

    def __init__(self, 
                 aData=None, 
                 aParent:NodeP=None):
        self.Data = aData
        self.Parent = aParent
        self.Children = list()

    def AddChild(self, aData) -> NodeP:
        self.Children.append(NodeP(aData, self))
        return self.Children[-1]

    def toJSON(self):
        ObjectEncoderADP.idc = 0
        return json.dumps(self, check_circular=False, cls=ObjectEncoderADP, sort_keys=True, indent=4)

class ObjectEncoderADP(json.JSONEncoder):
    idc :int = 0
    def default(self, obj):
        if hasattr(obj, "to_json"):
            return self.default(obj.to_json())
        elif hasattr(obj, "__dict__"):

            # set Id to obgect
            ObjectEncoderADP.idc = ObjectEncoderADP.idc + 1
            obj.__dict__["$id"]=ObjectEncoderADP.idc

            # set Id to all child objects
            for (key, value) in inspect.getmembers(obj):
                if hasattr(value, "__len__"):
                    for (vl) in value:
                        if vl.__class__.__name__ == NodeP.__name__:
                            vl_index = obj.__dict__[key].index(vl)
                            obj.__dict__[key][vl_index].__dict__["$ref"] = obj.__dict__["$id"]

            # create dict 4 JSON
            d = dict()

            for key, value in inspect.getmembers(obj):
                # if field has name "Parent", then delete it from object
                # and add to dictJson Parent{ "$ref"= X }
                if key == "Parent" and (obj.__class__.__name__ == NodeP.__name__):
                    if ((value is None) or (value.__class__.__name__ != NodeP.__name__)):
                        d["Parent"] = None
                    else:
                        d["Parent"] =  None if not hasattr(value, "$ref") else dict([("$ref", obj.__dict__["$ref"])])
                    del obj.__dict__["Parent"]
                
                elif (not key.startswith("__")
                    and not inspect.isabstract(value)
                    and not inspect.isbuiltin(value)
                    and not inspect.isfunction(value)
                    and not inspect.isgenerator(value)
                    and not inspect.isgeneratorfunction(value)
                    and not inspect.ismethod(value)
                    and not inspect.ismethoddescriptor(value)
                    and not inspect.isroutine(value)):
                    d[key] = value
            if hasattr(obj,"$ref"):
                del obj.__dict__["$ref"] #now it unnecessary

            return self.default(d)
        return obj

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

    tree = NodeP()
    tree.Data = DiscordNode("MLP Discord","root")
    child = tree.AddChild(DiscordNode("cat 1","category"))
    child.AddChild(DiscordNode("chan 1","category"))
    tree.AddChild(DiscordNode("cat 2","category"))

    j = tree.toJSON()
    print(j)
