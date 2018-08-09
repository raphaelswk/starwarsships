using System.ComponentModel.DataAnnotations;

namespace SWSC.Presentation.MVC.ViewModels
{
    public class DistanceVM
    {
        [Required]
        [Display(Name = "Distance in MGLT")]
        public decimal MGLT { get; set; }        
    }
}
