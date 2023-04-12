namespace Data
{
    public interface IPersonnelRepository : Base.IRepository<Models.Personnel>
    {
        Task<Guid?> GetPerson(string firstName, string lastName);
    }
}
