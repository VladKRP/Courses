

using System.ComponentModel.DataAnnotations;

namespace BestTickets.Models
{
    public class RouteViewModel
    {
        [Required]
        [Display(Name = "Откуда")]
        public string DeparturePlace { get; set; }
        [Required]
        [Display(Name = "Куда")]
        public string ArrivalPlace { get; set; }
        [Required]
        [Display(Name = "Дата")]
        public string Date { get; set; }
    }
}