using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Helpers.Class.Object
{
    public static class ClassObjectExt
    {
        public static string GetMethodName(this object the)
            => new StackTrace().GetFrame(1).GetMethod().Name;

        public static string GetNamespace(this object the)
            => System.Reflection.Assembly.GetCallingAssembly().EntryPoint.DeclaringType.Namespace;
    }
}
