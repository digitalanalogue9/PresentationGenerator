
using System;
using Raven.Client;
using PresentationGenerator.Core.Entities;
using PresentationGenerator.Core.Utility;
using StructureMap;

namespace PresentationGenerator.Core.Repositories
{
    public abstract class EntityRepository<TEntity, TDoc> : IEntityRepository<TEntity, TDoc>
        where TEntity : IEntity<TDoc>
    {
        internal readonly IDocumentStore _documentStore;
        internal static ILogger _logger;
        protected EntityRepository(ILogger logger, IDocumentStore documentStore)
        {
            _logger = logger;
            _documentStore = documentStore;
        }

        public string RavenIdentifier
        {
            get { return _documentStore == null ? string.Empty : _documentStore.Identifier; }
        }

        public TEntity Load(string id)
        {
            using (var session = _documentStore.OpenSession())
            {
                var document = session.Load<TDoc>(id);
                if (document == null)
                {
                    return default(TEntity);
                }
                return Create(document);
            }
        }

        public void Add(TEntity entity)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(entity.GetInnerDocument());
                session.SaveChanges();
            }
        }
        public void Add(TEntity entity, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ApplicationException("id parameter was empty");
            }

            using (var session = _documentStore.OpenSession())
            {
                session.Store(entity.GetInnerDocument(), id);
                session.SaveChanges();
            }
        }

        public void Remove(TEntity entity)
        {
            using (var session = _documentStore.OpenSession())
            {
                var document = session.Load<TDoc>(entity.Id);
                session.Delete(document);
                session.SaveChanges();
            }
        }

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }




        public abstract TEntity Create(TDoc doc);

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~EntityRepository()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {

        }
    }
}