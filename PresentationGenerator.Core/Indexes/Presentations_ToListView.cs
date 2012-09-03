using System.Linq;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Views.Models.Output;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace PresentationGenerator.Core.Indexes
{
    //public class Presentations_ToListView : AbstractIndexCreationTask<PresentationDocument, PresentationListItem>
    //{

    //    public Presentations_ToListView()
    //    {
    //        Map = docs => from doc in docs
    //                      select new
    //                                 {
    //                                     Id = doc.Id,
    //                                     Title = doc.Title,
    //                                     LastModifier = doc.UserId,
    //                                     LastModified = doc.LastModified,
    //                                     TotalPages = doc.Pages.Count
    //                                 };

    //        Sort(x => x.LastModified, SortOptions.String);
    //    }
    //}
}