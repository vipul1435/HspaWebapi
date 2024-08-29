using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Modals
{ 
    [Table("Photos")]
    public class Photo:BaseEntity
    {
        public required string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}