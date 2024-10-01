using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Mappings
{
    public static class ClientMapping
    {
        public static Client CreateClientToClient(CreateClientUserInputModel createClient)
        {
            return new Client
            {
                Name = createClient.Name,
                Phone = createClient.Phone,
                CreateDate = DateTime.Now,
                Address = new Address
                {
                    City = createClient.Address.City,
                    State = createClient.Address.State,
                    Street = createClient.Address.Street,
                    Neighborhood = createClient.Address.Neighborhood,
                    ZipCode = createClient.Address.ZipCode,
                    Number = createClient.Address.Number,
                    CreateDate = DateTime.Now
                },
                User = new ApplicationUser
                {
                    UserName = createClient.Email,
                    Email = createClient.Email,
                    CPF = createClient.CPF
                }
            };
        }
    }
}
