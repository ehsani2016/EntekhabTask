using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PersonnelRepository : Base.Repository<Models.Personnel>, IPersonnelRepository
    {
        internal PersonnelRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }


        public async Task<Guid?> GetPerson(string firstName, string lastName)
        {
            var result = await DbSet
                .Where(c =>
                    c.FirstName == firstName &&
                    c.LastName == lastName)
                .FirstOrDefaultAsync();

            return result?.Id;
        }


    }
}
