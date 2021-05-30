﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Mirai.Net.Data;
using Mirai.Net.Data.Events.Enums;
using Mirai.Net.Data.Managers;
using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concrete;
using Mirai.Net.Data.Messages.Enums;
using Mirai.Net.Data.Messengers.Media;
using Mirai.Net.Managers;
using Mirai.Net.Messengers.Concrete;
using Mirai.Net.Messengers.MediaUploader;
using Mirai.Net.Sessions;
using Mirai.Net.Utilities.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocket = WebSocketSharp.WebSocket;

//TODO: Edit unused modify to internal
//TODO: Added effective exception handle
namespace Mirai.Net.Test
{
    public class Program
    {
        public static async Task Main()
        {
            Bot.Session = new MiraiSession
            {
                Host = "127.0.0.1",
                Port = "2334",
                Key = "232511772e8745e0bd697f1dfb72f748",
                QQ = "2672886221"
            };
            Bot.Modules = new[]
            {
                new TestModule()
            };

            Bot.ReceivedGroupJoinRequest += args =>
            {
                if (args.GroupId == "389105053")
                {
                    Bot.HandleGroupJoinRequest(args, MemberJoinApplyOperateType.Accept);
                }
            };
            Bot.ReceivedGroupInvited += args =>
            {
                Bot.HandleInvitedRequest(args, true);
            };
            
            await Bot.Launch();
                    
            Console.WriteLine("Connected!");
            Console.WriteLine(await Bot.GetPluginVersion());

            await Do();

            while (true)
            {
                if (Console.ReadLine() != "exit") continue;
                
                await Bot.Terminate();
                Console.WriteLine("Disconnected!");
                
                return;
            }
        }

        public static async Task Do()
        {
            var mgr = new GroupFileManager("809830266");

            await mgr.CreateDirectory("TestDir");

            Console.WriteLine("Done");
        }
    }
}