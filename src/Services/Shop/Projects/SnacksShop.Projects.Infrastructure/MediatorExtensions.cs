using MediatR;
using SnacksShop.Projects.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace SnacksShop.Projects.Infrastructure
{
    public static class MediatorExtensions
    {
        /// <summary>
        /// Mediator扩展方法
        /// 用于遍历DBcontext中所有变更的实体领域事件，并发送通知
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task DispatchDomainEventAsync(this IMediator mediator, ProjectContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                                    .Entries<Entity>()
                                    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents)
                                             .ToList();

            domainEntities.ToList()
                          .ForEach(entity => entity.Entity.DomainEvents.Clear());

            var tasks = domainEvents.Select(async (domainEvent) => await mediator.Publish(domainEvent));

            await Task.WhenAll(tasks);
        }
    }
}
