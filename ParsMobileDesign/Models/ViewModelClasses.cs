using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsMobileDesign.Models
{
    public class PortfolioAndImages
    {
        public IEnumerable<Portfolio> PortfolioItems { get; set; }
        public PortfolioImage PortfolioImage { get; set; }
    }
    public class VmEmployeeExperience
    {
        public Employee Employee { get; set; }
        public EmployeeExperience Experience { get; set; }
    }
    public class VmContactMessage
    {
        public CompanyInfo CompanyInfo { get; set; }
        public Message Message { get; set; }
        public string  msg { get; set; }
    }
}
