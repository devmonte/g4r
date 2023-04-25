using Azure.Identity;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;

public class UserRepository : IUserRepository
{
    private readonly CosmosClient _client;
    private Microsoft.Azure.Cosmos.Container _usersContainer;

    public UserRepository()
    {
        _client = new(Environment.GetEnvironmentVariable("COSMOS_ENDPOINT"));
        //_ = _client.CreateDatabaseIfNotExistsAsync("G4R").Result;
        _usersContainer = _client.GetContainer("G4R", "Users");
    }

    public async Task<User> Add(User user)
    {
        var result = await _usersContainer.CreateItemAsync(user);
        return result.Resource;
    }

    public async Task<List<User>> GetAll()
    {
        _usersContainer = _client.GetContainer("G4R", "Users");
        var users = new List<User>();
        // Create query using a SQL string and parameters
        var query = new QueryDefinition(
            query: "SELECT * FROM Users"
        );

        using FeedIterator<User> feed = _usersContainer.GetItemQueryIterator<User>(
            queryDefinition: query
        );

        while (feed.HasMoreResults)
        {
            FeedResponse<User> response = await feed.ReadNextAsync();
            foreach (User item in response)
            {
                users.Add(item);
            }
        }
        return users;
    }
}