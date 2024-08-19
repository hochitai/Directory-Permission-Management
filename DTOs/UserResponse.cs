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

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Token { get; set; }
    }
}
