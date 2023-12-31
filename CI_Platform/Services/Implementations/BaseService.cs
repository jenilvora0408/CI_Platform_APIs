﻿using System.Linq.Expressions;
using DataAccessLayer.Repositories.Interfaces;
using Services.Interfaces;
using System.Linq.Expressions;

namespace Services.Implementations
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepo<T> _baseRepo;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IBaseRepo<T> baseRepo, IUnitOfWork unitOfWork)
        {
            _baseRepo = baseRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(T entity)
        {
            CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            await _baseRepo.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            await _baseRepo.AddRangeAsync(entities, cancellationToken);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _baseRepo.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _baseRepo.UpdateRange(entities);
            await _unitOfWork.SaveAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _baseRepo.GetAsync(predicate);
        }
    }
}
