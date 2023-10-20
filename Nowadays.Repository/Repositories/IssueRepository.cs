using Nowadays.Core.Models;
using Nowadays.Core.Repositories;

namespace Nowadays.Repository.Repositories;

public class IssueRepository : GenericRepository<Issue>, IIssueRepository
{
    public IssueRepository(NowadaysContext context) : base(context)
    {
    }
}