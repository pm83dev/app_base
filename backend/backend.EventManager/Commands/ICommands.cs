
using backend.EventManager.Entity;

namespace backend.EventManager.Commands
{
    public interface ICommands<TEntity> where TEntity : IEntity
    {
        public Task HandleCreateAsync(TEntity entity);
        public Task HandleUpdateAsync(TEntity entity);

        public Task HandleDeleteAsync(TEntity entity);
    }
}
