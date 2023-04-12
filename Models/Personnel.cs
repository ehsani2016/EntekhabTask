using System.ComponentModel.DataAnnotations;

namespace Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table(nameof(Personnel), Schema = "prs")]
    public class Personnel :
        SeedWork.Entity,
        SeedWork.Abstractions.IEntityHasIsActive,
        SeedWork.Abstractions.IEntityHasIsDelete,
        SeedWork.Abstractions.IEntityHasUpdateDateTime
    {
        public Personnel(string firstName, string lastName) : base()
        {
            UpdateDateTime = Utility.DateTime.Now;
            FirstName = firstName;
            LastName = lastName;

            Salaries = new List<Salary>();
        }


        /// <summary>
        /// نام 
        /// </summary>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.FirstName))]
        [StringLength(maximumLength: 255,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.MaxLength))]
        public string FirstName { get; set; }


        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.LastName))]
        [StringLength(maximumLength: 255,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.MaxLength))]
        public string LastName { get; set; }


        /// <summary>
        /// فعال؟
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.IsActive))]
        public bool IsActive { get; set; }


        /// <summary>
        /// حذف شده؟
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.IsDeleted))]
        public bool IsDeleted { get; set; }


        /// <summary>
        /// زمان حذف
        /// </summary>
        public DateTime? DeleteDateTime { get; private set; }


        /// <summary>
        /// زمان بروزرسانی
        /// </summary>
        public DateTime UpdateDateTime { get; }


        /// <summary>
        /// زمان حذف 
        /// </summary>
        public void SetDeleteDateTime()
        {
            if (IsDeleted)
            {
                DeleteDateTime = Utility.DateTime.Now;
            }
        }



        public virtual IList<Salary> Salaries { get; private set; }

    }
}

