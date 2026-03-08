using Spectre.Console;
using CodingTracker.Models;

namespace CodingTracker
{
    internal class UserInterface
    {
        DatabaseManager databaseManager = new DatabaseManager();
        Helpers helpers = new Helpers();

        internal void MainMenu()
        {
            bool isCloseApp = false;
            while (!isCloseApp) 
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[bold yellow]---Welcome to the Coding Tracker[/]---\n");

                var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<Enums.MenuOptions>()
                        .Title("What would you like to do?")
                        .AddChoices(Enum.GetValues<Enums.MenuOptions>()));

                switch (choice)
                {
                    case Enums.MenuOptions.ViewSessions:
                        ViewSessions();
                        break;
                    case Enums.MenuOptions.InsertSessions:
                        InsertSessions();
                        break;
                    case Enums.MenuOptions.UpdateSessions:
                        UpdateSessions();
                        break;
                    case Enums.MenuOptions.DeleteSessions:
                        DeleteSessions();
                        break;
                    case Enums.MenuOptions.CloseApp:
                        isCloseApp = true;   
                        break;

                }

            }
        }

        private void ViewSessions()
        {
            var sessions = databaseManager.Get();

            var table = new Table();

            table.AddColumn("[bold]ID[/]");
            table.AddColumn("[bold]Start Date[/]");
            table.AddColumn("[bold]End Date[/]");
            table.AddColumn("[bold]Duration[/]");

            foreach (var session in sessions)
            {
                table.AddRow(session.Id.ToString(),$"{session.StartTime}",$"{session.EndTime}",$"{session.Duration}");
            }

            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("Press enter key to go back to Main Menu");
            Console.ReadLine();
        }

        private void InsertSessions()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]---Insert Coding Sessions---[/]");
            
            AnsiConsole.MarkupLine("Please insert the [bold green]start[/] date and time (or type 0 to go back to the Main Menu).");
            var startDateTime = helpers.CheckDateTime();
            if (startDateTime == "0") return;

            AnsiConsole.MarkupLine("\nPlease insert the [bold green]end[/] date and time (or type 0 to go back to the Main Menu).");
            var endDateTime = helpers.CheckDateTime();
            if (endDateTime == "0") return;

            var duration = helpers.GetDuration(startDateTime, endDateTime);

            if (duration < TimeSpan.Zero)
            {
                AnsiConsole.Markup("[red]End date cannot be before start date! Please try again.[/]\n");
                Console.ReadLine();

            }
            else
            {
                CodingSession session = new CodingSession();
                session.StartTime = startDateTime;
                session.EndTime = endDateTime;
                session.Duration = $"{(int)duration.TotalHours:D2}:{duration.Minutes:D2}";

                int rows = databaseManager.Post(session);

                if (rows > 0)
                {
                    AnsiConsole.MarkupLine("\n[green]Session added sucessfully![/]");
                    Console.ReadLine();
                }
                else
                {
                    AnsiConsole.MarkupLine("\n[red]Session added failed! Try again later[/]");
                    Console.ReadLine();
                }

                
            }
            
        }

        private void UpdateSessions()
        {

        }


        private void DeleteSessions()
        {
            
        }
        
    }
}
