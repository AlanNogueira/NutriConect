using NutriConect.Business.Entities;
using NutriConect.Business.InputModels;
using NutriConect.Business.InputModels.Tip;
using NutriConect.Business.ViewModels;
using NutriConect.Business.ViewModels.Tip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Mappings
{
    public static class TipMapping
    {
        public static TipEvaluation CreateTipEvaluationToTipEvaluation(TipEvaluationInputModel tipEvaluation, Tip tip, Client? client, Professional? professional)
        {
            return new TipEvaluation
            {
                Title = tipEvaluation.Title,
                Text = tipEvaluation.Text,
                Value = tipEvaluation.Value,
                Tip = tip,
                Client = client,
                Professional = professional
            };
        }

        public static Tip CreateTipToTip(CreateTipInputModel createTip, Client? client = null, Professional? professional = null)
        {
            return new Tip
            {
                Title = createTip.Title,
                Text = createTip.Text,
                Client = client,
                Professional = professional
            };
        }

        public static TipByUserViewModel TipToTipByUserViewModel(Tip tip)
        {
            return new TipByUserViewModel 
            {
                Id = tip.Id,
                Text = tip.Text,
                Title = tip.Title,
            };
        }

        public static TipViewModel TipToViewModel(Tip tip)
        {
            return new TipViewModel
            {
                Id = tip.Id,
                Title = tip.Title,
                Text = tip.Text,
                Client = ClientMapping.ClienteToViewModel(tip.Client),
                Professional = ProfessionalMapping.ProfessionalToViewModel(tip.Professional)
            };
        }

        public static void UpdateTipToTip(UpdateTipInputMode updateTip, Tip tip)
        {
            tip.Title = updateTip.Title;
            tip.Text = updateTip.Text;
        }
    }
}
