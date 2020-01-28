﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PollyResilience.Service;

namespace RedisPublisher
{
    public class ConsoleApp
    {
        protected readonly ILogger<ConsoleApp> _logger;
        protected readonly IPublisherConfiguration _config;
        protected readonly IRedisClient _redisClient;

        public ConsoleApp(IPublisherConfiguration configuration,
            ILogger<ConsoleApp> logger,
            IRedisClient redisClient)
        {
            _logger = logger;
            _config = configuration;
            _redisClient = redisClient;
        }

        public async Task Run()
        {
            var i = 0;

            while (true)
            {
                await _redisClient.PublishAsync("messages", messages[i]);

                await Task.Delay(600);

                if (i == messages.Length - 1)
                    i = 0;
                else
                    i++;
            }
        }



        protected string[] messages => new string[] {
@"
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░",
@"
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
@"
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓
▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓",
@"
████████████████████████████████████████████████
████████████████████████████████████████████████
████████████████████████████████████████████████
████████████████████████████████████████████████
████████████████████████████████████████████████
████████████████████████████████████████████████
████████████████████████████████████████████████
████████████████████████████████████████████████
████████████████████████████████████████████████",
@"                                               
 /$$$$$$$   /$$$$$$  /$$       /$$   /$$     /$$
| $$__  $$ /$$__  $$| $$      | $$  |  $$   /$$/
| $$  \ $$| $$  \ $$| $$      | $$   \  $$ /$$/ 
| $$$$$$$/| $$  | $$| $$      | $$    \  $$$$/  
| $$____/ | $$  | $$| $$      | $$     \  $$/   
| $$      | $$  | $$| $$      | $$      | $$    
| $$      |  $$$$$$/| $$$$$$$$| $$$$$$$$| $$    
|__/       \______/ |________/|________/|__/    ",
@"                                               
$$$$$$$\   $$$$$$\  $$\       $$\   $$\     $$\ 
$$  __$$\ $$  __$$\ $$ |      $$ |  \$$\   $$  |
$$ |  $$ |$$ /  $$ |$$ |      $$ |   \$$\ $$  / 
$$$$$$$  |$$ |  $$ |$$ |      $$ |    \$$$$  /  
$$  ____/ $$ |  $$ |$$ |      $$ |     \$$  /   
$$ |      $$ |  $$ |$$ |      $$ |      $$ |    
$$ |       $$$$$$  |$$$$$$$$\ $$$$$$$$\ $$ |    
\__|       \______/ \________|\________|\__|    ",
@"
 _______    ______   __        __    __      __ 
/       \  /      \ /  |      /  |  /  \    /  |
$$$$$$$  |/$$$$$$  |$$ |      $$ |  $$  \  /$$/ 
$$ |__$$ |$$ |  $$ |$$ |      $$ |   $$  \/$$/  
$$    $$/ $$ |  $$ |$$ |      $$ |    $$  $$/   
$$$$$$$/  $$ |  $$ |$$ |      $$ |     $$$$/    
$$ |      $$ \__$$ |$$ |_____ $$ |_____ $$ |    
$$ |      $$    $$/ $$       |$$       |$$ |    
$$/        $$$$$$/  $$$$$$$$/ $$$$$$$$/ $$/     ",
@"
 _______    ______   __        __    __      __ 
|       \  /      \ |  \      |  \  |  \    /  \
| $$$$$$$\|  $$$$$$\| $$      | $$   \$$\  /  $$
| $$__/ $$| $$  | $$| $$      | $$    \$$\/  $$ 
| $$    $$| $$  | $$| $$      | $$     \$$  $$  
| $$$$$$$ | $$  | $$| $$      | $$      \$$$$   
| $$      | $$__/ $$| $$_____ | $$_____ | $$    
| $$       \$$    $$| $$     \| $$     \| $$    
 \$$        \$$$$$$  \$$$$$$$$ \$$$$$$$$ \$$    "
};
    }
}