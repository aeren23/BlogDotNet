﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.EntityLayer.DTOs.Categories
{
    public class CategoryUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
