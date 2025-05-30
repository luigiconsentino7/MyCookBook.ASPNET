﻿using AutoMapper;
using MyCookBook.Communication.Requests;
using MyCookBook.Domain.Entities;

namespace MyCookBook.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(d => d.Password, o => o.Ignore());
        }
    }
}
