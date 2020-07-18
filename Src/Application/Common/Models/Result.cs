using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardSlide.API.Application.Common.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }

        private Result(bool succeeded, IEnumerable<string> errors)
        {
            Success = succeeded;
            Errors = errors;
        }

        public static Result Successful() => new Result(true, Enumerable.Empty<string>());
        public static Result Failure(string error) => new Result(false, new string[] { error });
        public static Result Failure(IEnumerable<string> errors) => new Result(false, errors);

        public string GetErrorMessage()
        {
            var builder = new StringBuilder();
            Errors.ToList().ForEach(error => builder.Append(error + " "));
            return builder.ToString().TrimEnd();
        }
    }
}