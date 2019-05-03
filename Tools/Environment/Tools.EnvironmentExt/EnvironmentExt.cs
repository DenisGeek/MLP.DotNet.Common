using System;

namespace Tools.Environment
{
    public static class EnvironmentExt
    {
        public static string GetEnvironmentVariable(string variable, string defValue)
        {
            var value = Environment.GetEnvironmentVariable(variable);
            //check for a value, if nothing is returned then def val
            if (!string.IsNullOrEmpty(value))
                return value;
            else
                return defValue;
        }
    }
}
