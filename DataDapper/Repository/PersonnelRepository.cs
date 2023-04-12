namespace DataDapper
{
    public class PersonnelRepository : Base.Repository<Models.Personnel>, IPersonnelRepository
    {
        internal PersonnelRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
