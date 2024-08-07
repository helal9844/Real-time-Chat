using System.ComponentModel.DataAnnotations.Schema;

namespace Chat_DAL
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        public int ChatUserId { get; set; }
        public ChatUser ChatUser { get; set; }
    }
}