﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class DogProfileViewModel
    {
        public Dog Dog { get; set; }
        public List<Walk> Walks { get; set; }
    }
}
