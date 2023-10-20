using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Core.Models;

public class Company : BaseEntity
{
    public virtual ICollection<Project> Projects { get; set; }
    public virtual ICollection<Issue> Issues { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }

    public Company()
    {
        Projects = new HashSet<Project>();
        Issues = new HashSet<Issue>();
        Employees = new HashSet<Employee>();
    }
}
