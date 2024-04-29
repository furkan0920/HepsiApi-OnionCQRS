﻿using Hepsi.Domain.Common;
using Hepsi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsi.Domain.Entities
{
    public class Category : EntityBase
    {//requried:Nesen oluşturduğumuzda en başta doldurmamız gerek
        public Category()
        {

        }

        public Category(int parentId, string name, int priorty)
        {
            ParentId = parentId;
            Name = name;
            Priorty = priorty;
        }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Priorty { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
