﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuizaarDesktopUI.Library.Models
{
    public class CategoryShallowDTO : ICategoryDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}