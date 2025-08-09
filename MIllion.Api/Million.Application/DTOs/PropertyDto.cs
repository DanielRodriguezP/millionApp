using Million.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Application.DTOs
{
    public class PropertyDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        [MaxLength(4)]
        public string Year { get; set; }

        [ForeignKey("Owner")]
        public Guid? OwnerId { get; set; }
        public virtual Owner? Owner { get; set; }

        public virtual ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();
        public virtual ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();
    }
}
