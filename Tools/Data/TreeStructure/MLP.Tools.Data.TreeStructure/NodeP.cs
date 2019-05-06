using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Tools
{
    public class NodeP<TData>
    {
        public TData Data;
        public NodeP<TData> Parent;
        public List<NodeP<TData>> Children = new List<NodeP<TData>>();

        public void AddChild(TData aData)
            => Children.Add(new NodeP<TData>()
                                            {
                                                Data = aData,
                                                Parent = this,
                                            });
    }
}
