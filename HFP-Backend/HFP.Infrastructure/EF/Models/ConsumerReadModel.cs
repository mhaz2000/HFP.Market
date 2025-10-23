namespace HFP.Infrastructure.EF.Models
{
    internal class ConsumerReadModel
    {
        public required string Name { get; set; }
        public required string UId { get; set; }
        public bool IsWarehouseman { get; set; }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }

    }
}
