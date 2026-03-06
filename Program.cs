namespace CodingTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateTable();
        }
    }
}
