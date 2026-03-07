using Spectre.Console;

namespace CodingTracker
{
    internal class UserInterface
    {
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
                    case Enums.MenuOptions.AddSessions:
                        AddSessions();
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

        }

        private void AddSessions()
        {

        }

        private void UpdateSessions()
        {

        }


        private void DeleteSessions()
        {
            
        }
        
    }
}
