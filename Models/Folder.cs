namespace DirectoryPermissionManagement.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParrentFolderId { get; set; }
        public int DriveId { get; set; }
    }
}
