using Nowadays.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Core.Models;

public class Issue : BaseEntity
{
    public int CompanyId { get; set; }
    public int ProjectId { get; set; }
    public IssueStatus Status { get; set; }

    public virtual Company? Company { get; set; }
    public virtual Project? Project { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }

    public Issue()
    {
        Status = IssueStatus.Approved;

        Employees = new HashSet<Employee>();
    }
}
