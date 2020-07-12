using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardSlide.API.Application.Common.Models
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<string> Errors { get; set; }

        private AuthenticationResult(bool success, string token,
            string refreshToken, IEnumerable<string> errors)
        {
            Success = success;
            Token = token;
            RefreshToken = refreshToken;
            Errors = errors;
        }

        public static AuthenticationResult Successful(string token, string refreshToken)
        {
            return new AuthenticationResult(true, token, refreshToken, Enumerable.Empty<string>());
        }

        public static AuthenticationResult Failure(string error)
        {
            return new AuthenticationResult(false, null, null, new string[] { error });
        }

        public static AuthenticationResult Failure(IEnumerable<string> errors)
        {
            return new AuthenticationResult(false, null, null, errors);
        }

        public string GetErrorMessage()
        {
            var builder = new StringBuilder();
            Errors.ToList().ForEach(error => builder.Append(error + " "));
            return builder.ToString().TrimEnd();
        }
    }
}