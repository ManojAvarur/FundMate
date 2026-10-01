using System;
using System.Collections.Generic;
using System.Text;

namespace FundMate.Application.CustomExceptions;

public class InputValidationException : Exception
{
    public InputValidationException() : base("Invalid User Input!") { } 

    public InputValidationException(string message) : base(message) { }

    public InputValidationException(string message, Exception innerException)
      : base(message, innerException) { }
}
