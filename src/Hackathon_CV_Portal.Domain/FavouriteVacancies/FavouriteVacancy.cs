﻿using Hackathon_CV_Portal.Domain.FavouriteVacancies.Commands;
using System.ComponentModel.DataAnnotations;

namespace Hackathon_CV_Portal.Domain.FavouriteVacancies
{
    public class FavouriteVacancy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; private set; }
        [Required]
        public int VacansyId { get; private set; }

        //public ApplicationUser User { get; private set; }
        //public Vacancie Vacancie { get; private set; }

        public FavouriteVacancy() { }
        public FavouriteVacancy(AddFavouriteCommand command)
        {
            UserId = command.UserModel.UserId;
            VacansyId = command.VacasnyId;
        }

    }
}
