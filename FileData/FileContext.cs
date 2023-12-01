using System.Text.Json;
using Domain.Models;
 

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer;

 

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }
    
    public ICollection<Post> Posts
    {
        get
        {
            LoadData();
            return dataContainer!.Posts;
        }
    }
    
    
    private void LoadData()
    {
        if (dataContainer != null) return;
    
        if (!File.Exists(filePath))
        {
            dataContainer = new DataContainer()
            {
               
                
                Users = new List<User>(),
                Posts = new List<Post>()
            };
            return;
        }
        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
        
        // Ensure both Users and Posts are initialized
        if (dataContainer.Users == null)
        {
            dataContainer.Users = new List<User>();
        }

        if (dataContainer.Posts == null)
        {
            dataContainer.Posts = new List<Post>();
        }
        
    }
    
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}