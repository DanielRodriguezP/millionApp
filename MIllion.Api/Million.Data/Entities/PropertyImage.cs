using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Data.Entities
{
    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public Guid IdPropertyImage { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }

        [ForeignKey("Property")]
        public Guid? PropertyId { get; set; }
        public virtual Property? Property { get; set; }

    }
}
