using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Mealmate.Infrastructure.Repository.Base;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Repository
{
    public class QRCodeRepository : Repository<QRCode>, IQRCodeRepository
    {
        public QRCodeRepository(MealmateContext context)
            : base(context)
        {
        }

    }
}
