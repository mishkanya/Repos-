using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TulaEducation.Entitys
{
    [Table("EducationInfo")]
    public class EducationInfo : DataBaseEntity
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNubber { get; set; }
        public string Emain { get; set; }
        public string Location { get; set; }
    }
}
