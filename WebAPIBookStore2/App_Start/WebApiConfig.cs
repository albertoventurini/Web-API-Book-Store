﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPIBookStore2.Handlers;

namespace WebAPIBookStore2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.MessageHandlers.Add(new BasicAuthenticationHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
