using Spectre.Console;
using System.Text.Json;

namespace Redonis.Netwise
{
	public sealed class ConsoleMenu
	{
		private readonly CatFactService _catFactService;

		public ConsoleMenu(CatFactService catFactService)
		{
			_catFactService = catFactService;
		}

		public async Task RunAsync(CancellationToken cancellationToken = default)
		{
			while (true)
			{
				var option = AnsiConsole.Prompt(
					new SelectionPrompt<MenuOption>()
						.Title("[bold]Cat Facts Menu[/]")
						.UseConverter(FormatMenuOption)
						.AddChoices(
							MenuOption.GetCatFact,
							MenuOption.Exit));

				switch (option)
				{
					case MenuOption.GetCatFact:
						await GetCatFactAsync(cancellationToken);
						break;

					case MenuOption.Exit:
						return;
				}
			}
		}

		private async Task GetCatFactAsync(CancellationToken cancellationToken)
		{
			try
			{
				var catFact = await _catFactService.SaveRandomFactAsync(cancellationToken);

				AnsiConsole.MarkupLine($"[green]{Markup.Escape(catFact)}[/]");
			}
			catch (HttpRequestException)
			{
				AnsiConsole.MarkupLine("[red]Failed to retrieve cat fact from the API.[/]");
			}
			catch (JsonException)
			{
				AnsiConsole.MarkupLine("[red]The API returned an invalid response.[/]");
			}
			catch (IOException)
			{
				AnsiConsole.MarkupLine("[red]Failed to save cat fact to the file.[/]");
			}
			catch
			{
				AnsiConsole.MarkupLine("[red]An unexpected error occurred.[/]");
			}
			finally
			{
				AnsiConsole.WriteLine();
			}
		}

		private static string FormatMenuOption(MenuOption option)
		{
			return option switch
			{
				MenuOption.GetCatFact => "Get cat fact",
				MenuOption.Exit => "Exit",
				_ => option.ToString()
			};
		}
	}
}
