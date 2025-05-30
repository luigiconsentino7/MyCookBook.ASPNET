﻿using Microsoft.EntityFrameworkCore;
using MyCookBook.Domain.Entities;
using MyCookBook.Domain.Repositories.User;

namespace MyCookBook.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly MyCookBookDbContext _dbContext;

        public UserRepository(MyCookBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(User user) => await _dbContext.AddAsync(user);

        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);
    }
}
