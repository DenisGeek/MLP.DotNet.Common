using System;

namespace Tools.Environment
{
    public class EnvDiscord
    {
        public static readonly string Token;
        static EnvDiscord()
        {
            Token = System.Environment.GetEnvironmentVariable("MLP_DiscordBot_Token");
        }
    }
}
