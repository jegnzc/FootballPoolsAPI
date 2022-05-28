﻿using System.Linq.Expressions;

namespace RecruitmentSolutionsAPI.Interfaces;

public interface IGenericRepository<T> where T : class
{
    T GetById(int id);

    IEnumerable<T> GetAll();

    IEnumerable<T> Find(Expression<Func<T, bool>> expression);

    void Add(T entity);

    void Update(T entity);

    public IEnumerable<T> GetAllPopulated(Expression<Func<T, bool>> expression);

    void AddRange(IEnumerable<T> entities);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    ICollection<TType> Get<TType>(Expression<Func<T, bool>> where, Expression<Func<T, TType>> select)
        where TType : class;
}