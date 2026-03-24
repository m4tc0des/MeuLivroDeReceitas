using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build()
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(x => x.Name, (f) => f.Person.FirstName)
                .RuleFor(x => x.Email, (f, x) => f.Internet.Email(x.Name))
                .RuleFor(x => x.Password, (f) => f.Internet.Password());
        }
    }
}
