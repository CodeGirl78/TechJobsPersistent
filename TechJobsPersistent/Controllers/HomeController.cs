using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            AddJobViewModel addJobViewModel = new AddJobViewModel(context.Employers.ToList(), context.Skills.ToList());
            return View(addJobViewModel);
        }

        [HttpPost("/Add")]
        public IActionResult ProcessAddJobForm([Bind("Name,EmployerId")] AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Employer employer = context.Employers.Find(addJobViewModel.EmployerId);
                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    Employer = employer
                };

                foreach (var skill in selectedSkills)
                {
                    JobSkill newJobSkill = new JobSkill
                    {
                        JobId = newJob.Id,
                        Job = newJob,
                        SkillId = int.Parse(skill)
                    };
                    context.JobSkills.Add(newJobSkill);
                }

                context.Jobs.Add(newJob);
                context.SaveChanges();
                return Redirect("/Home");
            }
            return View("AddJob", addJobViewModel);
        }

            public IActionResult Detail(int id)
            {
                Job theJob = context.Jobs
                    .Include(j => j.Employer)
                    .Single(j => j.Id == id);

                List<JobSkill> jobSkills = context.JobSkills
                    .Where(js => js.JobId == id)
                    .Include(js => js.Skill)
                    .ToList();

                JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
                return View(viewModel);
            }
        }
    }