using System;
using System.Collections.Generic;

namespace PresentationGenerator.Core.Documents

{
    /// <summary>
    /// The basic matter object..
    /// </summary>
    public class PresentationDocument : IDocument
    {

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public DateTime LastModified { get; set; }
        public IList<Page> Pages { get; set; }

        public PresentationDocument()
        {
            Pages = new List<Page>();
        }

    }
}