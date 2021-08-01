namespace GFCA.APT.Domain.Models
{
    public class FileModel
    {
        public long? FileId { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
}
