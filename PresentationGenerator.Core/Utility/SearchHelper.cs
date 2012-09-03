using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using PresentationGenerator.Core.Utility.Voodoo;

namespace PresentationGenerator.Core.Utility
{
    public interface ISearchHelper
    {
        IList<TResult> PagedSearch<TSource, TResult>(Expression<Func<TSource, bool>> predicate, out int total, out int totalpages, int pagesize);
        IList<TResult> PagedSearch<TSource, TResult>(Expression<Func<TSource, bool>> predicate, string indexname, out int total, out int totalpages, int pagesize);
        IList<TSource> PagedLuceneSearchStartsWith<TSource>(string field, object value, string indexname, int pageindex, out int total, out int totalpages, int pagesize);
        IList<TSource> PagedLuceneSearchEquals<TSource>(string field, object value, string indexname, int pageindex, out int total, out int totalpages, int pagesize);

        IList<TResult> PagedSearch<TSource, TResult, TIndexCreator>(Expression<Func<TResult, bool>> predicate,
                                                                                    out int total, out int totalpages) where TIndexCreator : AbstractIndexCreationTask<TSource, TResult>, new();

        IList<TResult> SubDefaultPageSizeSearch<TSource, TResult>(Expression<Func<TSource, bool>> predicate, string indexname,
                                                                                  int maxrecords, out int total, out int totalpages);

        IList<TResult> SubDefaultPageSizeSearch<TSource, TResult, TIndexCreator>(Expression<Func<TResult, bool>> predicate,
                                                                                                int maxrecords, out int total, out int totalpages) where TIndexCreator : AbstractIndexCreationTask<TSource, TResult>, new();

        IList<TResult> SubDefaultPageSizeSearch<TSource, TResult>(Expression<Func<TSource, bool>> predicate, int maxrecords,
                                                                                  out int total, out int totalpages);
    }

    public abstract class SearchHelper : ISearchHelper
    {
        private readonly IDocumentStore _documentstore;

        public SearchHelper(IDocumentStore documentstore)
        {
            _documentstore = documentstore;
        }


        public IList<TResult> PagedSearch<TSource, TResult>(Expression<Func<TSource, bool>> predicate, out int total, out int totalpages, int pagesize = 1024)
        {
            var output = new List<TResult>();
            RavenQueryStatistics stats;
            using (IDocumentSession session = _documentstore.OpenSession())
            {
                session.Query<TSource>()
                    .Where(predicate)
                    .Statistics(out stats)
                    .Take(0)
                    .ToArray();
            }

            total = stats.TotalResults;
            var skippedResults = stats.SkippedResults;
            totalpages = (total + pagesize - 1) / pagesize;
            for (var x = 0; x < totalpages; x++)
            {
                // get a page at a time
                using (var session = _documentstore.OpenSession())
                {
                    var results = Enumerable.ToArray<TResult>(session.Query<TSource>()
                                              .Statistics(out stats)
                                              .Skip((x * 1024) + skippedResults)
                        // retrieve results for the second page, taking into account skipped results
                                              .Take(1024) // page size is 1024 e.g. full default page size
                                              .Where(predicate)
                                              .Project().To<TResult>()
                                              .Distinct());
                    skippedResults = stats.SkippedResults;
                    output.AddRange(results);
                }
            }
            return output;
        }


        public IList<TResult> PagedSearch<TSource, TResult>(Expression<Func<TSource, bool>> predicate, string indexname, out int total, out int totalpages, int pagesize = 1024)
        {

            var output = new List<TResult>();
            RavenQueryStatistics stats;
            using (var session = _documentstore.OpenSession())
            {
                session.Query<TSource>(indexname).Where(predicate).Statistics(out stats).Take(0).ToArray();
            }

            total = stats.TotalResults;
            var skippedResults = stats.SkippedResults;
            totalpages = (total + pagesize - 1) / pagesize;
            for (var x = 0; x < totalpages; x++)
            {
                // get a page at a time
                using (var session = _documentstore.OpenSession())
                {
                    var results = session.Query<TSource>(indexname)
                                               .Statistics(out stats)
                                               .Skip((x * pagesize) + skippedResults)
                                               .Take(pagesize) // page size is 1024 e.g. full default page size
                                               .Where(predicate)
                                               .Project().To<TResult>()
                                               .Distinct()
                                               .ToArray();
                    skippedResults = stats.SkippedResults;
                    output.AddRange(results);
                }
            }
            return output;
        }

        public IList<TSource> PagedLuceneSearchStartsWith<TSource>(string field, object value, string indexname, int pageindex, out int total, out int totalpages, int pagesize = 128)
        {
            var output = new List<TSource>();
            RavenQueryStatistics stats;
            using (var session = _documentstore.OpenSession())
            {
                session
                    .Advanced
                    .LuceneQuery<TSource>(indexname)
                    .WhereStartsWith(field, value)
                    .Statistics(out stats)
                    .Take(0)
                    .ToArray();
            }

            total = stats.TotalResults;
            totalpages = (total + pagesize - 1) / pagesize;
            if (total > 0)
            {
                var skippedResults = stats.SkippedResults;

                if (pageindex > -1)
                {
                    using (var session = _documentstore.OpenSession())
                    {
                        var results = session.Advanced.
                            LuceneQuery<TSource>(indexname)
                            .Statistics(out stats)
                            .Skip((pageindex * pagesize) + skippedResults)
                            // retrieve results for the second page, taking into account skipped results
                            .Take(pagesize) // page size is 128 e.g. full default page size
                            .WhereStartsWith(field, value)
                            //.Distinct()
                            .ToArray();
                        output.AddRange(results);
                    }
                }
                else
                {

                    for (var x = 0; x < totalpages; x++)
                    {

                        // get a page at a time
                        using (var session = _documentstore.OpenSession())
                        {
                            var results = session.Advanced.
                                LuceneQuery<TSource>(indexname)
                                .Statistics(out stats)
                                .Skip((x * pagesize) + skippedResults)
                                // retrieve results for the second page, taking into account skipped results
                                .Take(pagesize) // page size is 128 e.g. full default page size
                                .WhereStartsWith(field, value)
                                //.Distinct()
                                .ToArray();
                            skippedResults = stats.SkippedResults;
                            output.AddRange(results);
                        }
                    }
                }




            }
            return output;
        }

        public IList<TSource> PagedLuceneSearchEquals<TSource>(
            string field, object value, string indexname,
            int pageindex, out int total, out int totalpages, int pagesize = 128)
        {


            var output = new List<TSource>();
            RavenQueryStatistics stats;
            using (var session = _documentstore.OpenSession())
            {
                session
                    .Advanced
                    .LuceneQuery<TSource>(indexname)
                    .WhereStartsWith(field, value)
                    .Statistics(out stats)
                    .Take(0)
                    .ToArray();
            }

            total = stats.TotalResults;
            totalpages = (total + pagesize - 1) / pagesize;
            if (total > 0)
            {
                var skippedResults = stats.SkippedResults;

                if (pageindex > -1)
                {
                    using (var session = _documentstore.OpenSession())
                    {
                        var results = session.Advanced.
                            LuceneQuery<TSource>(indexname)
                            .Statistics(out stats)
                            .Skip((pageindex * pagesize) + skippedResults)
                            // retrieve results for the second page, taking into account skipped results
                            .Take(pagesize) // page size is 128 e.g. full default page size
                            .WhereEquals(field, value)
                            //.Distinct()
                            .ToArray();
                        output.AddRange(results);
                    }
                }
                else
                {
                    for (var x = 0; x < totalpages; x++)
                    {

                        // get a page at a time
                        using (var session = _documentstore.OpenSession())
                        {
                            var results = session.Advanced.
                                LuceneQuery<TSource>(indexname)
                                .Statistics(out stats)
                                .Skip((x * pagesize) + skippedResults)
                                // retrieve results for the second page, taking into account skipped results
                                .Take(pagesize) // page size is 128 e.g. full default page size
                                .WhereEquals(field, value)
                                //.Distinct()
                                .ToArray();
                            skippedResults = stats.SkippedResults;
                            output.AddRange(results);
                        }
                    }
                }




            }
            return output;
        }




        public IList<TResult> PagedSearch<TSource, TResult, TIndexCreator>(Expression<Func<TResult, bool>> predicate,
                                                         out int total, out int totalpages) where TIndexCreator : AbstractIndexCreationTask<TSource, TResult>, new()
        {
            var output = new List<TResult>();
            RavenQueryStatistics stats;
            using (IDocumentSession session = _documentstore.OpenSession())
            {
                session.Query<TResult, TIndexCreator>().Where(predicate).Statistics(out stats).Take(0).ToArray();
            }

            total = stats.TotalResults;
            var skippedResults = stats.SkippedResults;
            totalpages = (total + 1024 - 1) / 1024;
            for (var x = 0; x < totalpages; x++)
            {
                // get a page at a time
                using (IDocumentSession session = _documentstore.OpenSession())
                {
                    TResult[] results = session.Query<TResult, TIndexCreator>()
                        .Statistics(out stats)
                        .Skip((x * 1024) + skippedResults)
                        // retrieve results for the second page, taking into account skipped results
                        .Take(1024) // page size is 1024 e.g. full default page size
                        .Where(predicate)
                        .AsProjection<TResult>()
                        .Distinct()
                        .ToArray();
                    skippedResults = stats.SkippedResults;
                    output.AddRange(results);
                }
            }
            return output;
        }

        public IList<TResult> SubDefaultPageSizeSearch<TSource, TResult>(Expression<Func<TSource, bool>> predicate, string indexname,
                                                       int maxrecords, out int total, out int totalpages)
        {
            if (maxrecords > 1024)
            {
                IList<TResult> records = PagedSearch<TSource, TResult>(predicate, indexname, out total, out totalpages);
                return records.Take(maxrecords).ToList();
            }

            var output = new List<TResult>();
            RavenQueryStatistics stats;
            using (IDocumentSession session = _documentstore.OpenSession())
            {
                session.Query<TSource>(indexname).Where(predicate).Statistics(out stats).Take(0).ToArray();
            }

            total = stats.TotalResults;
            totalpages = total == 0 ? 0 : 1;
            using (var session = _documentstore.OpenSession())
            {
                var results = session.Query<TSource>(indexname)
                                          .Take(maxrecords) // page size is defined by user but should be < 1024
                                          .Where(predicate)
                                          .Project().To<TResult>()
                                          .Distinct()
                                          .ToArray();

                output.AddRange(results);
            }
            return output;
        }

        public IList<TResult> SubDefaultPageSizeSearch<TSource, TResult, TIndexCreator>(Expression<Func<TResult, bool>> predicate,
                                                                      int maxrecords, out int total, out int totalpages) where TIndexCreator : AbstractIndexCreationTask<TSource, TResult>, new()
        {
            if (maxrecords > 1024)
            {
                var records = PagedSearch<TSource, TResult, TIndexCreator>(predicate, out total, out totalpages);
                return records.Take(maxrecords).ToList();
            }

            var output = new List<TResult>();
            RavenQueryStatistics stats;
            using (var session = _documentstore.OpenSession())
            {
                session.Query<TResult, TIndexCreator>().Where(predicate).Statistics(out stats).Take(0).ToArray();
            }

            total = stats.TotalResults;
            totalpages = total == 0 ? 0 : 1;
            using (var session = _documentstore.OpenSession())
            {
                var results = session.Query<TResult, TIndexCreator>()
                    .Take(maxrecords) // page size is defined by user but should be < 1024
                    .Where(predicate)
                    .AsProjection<TResult>()
                    .Distinct()
                    .ToArray();
                output.AddRange(results);
            }
            return output;
        }

        public IList<TResult> SubDefaultPageSizeSearch<TSource, TResult>(Expression<Func<TSource, bool>> predicate, int maxrecords,
                                                       out int total, out int totalpages)
        {
            if (maxrecords > 1024)
            {
                var records = PagedSearch<TSource, TResult>(predicate, out total, out totalpages);
                return records.Take(maxrecords).ToList();
            }

            var output = new List<TResult>();
            RavenQueryStatistics stats;
            using (var session = _documentstore.OpenSession())
            {
                session.Query<TSource>().Where(predicate).Statistics(out stats).Take(0).ToArray();
            }

            total = stats.TotalResults;
            totalpages = total == 0 ? 0 : 1;
            using (var session = _documentstore.OpenSession())
            {
                var results = session.Query<TSource>()
                                          .Take(maxrecords) // page size is defined by user but should be < 1024
                                          .Where(predicate)
                                          .Project().To<TResult>()
                                          .Distinct()
                                          .ToArray();
                output.AddRange(results);
            }
            return output;
        }

    }



}