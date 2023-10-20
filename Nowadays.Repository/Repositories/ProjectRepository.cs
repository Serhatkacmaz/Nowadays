using Nowadays.Core.Models;
using Nowadays.Core.Repositories;

namespace Nowadays.Repository.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(NowadaysContext context) : base(context)
    {
    }
}
