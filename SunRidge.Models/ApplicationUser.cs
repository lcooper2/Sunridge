using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sunridge.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "N/A")]
        public string Occupation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}",
            ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public DateTime? Birthday { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string Email { get; set; }

        [Display(Name = "Emergency Contact")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "Emergency Contact #")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[None listed]")]
        public string EmergencyContactPhone { get; set; }

        [Display(Name = "Receive Sunridge Emails")]
        public bool? ReceiveEmails { get; set; }

        [Display(Name = "Archived")]
        public bool IsArchive { get; set; } = false;

        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        [NotMapped]
        public List<UserPhotos> Images { get; set; }

        //[Display(Name = "Address")]
        //public int AddressId { get; set; }

        //[ForeignKey("Address")]
        //public virtual Address Address { get; set; }

        //Navigation properties
        //public virtual ICollection<OwnerLot> OwnerLots { get; set; }

        //public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual ICollection<OwnerHistory> OwnerHistories { get; set; }
        //public virtual ICollection<FormResponse> FormResponses { get; set; }
        //public virtual ICollection<ClassifiedListing> ClassifiedListings { get; set; }
        //public virtual ICollection<KeyHistory> KeyHistories { get; set; }

        // Calculated properties

        // [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        
    }
}
