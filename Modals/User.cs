namespace webApi.Modals
{
    public class User:BaseEntity
    {
        public required string UserName { get; set; }
        public required byte[] Password { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordKey { get; set; }
    }
}
