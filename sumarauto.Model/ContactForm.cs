﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContactForm : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public string Product { get; set; }
        public bool Enquiry { get; set; }
        public string City { get; set; }

    }
}
