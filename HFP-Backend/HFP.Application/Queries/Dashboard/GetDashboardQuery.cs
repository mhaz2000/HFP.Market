using HFP.Application.DTO;
using HFP.Shared.Abstractions.Queries;

namespace HFP.Application.Queries.Dashboard
{
    public record GetDashboardQuery : IQuery<DashboardDto>;
}
