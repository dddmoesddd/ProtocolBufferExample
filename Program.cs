using Serilog;

// Ensure the correct Serilog sink for console logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Use the console for logging
    .CreateLogger();

try
{
    // Simulated user dictionary to store users
    var users = new Dictionary<int, User>();
    bool running = true;

    while (running)
    {
        Console.WriteLine("Select an operation:");
        Console.WriteLine("1. Create User");
        Console.WriteLine("2. Delete User");
        Console.WriteLine("3. Display Users");
        Console.WriteLine("4. Exit");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Create User
                CreateUser(users);
                break;

            case "2":
                // Delete User
                DeleteUser(users);
                break;

            case "3":
                // Display Users
                DisplayUsers(users);
                break;

            case "4":
                // Exit
                running = false;
                break;

            default:
                Console.WriteLine("Invalid choice. Please select again.");
                break;
        }
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred.");
    Console.WriteLine("An unexpected error occurred. Please try again later.");
}
finally
{
    Log.CloseAndFlush();
}

// CRUD Operations

void CreateUser(Dictionary<int, User> users)
{
    try
    {
        Console.WriteLine("Enter user ID:");
        int id = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter user name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter family name:");
        string familiy = Console.ReadLine();

        Console.WriteLine("Enter national code:");
        int nationalcode = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter datetime (e.g., 2025-01-11T15:30:00):");
        string datetime = Console.ReadLine();

        var newUser = new User { Id = id, Name = name, Familiy = familiy, Nationalcode = nationalcode, Datetime = datetime };

        if (users.ContainsKey(id))
        {
            Console.WriteLine("Error: User with this ID already exists.");
        }
        else
        {
            users[id] = newUser;
            Console.WriteLine($"User {name} created successfully.");
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error while creating user.");
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void DeleteUser(Dictionary<int, User> users)
{
    try
    {
        Console.WriteLine("Enter user ID to delete:");
        int id = int.Parse(Console.ReadLine());

        if (users.ContainsKey(id))
        {
            users.Remove(id);
            Console.WriteLine($"User with ID {id} deleted successfully.");
        }
        else
        {
            Console.WriteLine($"No user found with ID {id}.");
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error while deleting user.");
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void DisplayUsers(Dictionary<int, User> users)
{
    if (users.Count == 0)
    {
        Console.WriteLine("No users found.");
    }
    else
    {
        Console.WriteLine("Users:");
        foreach (var user in users.Values)
        {
            Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Family: {user.Familiy}, National Code: {user.Nationalcode}, Date: {user.Datetime}");
        }
    }
}
