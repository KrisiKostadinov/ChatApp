using System.Collections.Generic;

namespace ChatServer.Common.Extentions
{
    public class Result
    {
        public Result(params Error[] errors)
        {
            this.Errors = errors;

            this.Succeeded = false;
        }

        public static Result Success => new Result(true);

        protected Result(bool success)
        {
            this.Succeeded = success;
            this.Errors = null;
        }

        public IEnumerable<Error> Errors { get; }

        public bool Succeeded { get; protected set; } = true;

        public static Result Failed(params Error[] errors)
        {
            return new Result(errors);
        }

        public override string ToString()
        {
            return this.Errors.ToString();
        }
    }
}
