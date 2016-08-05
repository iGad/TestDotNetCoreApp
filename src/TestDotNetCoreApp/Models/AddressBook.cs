using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestDotNetCoreApp.Models
{
    public class AddressBook
    {
        [Key]
        [Display(Name = "ИД")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string FIO { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Эл. почта")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Подразделение")]
        public string Subdivision { get; set; }

        [Required]
        [Display(Name = "Должность")]
        public string Position { get; set; }
    }
}
