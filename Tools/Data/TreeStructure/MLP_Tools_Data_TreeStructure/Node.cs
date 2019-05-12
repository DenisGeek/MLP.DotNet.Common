using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Tools
{
    public class Node<TData>
    {
        public TData Data;
        public List<Node<TData>> Children = new List<Node<TData>>();

        public Node<TData> AddChild(TData aData)
        {
            var res = new Node<TData>(){ Data = aData};
            Children.Add(res);
            return res;
        }
           
    }
}
