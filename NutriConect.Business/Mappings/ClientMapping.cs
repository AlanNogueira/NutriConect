using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.ViewModels;
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
                }
            };
        }

        public static void UpdateClientToClient(UpdateClientInputModel updateClient, Client client)
        {
            client.Name = updateClient.Name;
            client.Phone = updateClient.Phone;

            client.Address.City = updateClient.Address.City;
            client.Address.State = updateClient.Address.State;
            client.Address.Neighborhood = updateClient.Address.Neighborhood;
            client.Address.Street = updateClient.Address.Street;
            client.Address.Number = updateClient.Address.Number;
            client.Address.ZipCode = updateClient.Address.ZipCode;
        }

        public static ClientViewModel? ClienteToViewModel(Client? client)
        {
            return client is null ? null : new ClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                Phone = client.Phone,
                Address = new AddressViewModel
                {
                    Id = client.Address.Id,
                    City = client.Address.City,
                    State = client.Address.State,
                    Neighborhood = client.Address.Neighborhood,
                    Street = client.Address.Street,
                    Number = client.Address.Number,
                    ZipCode = client.Address.ZipCode
                },
                User = new UserViewModel
                {
                    Id = client.User.Id,
                    Email = client.User.Email,
                }
            };
        }
    }
}
