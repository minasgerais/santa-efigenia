using RestSharp;

namespace Sauron.Crawlers.Extensions
{
    public static class RestSharpExtensions
    {
        public static bool IsSuccessful(this IRestResponse response)
        {
            return response.HasSuccessfulStatusCode() && response.ResponseStatus == ResponseStatus.Completed;
        }

        public static bool HasSuccessfulStatusCode(this IRestResponse response)
        {
            int numericResponse = (int)response.StatusCode;
            return numericResponse >= 200 && numericResponse <= 399;
        }

        public static string GetContent(this IRestResponse response)
        {
            return (response.IsSuccessful()) ? response.Content : $"{response.ErrorMessage}:{response.ErrorException.ToString()}";
        }
    }
}
