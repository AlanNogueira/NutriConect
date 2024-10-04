using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Data.Repository
{
    public class TipEvaluationRepository : BaseRepository<TipEvaluation>, ITipEvaluationRepository
    {
        public TipEvaluationRepository(ContextBase db) : base(db) { }
    }
}
