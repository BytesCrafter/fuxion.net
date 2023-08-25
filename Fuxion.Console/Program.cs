
class Program 
{
    static void Main(string[] args) 
    {
        if (args.Length > 0) 
        {
            Program.Process(args);
        }

        else 
        {
            Console.WriteLine("Invalid command!");
        }
    }

    protected static void Process(string[] strings) 
    {
        if( strings.Length > 0 && strings[0] == "server" ) {
            Console.WriteLine("Server is not running...");
        }

        Console.WriteLine("Command invalid...");
    }
}