using System.ComponentModel.DataAnnotations;

namespace Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table(nameof(Salary), Schema = "acc")]
    public class Salary :
        SeedWork.Entity,
        SeedWork.Abstractions.IEntityHasIsDelete,
        SeedWork.Abstractions.IEntityHasUpdateDateTime
    {
        public Salary() : base()
        {
            UpdateDateTime = Utility.DateTime.Now;
        }

        // **********
        // **********
        // **********
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Personnel))]
        [Required
            (ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public System.Guid IdPersonnel { get; set; }

        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Personnel))]
        public virtual Personnel? Personnel { get; set; }
        // **********
        // **********
        // **********


        /// <summary>
        /// حقوق پایه
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.BasicSalary))]
        [Required
            (ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public decimal BasicSalary { get; set; }


        /// <summary>
        /// فوق العاده حق جذب
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Allowance))]
        [Required
            (ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public decimal Allowance { get; set; }


        /// <summary>
        /// حق ایاب و ذهاب
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Transportation))]
        [Required
            (ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public decimal Transportation { get; set; }


        /// <summary>
        /// مالیات
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Tax))]
        [Required
            (ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public decimal Tax { get; set; }


        /// <summary>
        /// تاریخ حقوق 
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Date))]
        [Required
            (ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public DateTime Date { get; set; }


        /// <summary>
        /// مبلغ اضافه کار
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.OverTimeAmount))]
        [Required
            (ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public decimal OverTimeAmount { get; set; }


        /// <summary>
        /// مبلغ نهایی حقوق 
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.OverTimeAmount))]
        [Required
            (ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        public decimal FinalSalaryAmount { get; set; }


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
    }
}
