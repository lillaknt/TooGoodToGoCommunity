using System.Collections.Generic;
using Domain.Models;

namespace FileData;
/// Represents the container for user and post data
public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<Post> Posts { get; set; }
}