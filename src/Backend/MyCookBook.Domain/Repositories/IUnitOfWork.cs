﻿namespace MyCookBook.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
