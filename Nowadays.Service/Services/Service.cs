using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nowadays.Core.Dtos;
using Nowadays.Core.Models;
using Nowadays.Core.Repositories;
using Nowadays.Core.Services;
using Nowadays.Core.UnitOfWorks;
using Nowadays.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Service.Services;

public class Service<Entity, Dto> : IService<Entity, Dto> where Entity : BaseEntity where Dto : class
{
    private readonly IGenericRepository<Entity> _repository;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public Service(IGenericRepository<Entity> repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<NowadaysResponseDto<Dto>> AddAsync(Dto dto)
    {
        var newEntity = _mapper.Map<Entity>(dto);
        await _repository.AddAsync(newEntity);
        await _unitOfWork.CommitAsync();

        var newDto = _mapper.Map<Dto>(newEntity);
        return NowadaysResponseDto<Dto>.Success(StatusCodes.Status200OK, newDto);
    }

    public async Task<NowadaysResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtoList)
    {
        var newEntities = _mapper.Map<IEnumerable<Entity>>(dtoList);
        await _repository.AddRangeAsync(newEntities);
        await _unitOfWork.CommitAsync();

        var newDtoList = _mapper.Map<IEnumerable<Dto>>(newEntities); ;
        return NowadaysResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, newDtoList);
    }

    public async Task<NowadaysResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression)
    {
        var anyEntity = await _repository.AnyAsync(expression);
        return NowadaysResponseDto<bool>.Success(StatusCodes.Status200OK, anyEntity);
    }

    public async Task<NowadaysResponseDto<IEnumerable<Dto>>> GetAllAsync()
    {
        var entities = await _repository.GetAll().ToListAsync();
        var dtoList = _mapper.Map<IEnumerable<Dto>>(entities);
        return NowadaysResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtoList);
    }

    public async Task<NowadaysResponseDto<Dto>> GetByIdAsync(object id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException($"{typeof(Entity).Name}({id}) not found");

        var dto = _mapper.Map<Dto>(entity);
        return NowadaysResponseDto<Dto>.Success(StatusCodes.Status200OK, dto);
    }

    public async Task<NowadaysResponseDto<NoContentDto>> RemoveAsync(object id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException($"{typeof(Entity).Name}({id}) not found");

        _repository.Remove(entity);

        await _unitOfWork.CommitAsync();
        return NowadaysResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
    }

    public async Task<NowadaysResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<object> ids)
    {
        var entities = await _repository.Where(x => ids.Contains(x.Id)).ToListAsync();
        _repository.RemoveRange(entities);

        await _unitOfWork.CommitAsync();
        return NowadaysResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
    }

    public async Task<NowadaysResponseDto<NoContentDto>> UpdateAsync(Dto dto)
    {
        var entity = _mapper.Map<Entity>(dto);
        _repository.Update(entity);

        await _unitOfWork.CommitAsync();
        return NowadaysResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
    }

    public async Task<NowadaysResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression)
    {
        var entities = await _repository.Where(expression).ToListAsync();
        var dtoList = _mapper.Map<IEnumerable<Dto>>(entities);
        return NowadaysResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtoList);
    }

    public NowadaysResponseDto<int> Count(Expression<Func<Entity, bool>> expression)
    {
        var count = _repository.Count(expression);
        return NowadaysResponseDto<int>.Success(StatusCodes.Status200OK, count);
    }

    public async Task<NowadaysResponseDto<IEnumerable<Dto>>> GetAllWithIncludeAllAsync()
    {
        var entities = await _repository.GetAllWithIncludeAll().ToListAsync();
        var dtoList = _mapper.Map<IEnumerable<Dto>>(entities);

        return NowadaysResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtoList);
    }

    public async Task<NowadaysResponseDto<IEnumerable<Dto>>> GetAllWithIncludeAsync(List<Expression<Func<Entity, object>>> includeProperties)
    {
        var entities = await _repository.GetAllWithInclude(includeProperties).ToListAsync();
        var dtoList = _mapper.Map<IEnumerable<Dto>>(entities);

        return NowadaysResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtoList);
    }
}
