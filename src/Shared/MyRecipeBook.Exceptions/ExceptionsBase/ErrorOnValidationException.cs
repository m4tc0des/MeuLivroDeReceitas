namespace MyRecipeBook.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException: MyRecipeBookException
    {
        public IList<string> ErrorMessage { get; set; }

        public ErrorOnValidationException(IList<string> errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
