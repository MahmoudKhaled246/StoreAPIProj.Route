﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Route.Core.Entities
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
