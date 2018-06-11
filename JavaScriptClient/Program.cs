/*
 * @Author: Atilla Tanrikulu 
 * @Date: 2018-04-16 10:10:45 
 * @Last Modified by: Atilla Tanrikulu
 * @Last Modified time: 2018-04-17 10:29:37
 */

using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace JavaScriptClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "JavaScriptClient";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5003")
                .Build();
    }
}
