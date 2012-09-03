using System;

namespace PresentationGenerator.Core.Views.Models.Input
{
    public class PresentationListInputModel
    {
        public string UserId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}