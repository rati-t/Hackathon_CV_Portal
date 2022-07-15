﻿using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.Vacancies.Queries;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Vacancies.Commands;
using Hackathon_CV_Portal.Domain.Vcancies;
using Hackathon_CV_Portal.Web.Models.VacancyModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Web.Controllers.Vacancies
{
    public class VacancyController : BaseController
    {
        private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService, SignInManager<ApplicationUser> signInManager) : base(signInManager)
        {
            _vacancyService = vacancyService;
        }

        public async Task<IActionResult> Index(string searchCategory, string vacancyType, int page = 1, int companyId = 0)
        {
            Expression<Func<Vacancy, bool>>? expression = null;
            if (!String.IsNullOrEmpty(searchCategory))
            {
                if (companyId == 0)
                    expression = x => x.Category.Name.Contains(searchCategory);
                else
                    expression = x => x.Category.Name.Contains(searchCategory) && x.UserId == companyId;
            }

            if (!String.IsNullOrEmpty(vacancyType))
            {
                if (companyId == 0)
                    expression = x => x.Type == Enum.Parse<VacancyType>(vacancyType);
                else
                    expression = x => x.Category.Name.Contains(searchCategory) && x.UserId == companyId;
            }

            if (String.IsNullOrEmpty(searchCategory) && String.IsNullOrEmpty(vacancyType) && companyId != 0)
                expression = x => x.UserId == companyId;

            LoadUserModel();

            if (UserModel?.UserId != companyId && companyId != 0)
                return RedirectToAction("AccessDenied", "Account");

            var query = new ListVacancyQuery()
            {
                Page = page,
                Expression = expression,
                UserModel = UserModel,
            };

            var result = await _vacancyService.ListVacancyQuery(query);

            if (result == null)
                return RedirectToAction("Index", "NotFound");

            return View(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var result = await _vacancyService.GetVacancyById(id);
            if (result == null)
                return RedirectToAction("NotFound", "Home");

            return View(result);
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> AddFavourite(int id)
        {
            var returnAcction = "AddFavourite";
            var returnController = "Vacancy";
            var routeiD = id.ToString();

            if (!IsSignedId())
                return RedirectToAction("LogIn", "Account", new { returnAcction, returnController, routeiD });

            LoadUserModel();
            var command = new AddFavouriteCommand()
            {
                VacasnyId = id,
                UserModel = UserModel
            };

            await _vacancyService.AddFavourite(command);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFavourite(int id)
        {
            var returnAcction = "emoveFavourite";
            var returnController = "Vacancy";
            var routeiD = id.ToString();

            if (!IsSignedId())
                return RedirectToAction("LogIn", "Account", new { returnAcction, returnController, routeiD });

            LoadUserModel();
            var command = new RemoveFavouriteCommand()
            {
                VacasnyId = id,
                UserModel = UserModel
            };

            await _vacancyService.RemoveFavourite(command);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateVacancyDTO model)
        {
            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

            var command = new CreateVacancyCommand()
            {
                Title = model.Title,
                Description = model.Description,
                DeadLine = model.DeadLine,
                CategoryId = model.CategoryId,
                Currency = model.Currency,
                //Salary = model.Salary,
                UserId = UserModel.UserId,
                UserName = UserModel.UserName
            };

            await _vacancyService.CreateVacancy(command);

            return RedirectToAction("Index");
        }
    }
}
