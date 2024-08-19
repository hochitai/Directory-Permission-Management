using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.DTOs
{
    [Keyless]
    public class UserRequest
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
