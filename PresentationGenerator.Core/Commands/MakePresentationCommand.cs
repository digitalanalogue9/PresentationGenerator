using System;
using System.Collections.Generic;
using PresentationGenerator.Core.Documents;

namespace PresentationGenerator.Core.Commands
{
    public class MakePresentationCommand
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public IList<Page> Pages { get; set; }

        public MakePresentationCommand(string userid, string presentationid, IList<Page> pages, string title, DateTime created)
        {
            UserId = userid;
            Id = presentationid;
            Pages = pages;
            Title = title;
            Created = created;
        }

    }
}