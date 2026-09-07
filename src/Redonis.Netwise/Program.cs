using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Redonis.Netwise;
using Redonis.Netwise.Clients;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
	.AddOptions<CatFactOptions>()
	.BindConfiguration(CatFactOptions.SectionName)
	.ValidateOnStart();

builder.Services.AddHttpClient<ICatFactClient, CatFactClient>((sp, httpClient) => 
{
	var options = sp.GetRequiredService<IOptions<CatFactOptions>>().Value;
	httpClient.BaseAddress = options.BaseAddress;
});

builder.Services.AddSingleton(sp =>
{
	var options = sp.GetRequiredService<IOptions<CatFactOptions>>().Value;
	return new FileWriter(options.FilePath);
});

builder.Services.AddTransient<CatFactService>();

using var host = builder.Build();

var catFactService = host.Services.GetRequiredService<CatFactService>();

await catFactService.SaveRandomFactAsync();
