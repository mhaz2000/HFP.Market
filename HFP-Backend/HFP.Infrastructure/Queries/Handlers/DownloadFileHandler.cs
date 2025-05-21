using HFP.Application.Queries.Files;
using HFP.Shared.Abstractions.Exceptions;
using HFP.Shared.Abstractions.Queries;

namespace HFP.Infrastructure.Queries.Handlers
{
    internal class DownloadFileHandler : IQueryHandler<DownloadFileQuery, FileStream>
    {
        public async Task<FileStream> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var path = Directory.GetCurrentDirectory() + "/FileStorage";
            var filePath = Path.Combine(path, $"{request.Id}.dat");

            if (!File.Exists(filePath))
                throw new BusinessException("فایل مورد نظر یافت نشد.");

            return await Task.FromResult(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));

        }
    }
}
