﻿using BookManagement.Data.Models;
using BookManagement.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Data.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByTitleAsync(string title)
        {
            return await _context.Books.AnyAsync(b => b.Title == title);
        }

        // ✅ Implement IncrementBookViewsAsync
        public async Task IncrementBookViewsAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                book.ViewsCount++;  // Increment the BookViews count
                await _context.SaveChangesAsync();
            }
        }
    }
}
