namespace MyCookBook.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : MyCookBookException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages)
        {
            this.ErrorMessages = errorMessages;
        }
    }
}
