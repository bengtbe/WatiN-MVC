using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WatiN_MVC.SampleSite.Models
{
    public class Profile
    {
        [Required]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public int? Age { get; set; }

        public string Email {get; set;}

        public string Website {get; set;}

        public Company Company {get; set;}

        public string Location {get; set;}
    }

    public enum Gender
    {
        Male,
        Female
    }
}