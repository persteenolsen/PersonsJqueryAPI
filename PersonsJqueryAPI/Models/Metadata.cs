using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PersonsJqueryAPI.Models
{
    public class Metadata
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(20, ErrorMessage = "Name cannot be longer than 20 characters!")]
        public string Navn { get; set; }

        [Required(ErrorMessage = "LastName is required!")]
        [MaxLength(20, ErrorMessage = "LastName cannot be longer than 20 characters!")]
        public string Efternavn { get; set; }

        [Required(ErrorMessage = "Age is required!")]
        [Range(18, 99, ErrorMessage = "Sorry, age must be between 18 and 99!")]
        public string Alder { get; set; }

        public string Editable { get; set; }
    }
}