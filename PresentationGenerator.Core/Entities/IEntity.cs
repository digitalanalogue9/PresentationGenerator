using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PresentationGenerator.Core.Entities
{
    public interface IEntity<TDoc>
    {
        string Id { get; }
        TDoc GetInnerDocument();
    }
}
