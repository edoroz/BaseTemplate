using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTemplate.Data.Models {
    public abstract class BaseEntity {
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string   RegisterUser { get; set; } = string.Empty;
        public bool     IsActive     { get; set; } = true;
        public DateTime LastUpdate   { get; set; } = DateTime.Now;
        public string   UserUpdate   { get; set; } = string.Empty;
    }
}
