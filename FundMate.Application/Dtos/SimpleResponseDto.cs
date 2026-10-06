using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace FundMate.Application.Dtos;

public class SimpleResponseDto
{
    public bool Success { get; set; }

    public string? ErrorMessage { get; set; }

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }

    public SimpleResponseDto(string errorMsg, HttpStatusCode statusCode)
    {
        Success = false;
        ErrorMessage = errorMsg;
        StatusCode = statusCode;
    }

    public SimpleResponseDto(bool successStatus)
    {
        Success = successStatus;
    }
}
