using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Kickit.Models
{
    public class EmailFormModel
    {        
       [Required, Display(Name = "Who are you?")]
       public string FromName { get; set; }
       [Required, Display(Name = "Whats your email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required,Display(Name ="Who do you want to Kickit with?")] 
        public string ReceiverName { get; set; }
        [Required  Display(Name = "Enter Email"),]
        public string ReceiverEmail { get; set; }
        [Required  Display(Name = "Enter Date"),]
        public string Date { get; set; }
       // [Required  Display(Name = "Enter Time"),]
       // public string Time { get; set; }
        [Required]
        public string Message { get; set; }
        public string Date2 { get; set; }
        public string Date3 { get; set; }
        public string unavailable { get; set; }
        [Required(ErrorMessage = "Please enter your zipcode")]
        public float ZipCode { get; set; }
    }
}