using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.DTOs
{
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public DateTime Timestamp { get; set; }

        public ResponseDTO()
        {
            Timestamp = DateTime.UtcNow;
            Errors = new List<string>();
        }

        public static ResponseDTO<T> CreateSuccess(T data, string message = "Operation successful")
        {
            return new ResponseDTO<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ResponseDTO<T> CreateError(string message, List<string> errors = null)
        {
            return new ResponseDTO<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
