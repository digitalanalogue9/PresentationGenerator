using System;
using PresentationGenerator.Core.Entities;

namespace PresentationGenerator.Core.Repositories
{
    public interface IEntityRepository<TEntity, TDocument> : IDisposable where TEntity : IEntity<TDocument>
    {
        string RavenIdentifier { get; }
        TEntity Load(string id);
        void Add(TEntity entity);
        void Add(TEntity entity, string id);
        void Remove(TEntity entity);
        TEntity Create(TDocument document);
    }
}