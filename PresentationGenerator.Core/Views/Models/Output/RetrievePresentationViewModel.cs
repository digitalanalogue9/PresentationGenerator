using System.Collections.Generic;
using System.Linq;
using System.Text;
using PresentationGenerator.Core.Documents;

namespace PresentationGenerator.Core.Views.Models.Output
{
    public class RetrievePresentationViewModel
    {
        public string PresentationId { get; set; }
        public string Title { get; set; }
        public IList<Page> Pages { get; set; }
    }
}
