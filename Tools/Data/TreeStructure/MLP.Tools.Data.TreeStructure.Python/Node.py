#!/usr/bin/env python
from typing import Generic
import typing

T = typing.TypeVar('T')

class Node(Generic[T]):
    #https://softwareengineering.stackexchange.com/questions/254576/is-it-a-good-practice-to-declare-instance-variables-as-none-in-a-class-in-python
    #don't do it!!! this like static in CS (class level in Python)
    #Data: T
    #Parent: Node[T]
    #Children: list[Node[T]]# = list[Node[T]]()

    def __init__(self, 
                 aData:T =None, 
                 aParent:Node[T] =None
                 ):
        #class fields self-declarated in class methods, in all class methods !!!
        self.Data: T = aData
        self.Parent: Node[T] = aParent
        self.Children = list[Node[T]]()


    def AddChild(aData:T) -> None:
        Children.append(Node[T](aData, self))

tree = Node[str]("")
tree.Data = "root";
tree.AddChild("branch_1")
tree.Children[0].AddChild("branch_1.1");
tree.AddChild("branch_2");