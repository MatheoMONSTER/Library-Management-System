using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Database
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
