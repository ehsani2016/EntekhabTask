namespace Models.SeedWork.Abstractions
{
    public interface IEntityHasIsDelete
    {
        bool IsDeleted { get; set; }

        System.DateTime? DeleteDateTime { get; }

        void SetDeleteDateTime();
    }
}
