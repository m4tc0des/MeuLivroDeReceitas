using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using Xunit;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterUserValidator();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);


        }
    }
}
