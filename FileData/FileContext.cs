using System.Text.Json;
using Domain.Models;

namespace FileData;

/// Class for accessing data stored in a JSON file
public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;

    /// Gets the collection of users stored in the data file
    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }

    /// Gets the collection of posts stored in the data file
    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return dataContainer!.Posts;
        }
    }

    /// Loads data from the JSON file into the data container
    private void LoadData()
    {
        if (dataContainer != null) return;

        if (!File.Exists(filePath))
        {
            dataContainer = new DataContainer
            {
                Users = new List<User>(),
                Posts = new List<Post>()
            };
            return;
        }

        var content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);

        // Ensure both Users and Posts are initialized
        if (dataContainer.Users == null) dataContainer.Users = new List<User>();

        if (dataContainer.Posts == null) dataContainer.Posts = new List<Post>();
    }

    /// Saves changes made to the data container back to the JSON file
    public void SaveChanges()
    {
        var serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}