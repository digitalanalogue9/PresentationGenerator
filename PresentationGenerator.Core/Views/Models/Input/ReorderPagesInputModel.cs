using System.Collections.Generic;

namespace PresentationGenerator.Core.Views.Models.Input
{
    public class ReorderPagesInputModel
    {
        public string Id { get; set; }
        public IList<Position> Positions{ get; set; }

        public ReorderPagesInputModel()
        {
          Positions = new List<Position>();   
        }
    }

    public class Position    
    {
        public string Id { get; set; }
        public int Order { get; set; }
    }
}