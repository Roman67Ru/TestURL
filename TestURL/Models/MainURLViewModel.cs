using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestURL.Models
{
    [Class(Table = "mainurl")]
    public class MainURLViewModel
    {
        [Id(0, Name = "idMainURL")]
        public virtual int idMainURL { get; set; }
        [Property]
        public virtual string LongUrl { get; set; }
        [Property]
        public virtual string ShortUrl { get; set; }
        [Property]
        public virtual DateTime DateAdd { get; set; }
        [Property]
        public virtual int Count { get; set; }
    }
}
