using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YanBinPower
{

    public sealed class ExceptionHelper : ApplicationException
    {
        private readonly static ExceptionHelper exceptionHelper = new ExceptionHelper();
        private readonly string error;
        private Exception innerException;
        public ExceptionHelper() { }
        public ExceptionHelper(string msg) : base(msg) { this.error = msg; }
        public ExceptionHelper(string msg, Exception innerException) : base(msg) { this.innerException = innerException; this.error = msg; }
        public string GetError() { return error; }
        public static ExceptionHelper GetInstance() { return exceptionHelper; }
    }
}
