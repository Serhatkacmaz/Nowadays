using AutoMapper;
using Nowadays.Core.Dtos;
using Nowadays.Core.Models;
using Nowadays.Core.Repositories;
using Nowadays.Core.Services;
using Nowadays.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Service.Services;

public class IssueService : Service<Issue, IssueDto>, IIssueService
{
    public IssueService(IGenericRepository<Issue> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
    {
    }
}
