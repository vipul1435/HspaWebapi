namespace webApi.Dtos
{
    public class PhotoDto
    {
        public required string PublicId { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsPrimary { get; set; } 
    }
}
