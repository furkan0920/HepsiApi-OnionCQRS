using Hepsi.Application.Interface.RedisCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsi.Application.Beheviors
{
    public class RedisCacheBehevior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IRedisCacheService redisCacheService;

        public RedisCacheBehevior(IRedisCacheService redisCacheService)
        {
            this.redisCacheService = redisCacheService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(redisCacheService is  ICacheableQuery query)
            {
                var cacheKey=query.CacheKey;
                var cacheTime=query.CacheTime;
                var cacheData = await redisCacheService.GetAsync<TResponse>(cacheKey);
                if (cacheData is not null)
                    return cacheData;
                var response = await next();
                if(response is not null)
                    await redisCacheService.SetAsync(cacheKey, response,DateTime.Now.AddMinutes(cacheTime));

                return response;

            }
            return await next();    
        }
    }
}
