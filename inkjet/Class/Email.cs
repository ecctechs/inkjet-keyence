using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace inkjet.Class
{
    public class Email
    {
        [Name(name: "No.")]
        public int EmailNo { get; set; }

        [Name(name: "Email")]
        public string EmailName { get; set; }

    }
}
