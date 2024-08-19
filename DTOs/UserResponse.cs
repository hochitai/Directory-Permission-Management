using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.DTOs
{
    public class UserResponse
    {
        public UserResponse(int id, string name, string? token)
        {
            Id = id;
            Name = name;
            Token = token;
        }

        public UserResponse(User user)
        {
            Id = user.Id;
            Name = user.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Token { get; set; }
    }
}
