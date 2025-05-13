namespace HFP.Application.DTO
{
    public record UserDto
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
    }
}
