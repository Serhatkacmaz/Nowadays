using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Core.Models;

public class Employee : BaseEntity
{
    public int CompanyId { get; set; }
    public string TC { get; set; }
    public string UserName { get; set; }
    public string LastName { get; set; }

    public virtual Company? Company { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
    public virtual ICollection<Issue> Issues { get; set; }

    public Employee()
    {
        TC = string.Empty;
        UserName = string.Empty;
        LastName = string.Empty;

        Projects = new HashSet<Project>();
        Issues = new HashSet<Issue>();
    }
}
