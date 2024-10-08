﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Entities
{
    public class Professional : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public ApplicationUser User { get; set; }
        public List<ProfessionalEvaluation> ProfessionalEvaluations { get; set; }
    }
}
