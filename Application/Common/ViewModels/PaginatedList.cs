﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.ViewModels
{
    public static class PaginatedListExtensions
    {
        public static async Task<PaginatedList<T>> PaginateAsync<T>(this IQueryable<T> source, int page, int itemsPerPage, CancellationToken token)
        {
            var totalItems = await source.CountAsync(token);
            var items = await source.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync(token);

            return new PaginatedList<T>(items, page, itemsPerPage, totalItems);
        }
    }

    public class PaginatedList<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        public int TotalItems { get; }
        public int ItemsPerPage { get; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        private readonly List<T> _items = new List<T>();
        public IReadOnlyCollection<T> Items => _items.AsReadOnly();

        public PaginatedList(List<T> items, int pageIndex, int itemsPerPage, int totalItems)
        {
            PageIndex = pageIndex;
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;

            TotalPages = (int)Math.Ceiling(totalItems / (double)itemsPerPage);

            _items.AddRange(items);
        }
    }
}
