using System;
using System.Collections.Generic;
using PresentationGenerator.Core.Views.Models.Input;

namespace PresentationGenerator.Core.Commands
{
    public class ReOrderPagesCommand
    {
        public string Id { get; set; }
        public IList<Position> Positions{ get; set; }
        public DateTime LastModified { get; set; }

        public ReOrderPagesCommand(string id, IList<Position> positions, DateTime lastmodified)
        {
            Id = id;
            Positions = positions;
            LastModified = lastmodified;
        }
    }
}