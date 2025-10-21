namespace HFP.Infrastructure.EF.Models
{
    internal class WarehousemanReadModel
    {
        public required string Name { get; set; }
        public required string UId { get; set; }

        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }

    }
}
