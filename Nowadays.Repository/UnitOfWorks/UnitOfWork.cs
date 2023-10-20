using Nowadays.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Repository.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly NowadaysContext _context;

    public UnitOfWork(NowadaysContext context)
    {
        _context = context;
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
