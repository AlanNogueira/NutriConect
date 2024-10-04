using NutriConect.Business.Entities;
using NutriConect.Business.InputModels.Professional;
using NutriConect.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Mappings
{
    public static class ProfessionalMapping
    {
        public static Professional CreateProfessionalToProfessional(CreateProfessionalInputModel createProfessional)
        {
            return new Professional
            {
                Name = createProfessional.Name,
                Phone = createProfessional.Phone,
                CreateDate = DateTime.Now,
                Address = new Address
                {
                    City = createProfessional.Address.City,
                    State = createProfessional.Address.State,
                    Street = createProfessional.Address.Street,
                    Neighborhood = createProfessional.Address.Neighborhood,
                    ZipCode = createProfessional.Address.ZipCode,
                    Number = createProfessional.Address.Number,
                },
                User = new ApplicationUser
                {
                    UserName = createProfessional.Email,
                    Email = createProfessional.Email,
                }
            };
        }

        public static void UpdateProfessionalToProfessional(UpdateProfessionalInputModel updateProfessional, Professional professional) 
        {
            professional.Name = updateProfessional.Name;
            professional.Phone = updateProfessional.Phone;

            professional.Address.City = updateProfessional.Address.City;
            professional.Address.State = updateProfessional.Address.State;
            professional.Address.Neighborhood = updateProfessional.Address.Neighborhood;
            professional.Address.Street = updateProfessional.Address.Street;
            professional.Address.Number = updateProfessional.Address.Number;
            professional.Address.ZipCode = updateProfessional.Address.ZipCode;
        }

        public static ProfessionalViewModel? ProfessionalToViewModel(Professional? professional)
        {
            return professional is null ? null : new ProfessionalViewModel
            {
                Id = professional.Id,
                Name = professional.Name,
                Phone = professional.Phone,
                Address = new AddressViewModel
                {
                    Id = professional.Address.Id,
                    City = professional.Address.City,
                    State = professional.Address.State,
                    Neighborhood = professional.Address.Neighborhood,
                    Street = professional.Address.Street,
                    Number = professional.Address.Number,
                    ZipCode = professional.Address.ZipCode
                },
                User = new UserViewModel
                {
                    Id = professional.User.Id,
                    Email = professional.User.Email
                }
            };
        }
    }
}
