using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;


using Steeltoe.CloudFoundry.Connector.RabbitMQ;
using DotNetBuild.Infrastructure.Messaging.Publisher;
using DotNetBuild.Infrastructure.Messaging.Subscriber;
using DotNetBuild.Services.Interfaces;
using DotNetBuild.Services;
using DotNetBuild.Domain.Models;
using Steeltoe.CloudFoundry.Connector.Redis;
using DotNetBuild.Services;
using DotNetBuild.Infrastructure.Repositories.Interfaces;
using DotNetBuild.Infrastructure.Repositories;
using DotNetBuild.Services.Interfaces;
using Steeltoe.CloudFoundry.Connector.PostgreSql.EFCore;
using DotNetBuild.Domain.Models;
using Microsoft.EntityFrameworkCore;
using DotNetBuild.Infrastructure.Initialization;
using DotNetBuild.Domain.Models.Interfaces;
using DotNetBuild.Infrastructure.Repositories;
using DotNetBuild.Services.Interfaces;
using DotNetBuild.Services;
namespace DotNetBuild
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        services.AddSwaggerGen();
            services.AddRabbitMQConnection(Configuration);
            services.AddTransient<IMessagePublisher, RabbitMQPublisher>();
            services.AddTransient<IMessageSubscriber, RabbitMQSubscriber>();
            services.AddTransient<IMessagePublisherService, MessagePublisherService>();
            services.AddTransient<IMessageSubscriberService, MessageSubscriberService>();
             services.Configure<MQConnectionData>(Configuration.GetSection("RabbitMQConectionString"));
            // This adds a IDistributedCache to the container
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = Configuration.GetConnectionString("RedisConectionString");
            });
            services.AddTransient<ICacheDataRepository, RedisCacheDataRepository>();
            services.AddTransient<ICacheService, CacheService>();

            // Add Context and use Postgres as provider ... provider will be configured from VCAP_ info            
            services.AddDbContext<InitializeContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostgressConectionString")));
            services.AddTransient<ISampleDataRepository, PostgresSampleDataRepository>();
            services.AddTransient<ISampleService, SampleService>();
            services.AddMvc();
            services.AddControllers();
        }
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



       

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });  

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    
    }
}
