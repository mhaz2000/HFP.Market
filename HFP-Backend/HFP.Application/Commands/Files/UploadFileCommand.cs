using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Files
{
    public record UploadFileCommand(MemoryStream File) : ICommand<Guid>;
}
