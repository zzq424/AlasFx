﻿using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rye
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureApp(this IHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                // 存储环境对象
                App.HostEnvironment = hostingContext.HostingEnvironment;

                // 加载配置
                App.AddConfigureFiles(config, hostingContext.HostingEnvironment);
            })
            .ConfigureServices(services => services.AddRye());
        }
    }
}
