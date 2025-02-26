﻿using Hackathon_CV_Portal.Domain.Enums;

namespace Hackathon_CV_Portal.Domain.Vacancies.Commands
{
    public class CreateVacancyCommand
    {
        public int CategoryId { get; set; }
        public VacancyType Type { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public string SalaryRange { get; set; }
        public DateTime DeadLine { get; set; }
        public string Description { get; set; }
        public string Responsibility { get; set; }
        public string Qualifications { get; set; }
        public UserModel UserModel { get; set; }
    }
}
