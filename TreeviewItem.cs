using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeView.Models;

namespace TreeView
{
    public class TreeviewItem
    {
        
            public int MenuId { get; set; }
           
            public string MenuName { get; set; }
        public int? ParentId { get; set; }
        public List<TreeviewItem> Children { get; set; }
     }

    public class EmployeeHierarchy
    {
        public Menu Employee { get; set; }
        public IEnumerable<EmployeeHierarchy> Employees { get; set; }
    }
}
