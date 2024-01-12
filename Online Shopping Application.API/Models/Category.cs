using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_Application.API.Models
{
    public partial class Category
    {
        [Key]

        public int ID{ get; set; }


        public string? Name { get; set; }


        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual UserLogin? CreatedByNavigation { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual UserLogin? UpdatedByNavigation { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
