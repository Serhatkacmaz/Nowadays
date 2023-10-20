using Nowadays.Core.Models;
using Nowadays.Core.Repositories;

namespace Nowadays.Repository.Repositories;

public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(NowadaysContext context) : base(context)
    {
    }
}
