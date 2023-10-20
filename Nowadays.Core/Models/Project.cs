using Nowadays.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Core.Models;

public class Project : BaseEntity
{
    public int CompanyId { get; set; }
    public ProjectStatus Status { get; set; }

    public virtual Company? Company { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual ICollection<Issue> Issues { get; set; }

    public Project()
    {
        Status = ProjectStatus.Approved;

        Employees = new HashSet<Employee>();
        Issues = new HashSet<Issue>();
    }
}
