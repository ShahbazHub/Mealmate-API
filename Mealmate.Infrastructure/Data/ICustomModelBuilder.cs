using Microsoft.EntityFrameworkCore;

namespace Mealmate.Infrastructure.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
