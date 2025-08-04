namespace Assignment_2;

class Program
{
    static string FileName = "VideoGames.txt";
    static List<Game> VideoGames = GetGameList();
    
    static void Main()
    {
        for (int i = 0; i < VideoGames.Count; i++)
        {
            Console.WriteLine(VideoGames[i].ListGame());
        }
        AddGame();
    }

    static List<Game> GetGameList()
    {
        List<Game> videoGames = new List<Game>();
        try
        {
            StreamReader videoGamesFile = new StreamReader(FileName);
            string? gameString = videoGamesFile.ReadLine();
            while (gameString != null)
            {
                string[] gameArray = gameString.Split(',');
                videoGames.Add(new Game(
                    Convert.ToInt32(gameArray[0]),
                    gameArray[1],
                    Convert.ToInt32(gameArray[2]),
                    Convert.ToInt32(gameArray[3]),
                    Convert.ToInt32(gameArray[4])
                ));
                gameString = videoGamesFile.ReadLine();
            }
            videoGamesFile.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return videoGames;
    }

    static List<int> GetItemNumbers()
    {
        List<int> itemNumbers = new List<int>();
        for (int i = 0; i < VideoGames.Count; i++)
        {
            itemNumbers.Add(VideoGames[i].GetItemNumber());
        }
        return itemNumbers;
    }

    static void AddGame()
    {
        int itemNumber = 0;
        Console.WriteLine("Please enter an item number or leave blank to generate one");
        string? input = Console.ReadLine();
        while (itemNumber == 0)
        {
            List<int> itemNumbers = GetItemNumbers();
            if (input == "")
            {
                itemNumber = 1000;
                while (itemNumbers.Contains(itemNumber))
                {
                    itemNumber++;
                }
            }
            else if (!Int32.TryParse(input, out itemNumber) || itemNumber < 1 || itemNumber > 9999)
            {
                Console.WriteLine("Invalid input, please enter a number between 1 and 9999");
                input = Console.ReadLine();
                itemNumber = 0;
            }
            else if (itemNumbers.Contains(Convert.ToInt32(input)))
            {
                Console.WriteLine("Item number already exists, please enter another item number");
                input = Console.ReadLine();
                itemNumber = 0;
            }
        }
        
        Console.WriteLine("Please enter the game name");
        string? itemName = Console.ReadLine();
        while (String.IsNullOrEmpty(itemName))
        {
            Console.WriteLine("Input is blank. Please enter the game name");
            itemName = Console.ReadLine(); 
        }
        
        int price;
        Console.WriteLine("Please enter the game price");
        input = Console.ReadLine();
        while (!Int32.TryParse(input, out price) || price < 1)
        {
            Console.WriteLine("Invalid input, please enter a positive integer for the game price");
            input = Console.ReadLine();
        }
        
        int userRating;
        Console.WriteLine("Please enter the game rating (1-5)");
        input = Console.ReadLine();
        while (!Int32.TryParse(input, out userRating) || userRating < 1 || userRating > 5)
        {
            Console.WriteLine("Invalid input, please enter a number between 1 and 5");
            input = Console.ReadLine();
        }
        
        int quantity;
        Console.WriteLine("Please enter the quantity in stock");
        input = Console.ReadLine();
        while (!Int32.TryParse(input, out quantity) || quantity < 1)
        {
            Console.WriteLine("Invalid input, please enter a positive integer for the quantity");
            input = Console.ReadLine();
        }
        Game newGame = new Game(itemNumber, itemName, price, userRating, quantity);
        VideoGames.Add(newGame);
        try
        {
            File.AppendAllText(FileName,"\n"+ newGame.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

internal class Game
{
    private int ItemNumber;
    private string ItemName;
    private int Price;
    private int UserRating;
    private int Quantity;

    public Game(int itemNumber,  string itemName, int price, int userRating, int quantity)
    {
        ItemNumber =  itemNumber;
        ItemName = itemName;
        Price =  price;
        UserRating = userRating;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return ItemNumber + "," + ItemName + "," + Price + "," + UserRating + "," + Quantity;
    }
    public string ListGame()
    {
        return ItemNumber + ": " + ItemName + " - $" + Price + ", " + UserRating + " Stars, " + Quantity + " In Stock";
    }
    
    public int GetItemNumber()
    {
        return ItemNumber;
    }
    public string GetItemName()
    {
        return ItemName;
    }
    public int GetPrice()
    {
        return Price;
    }
    public int GetUserRating()
    {
        return UserRating;
    }
    public int GetQuantity()
    {
        return Quantity;
    }
}
