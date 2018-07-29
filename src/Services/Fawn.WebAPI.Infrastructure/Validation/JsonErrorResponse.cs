namespace Fawn.WebAPI.Infrastructure.Validation
{
    public class JsonErrorResponse
    {
        public string[] Messages { get; set; }

        public object DeveloperMessage { get; set; }
    }
}