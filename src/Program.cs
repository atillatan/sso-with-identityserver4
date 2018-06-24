/*
 * @Author: Atilla Tanrikulu 
 * @Date: 2018-04-16 10:10:45 
 * @Last Modified by: Atilla Tanrikulu
 * @Last Modified time: 2018-04-17 10:26:52
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SSO.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "SSO.Web";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .UseUrls("http://localhost:5000")                 
                   .Build();
        }
    }
}
