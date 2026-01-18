using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Common
{
    public class Result<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public string? Error { get; init; }
        public static Result<T> Ok(T data) => new() { Success = true, Data = data }; 
        public static Result<T> Fail(string error) => new() { Success = false, Error = error };
    }
}
