﻿using LighthouseControlCmd;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LighthouseControlCore
{
	public class LighthouseControlCmd
	{
		public static void Main(string[] args)
		{
			using var host = Host.CreateDefaultBuilder()
				.ConfigureServices((ctx, services) =>
				{
					services.Configure<AppSettings>(ctx.Configuration.GetSection("AppSettings"));
					services.AddSingleton<LighthousePowerController>();
				}).Build();

			var powerController = host.Services.GetService<LighthousePowerController>();
			powerController.Initialise();

			if (args.Length != 1)
			{
				Console.WriteLine("Supply either 'on' or 'off'");
				return;
			}

			switch (args[0])
			{
				case "on":
					powerController.TurnOn();
					break;
				case "off":
					powerController.TurnOff();
					break;
				default:
					throw new ArgumentException($"Unknown input command '{args[0]}'");
			}
		}
	}
}
