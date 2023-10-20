using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nowadays.Core.Dtos;

public class NowadaysResponseDto<T>
{
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    #region Success
    public static NowadaysResponseDto<T> Success(int statusCode, T data)
    {
        return new NowadaysResponseDto<T> { Data = data, StatusCode = statusCode };
    }

    public static NowadaysResponseDto<T> Success(int statusCode)
    {
        return new NowadaysResponseDto<T> { StatusCode = statusCode };
    }
    #endregion

    #region Fail
    public static NowadaysResponseDto<T> Fail(int statusCode, List<string> errors)
    {
        return new NowadaysResponseDto<T> { StatusCode = statusCode, Errors = errors };
    }

    public static NowadaysResponseDto<T> Fail(int statusCode, string error)
    {
        return new NowadaysResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
    }
    #endregion
}
