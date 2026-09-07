using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Redonis.Netwise;
using Redonis.Netwise.Clients;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient<ICatFactClient, CatFactClient>(httpClient => 
{ 
	httpClient.BaseAddress = new Uri("https://catfact.ninja");
});

builder.Services.AddSingleton(new FileWriter("cat-facts.txt"));

builder.Services.AddTransient<CatFactService>();

using var host = builder.Build();

var catFactService = host.Services.GetRequiredService<CatFactService>();

await catFactService.SaveRandomFactAsync();
