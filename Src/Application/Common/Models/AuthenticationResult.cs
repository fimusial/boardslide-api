using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardSlide.API.Application.Common.Models
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Errors { get; set; }

        private AuthenticationResult(bool success, string token, IEnumerable<string> errors)
        {
            Success = success;
            Token = token;
            Errors = errors;
        }

        public static AuthenticationResult Successful(string token)
        {
            return new AuthenticationResult(true, token, Enumerable.Empty<string>());
        }

        public static AuthenticationResult Failure(IEnumerable<string> errors)
        {
            return new AuthenticationResult(false, null, errors);
        }

        public string GetErrorMessage()
        {
            var builder = new StringBuilder();
            Errors.ToList().ForEach(error => builder.Append(error + " "));
            return builder.ToString().TrimEnd();
        }
    }
}