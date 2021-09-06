using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lara_VI.Entities
{
    public class InquiryHeader
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime InquiryDate { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Email { get; set; }





    }
}
