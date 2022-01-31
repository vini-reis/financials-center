using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.Helpers
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public T Data { get; set; }

        public void Succeeded(T data)
        {
            Success = true;
            Data = data;
        }

        public void Failed(string message)
        {
            Message = message;
        }

        public void Failed(string message, string internalMessage)
        {
            Message = message;
            InternalMessage = internalMessage;
        }

    }
}
