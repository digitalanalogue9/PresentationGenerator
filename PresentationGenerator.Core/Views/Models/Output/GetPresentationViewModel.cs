using System;
using System.Collections.Generic;
using PresentationGenerator.Core.Documents;

namespace PresentationGenerator.Core.Views.Models.Output
{
    public class GetPresentationViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IList<Page> Pages { get; set; }

        public GetPresentationViewModel()
        {
            Id = string.Empty;
            Title = string.Empty;
            Pages = new List<Page>();
        }

    }
}