using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SeedWork
{
    public abstract class Entity:object,
        Abstractions.IEntity<Guid>
    {
        public Entity():base()
        {
            InsertDateTime = 
                DateTime.Now;

            Id = 
                Guid.NewGuid();
        }

        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Id))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }


        [Display(ResourceType = typeof(Resources.DataDictionary),
           Name = nameof(Resources.DataDictionary.InsertDateTime))]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime InsertDateTime { get; set; }
    }
}
