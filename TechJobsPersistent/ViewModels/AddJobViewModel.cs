using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "Job name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employer is required")]
        public int EmployerId { get; set; }
        public List<SelectListItem> Employers { get; set; }

        [Required(ErrorMessage = "Skill is required")]
        public int SkillId { get; set; }
        public List<Skill> Skills { get; set; }

        public AddJobViewModel()
        {
        }

        public AddJobViewModel(List<Employer> employers, List<Skill> possibleSkills)
        {
            Employers = new List<SelectListItem>();
            Skills = possibleSkills;

            foreach (var employer in employers)
            {
                Employers.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                });
            }
        }
    }
}