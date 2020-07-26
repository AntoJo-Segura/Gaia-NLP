// <copyright file="OperationNotFoundException.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using AutoWrapper.Wrappers;

namespace Gaia.Application.Exceptions
{
    public class OperationNotFoundException : ApiException
    {
        private const int _statusCode = (int)HttpStatusCode.NotFound;

        public OperationNotFoundException(object customError)
            : base(customError, _statusCode)
        {
        }

        public OperationNotFoundException(IEnumerable<ValidationError> errors)
            : base(errors, _statusCode)
        {
        }

        public OperationNotFoundException(Exception ex)
            : base(ex, _statusCode)
        {
        }

        public OperationNotFoundException(string message, string errorCode = null, string refLink = null)
            : base(message, _statusCode, errorCode, refLink)
        {
        }
    }
}
