namespace HFP.Application.DTO
{
    public record ConsumerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UId { get; set; }
        public bool IsWarehouseman { get; set; }
    }
}
