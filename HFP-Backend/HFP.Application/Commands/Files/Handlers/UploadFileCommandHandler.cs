using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Files.Handlers
{
    internal class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, Guid>
    {
        public async Task<Guid> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var path = Directory.GetCurrentDirectory() + "/FileStorage";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileId = Guid.NewGuid();

            var dir = Path.Combine(path, $"{fileId}.dat");

            using (var fileStream = new FileStream(dir, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
            {
                request.File.Position = 0;
                await request.File.CopyToAsync(fileStream);
            }

            return fileId;
        }
    }
}
