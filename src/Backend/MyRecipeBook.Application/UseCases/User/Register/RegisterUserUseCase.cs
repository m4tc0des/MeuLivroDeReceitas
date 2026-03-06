using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        public async Task<ResponseRegisteredUserJson>Execute(RequestRegisterUserJson request)
        {
            var criptographyPassword = new PasswordEncripter();

            Validade(request);

            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            var user = autoMapper.Map<Domain.Entities.User>(request);

            user.Password = criptographyPassword.Encrypt(request.Password!);

            await _writeOnlyRepository.Add(user);

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
            };
        }
        public void Validade(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);
            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
