using System;

namespace PresentationGenerator.Core.Views.Models.Output
{
    public class PresentationListItem   
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string LastModifier { get; set; }
        public DateTime LastModified { get; set; }
        public int TotalPages { get; set; }
    }
}