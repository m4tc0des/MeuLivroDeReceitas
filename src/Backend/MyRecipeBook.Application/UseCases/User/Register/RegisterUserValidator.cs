using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator: AbstractValidator<RequestRegisterUserJson> // classe de validação para o registro de usuário
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.NAME_EMPTY); 
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY); 
            RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID); 
            RuleFor(x => x.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesExceptions.PASSWORD_LENGHT); 
        }
    }
}
