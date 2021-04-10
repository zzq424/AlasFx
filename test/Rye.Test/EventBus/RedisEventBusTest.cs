﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Rye.Configuration;
using Rye.EventBus;
using Rye.EventBus.Redis;

using System.Linq;
using System.Threading.Tasks;

using Xunit;

namespace Rye.Test.EventBus
{
    public class RedisEventBusTest
    {
        [Fact()]
        public async Task PushblishTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddRedisEventBus(options =>
                {
                    options.RedisOptions = redisOptions =>
                        ConfigurationManager.Appsettings.GetSection("Framework:Redis").GetChildren().FirstOrDefault().Bind(redisOptions);
                    options.Key = "RyeEventBus";
                });
            serviceCollection.SubscriberEvent<IRedisEventBus>((_, bus) =>
            {
                bus.Subscribe<TestEvent>(new TestRedisEventHandler { Id = 1 });
                bus.Subscribe<TestEvent>(new TestRedisEventHandler { Id = 2 });
            });
            var services = serviceCollection.BuildServiceProvider();

            var eventBus = services.GetRequiredService<IRedisEventBus>();
            eventBus.Subscribe<TestEvent>(
                new TestRedisEventHandler[] { new TestRedisEventHandler() { Id = 0 }, new TestRedisEventHandler() { Id = 1 } }
            );

            for (var i = 0; i < 10; i++)
            {
                eventBus.Publish(new TestEvent { Id = i });
                await Task.Delay(200);
            }

            await Task.Delay(20000);
            eventBus.Dispose();
            Assert.True(true);
        }
    }
}
