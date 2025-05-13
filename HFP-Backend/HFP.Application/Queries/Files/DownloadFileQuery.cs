using HFP.Shared.Abstractions.Queries;

namespace HFP.Application.Queries.Files
{
    public record DownloadFileQuery(Guid Id) : IQuery<FileStream>;
}
