﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<Question> Questions { get; set; }
    }

    public class CategoryShallowDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public CategoryShallowDTO() { }

        public CategoryShallowDTO(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }

    public class CategoryDTO : CategoryShallowDTO
    {
        public class QuestionWrapper
        {
            public long Id { get; set; }
            public string Content { get; set; }

            public QuestionWrapper(Question question)
            {
                Id = question.Id;
                Content = question.Content;
            }
        }

        public IEnumerable<QuestionWrapper> Questions { get; set; }

        public CategoryDTO(Category category) : base(category)
        {
            Questions = category.Questions.Select(q => new QuestionWrapper(q));
        }
    }
}
