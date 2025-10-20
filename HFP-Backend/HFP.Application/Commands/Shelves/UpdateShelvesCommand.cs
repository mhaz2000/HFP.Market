using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Shelves
{
    public record UpdateShelvesCommand : ICommand
    {
        public ICollection<ShelvesCommand> Shelves { get; set; } = [];
    }
    public record ShelvesCommand : ICommand
    {
        public int Order { get; set; }
        public List<Guid> ProductIds { get; set; } = [];
    }
}
