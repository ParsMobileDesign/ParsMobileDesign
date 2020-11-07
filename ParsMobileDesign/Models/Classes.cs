using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParsMobileDesign.Models
{
    public class Portfolio
    {
        public Portfolio()
        {
            Title = "";
            Type = "";
            Description = "";
        }
        [Key]
        public int PortfolioId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        [MaxLength(50)]
        [Required]

        public string Type { get; set; }
        [MaxLength(500)]
        [Required]

        public string Description { get; set; }
        [MaxLength(30)]
        public string WebsiteAddr { get; set; }
        public List<PortfolioImage> Images { get; set; }

    }
    public class PortfolioImage
    {
        [Key]
        public int Id { get; set; }

        public string ImageAddr { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

    }
    public class Message
    {
        public Message()
        {
            Name = "";
            Email = "";
            MessageText = "";
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name ="Your name")]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        public string MessageText { get; set; }
    }
    public class CompanyInfo
    {
        public CompanyInfo()
        {
            CorpTitle = "";
            Email = "";
            Tel = "";
            Address = "";
            Description = "";
        }
        [Key]
        public byte Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string CorpTitle { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
    public class Employee
    {
        public Employee()
        {
            Fname = "";
            LName = "";
            Email = "";
            Tel = "";
            Address = "";
            Description = "";
            Picture = new byte[10];
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Fname { get; set; }
        [MaxLength(50)]
        [Required]
        public string LName { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        [MaxLength(50)]
        [Required]
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public List<EmployeeSpecialty> Specialities { get; set; }
        public List<EmployeeExperience> Experiences { get; set; }
    }
    public class EmployeeSpecialty
    {
        public EmployeeSpecialty()
        {
            Title = "";
            Domain = "";
        }
        public EmployeeSpecialty(Employee emp) : base()
        {
            EmployeeId = emp.Id;
            Employee = emp;
        }
        [Key]
        public byte Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        public string Domain { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }

    public class EmployeeExperience
    {
        public EmployeeExperience()
        {
            Period = "";
            Title = "";
            Location = "";
            Order = 0;
        }
        public EmployeeExperience(Employee emp):base()
        {
            EmployeeId = emp.Id;
            Employee = emp;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Period { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        public byte Order{ get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }

}
