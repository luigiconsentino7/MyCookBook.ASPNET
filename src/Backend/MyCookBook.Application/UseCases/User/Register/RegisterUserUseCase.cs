using AutoMapper;
using MyCookBook.Application.Services.AutoMapper;
using MyCookBook.Application.Services.Cryptography;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;
using MyCookBook.Domain.Repositories;
using MyCookBook.Domain.Repositories.User;
using MyCookBook.Exceptions.ExceptionsBase;

namespace MyCookBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PasswordEncrypter _passwordEncrypter;

        public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, IUserReadOnlyRepository readOnlyRepository, IMapper mapper, PasswordEncrypter passwordEncrypter, IUnitOfWork unitOfWork)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _passwordEncrypter = passwordEncrypter;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            var passwordEncrypt = new PasswordEncrypter();

            Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);    
            
            user.Password = _passwordEncrypter.Encrypt(request.Password);

            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisterUserJson
            { 
                Name = request.Name
            };
        }

        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
