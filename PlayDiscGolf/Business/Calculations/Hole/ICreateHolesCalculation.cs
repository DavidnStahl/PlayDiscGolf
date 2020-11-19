using PlayDiscGolf.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Business.Calculations.Hole
{
    public interface ICreateHolesCalculation
    {
        public CreateHolesViewModel ConfigureHoles(CreateHolesViewModel model);

    }
}
