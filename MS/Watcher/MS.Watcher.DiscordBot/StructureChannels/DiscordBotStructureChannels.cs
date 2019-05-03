using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.Watcher.DiscordBot.StructureChannels
{
    public class DiscordBotStructureChannels
    {
        public int ScanInterval = 1000;
        public async Task Start(DiscordSocketClient aClient)
        {
            var i = 0;
            while (true)
            {
                var structure = await Scan();
                Console.WriteLine($"{i}");
                await Task.Delay(ScanInterval);
            }
        }
        public async Task<string> Scan()
        {
            return "";
        }
    }
}
