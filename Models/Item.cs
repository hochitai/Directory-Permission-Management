namespace DirectoryPermissionManagement.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int FolderId { get; set; }
        public int DriveId {  get; set; }
    }
}
