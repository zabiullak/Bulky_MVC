﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
            //_db.Categories == _dbSet 
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filter, string? includeProperty = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (var includeProp in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperty = null)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (var includeProp in includeProperty.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }
    }
}
