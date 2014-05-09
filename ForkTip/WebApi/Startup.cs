// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Dapr Labs">
//   Copyright 2014, Dapr Labs Pty. Ltd.
// </copyright>
// <summary>
//   Defines the Startup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using ForkTip.Web;

[assembly: Microsoft.Owin.OwinStartup(typeof(Startup))]

namespace ForkTip.Web
{
    using Orleans.OwinMiddleware;

    using Owin;

    /// <summary>
    /// Initialization routines.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configure the application.
        /// </summary>
        /// <param name="app">The app being configured.</param>
        public void Configuration(IAppBuilder app)
        {
            app.ConfigureOrleans();
            ConfigureWebApi(app);
            ConfigureFileServer(app);
        }
    }
}