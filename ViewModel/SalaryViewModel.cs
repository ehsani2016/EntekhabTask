using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class SalaryViewModel
    {
        public SalaryViewModel() : base()
        {
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
        [MaxLength(255,
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
        [MaxLength(255,
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.MaxLength))]
        public string LastName { get; set; }


        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.DisplayName))]
        public string DisplayName => $"{FirstName} {LastName}";


        /// <summary>
        /// حقوق پایه
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        [Display(ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.BasicSalary))]
        public decimal BasicSalary { get; set; }


        /// <summary>
        /// فوق العاده حق جذب
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        [Display(ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.Allowance))]
        public decimal Allowance { get; set; }


        /// <summary>
        /// حق ایاب و ذهاب
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        [Display(ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.Transportation))]
        public decimal Transportation { get; set; }


        /// <summary>
        /// مالیات
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        [Display(ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.Tax))]
        public decimal Tax { get; set; }


        /// <summary>
        /// تاریخ حقوق 
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(Resources.Messages),
            ErrorMessageResourceName = nameof(Resources.Messages.Required))]
        [Display(ResourceType = typeof(Resources.DataDictionary),
         Name = nameof(Resources.DataDictionary.Date))]
        public DateTime Date { get; set; }

    }
}
