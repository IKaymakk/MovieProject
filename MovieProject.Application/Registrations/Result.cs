using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.Registrations
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        // Başarılı işlem sonucu
        public static Result Success() => new Result { IsSuccess = true };

        // Başarısız işlem sonucu
        public static Result Failure(string message) => new Result { IsSuccess = false, Message = message };
    }

}
