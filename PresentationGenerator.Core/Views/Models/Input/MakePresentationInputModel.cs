using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PresentationGenerator.Core.Documents;

namespace PresentationGenerator.Core.Views.Models.Input
{
    public class MakePresentationInputModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public IList<Page> Pages { get; set; }

        public MakePresentationInputModel()
        {
            Pages = new List<Page>();
        }

    }
}