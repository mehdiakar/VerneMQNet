using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Register all monitoring services and administration managers in service collection
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection UseVerneMQNetAllServices(this IServiceCollection services)
		{
			services.UseVerneMQNetAdministrationServices();
			services.UseVerneMQNetMonitoringServices();
			return services;
		}

		/// <summary>
		/// Register all administration managers in service collection
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection UseVerneMQNetAdministrationServices(this IServiceCollection services)
		{
			services.AddSingleton<Administration.Manager.ICluster, Administration.Manager.Cluster>();
			services.AddSingleton<Administration.Manager.IConfiguration, Administration.Manager.Configuration>();
			services.AddSingleton<Administration.Manager.INode, Administration.Manager.Node>();
			services.AddSingleton<Administration.Manager.IPlugin, Administration.Manager.Plugin>();
			services.AddSingleton<Administration.Manager.ISession, Administration.Manager.Session>();
			services.AddSingleton<Administration.Manager.IWebhook, Administration.Manager.Webhook>();
			return services;
		}

		/// <summary>
		/// Register all monitoring service in service collection
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection UseVerneMQNetMonitoringServices(this IServiceCollection services)
		{
			services.AddSingleton<Monitoring.IHealthChecker, Monitoring.HealthChecker>();
			services.AddSingleton<Monitoring.IStatus, Monitoring.Status>();
			return services;
		}


	}
}
