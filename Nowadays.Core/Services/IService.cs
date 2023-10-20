using Nowadays.Core.Dtos;
using Nowadays.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Core.Services;

public interface IService<Entity, Dto> where Entity : BaseEntity where Dto : class
{
    Task<NowadaysResponseDto<Dto>> GetByIdAsync(object id);

    Task<NowadaysResponseDto<IEnumerable<Dto>>> GetAllAsync();
    Task<NowadaysResponseDto<IEnumerable<Dto>>> GetAllWithIncludeAllAsync();
    Task<NowadaysResponseDto<IEnumerable<Dto>>> GetAllWithIncludeAsync(List<Expression<Func<Entity, object>>> includeProperties);

    Task<NowadaysResponseDto<Dto>> AddAsync(Dto dtoList);
    Task<NowadaysResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> entities);

    Task<NowadaysResponseDto<NoContentDto>> UpdateAsync(Dto dto);

    Task<NowadaysResponseDto<NoContentDto>> RemoveAsync(object id);
    Task<NowadaysResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<object> ids);

    Task<NowadaysResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);
    Task<NowadaysResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);

    NowadaysResponseDto<int> Count(Expression<Func<Entity, bool>> expression);
}