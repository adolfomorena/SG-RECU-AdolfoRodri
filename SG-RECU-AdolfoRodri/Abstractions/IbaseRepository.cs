﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SG_RECU_AdolfoRodri.Abstractions
{
    public interface IbaseRepository<T> : IDisposable where T : TableData, new()
    {
        void SaveItem(T item);
        void SaveItemCascada(T item, bool isCascada = true);
        T GetItem(int id);
        T GetItem(Expression<Func<T, bool>> predicate);
        List<T> GetItems();
        List<T> GetItemsCascada();
        List<T> GetItems(Expression<Func<T, bool>> predicate);
        void DeleteItem(T item);
    }
}
