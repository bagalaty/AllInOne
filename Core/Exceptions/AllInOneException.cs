// ------------------------------------------------------------------------------
//  Copyright (c) 2015 Microsoft Corporation
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

 
     
    
namespace Core.Exceptions
    {
        [Serializable]
        public class AllInOneException : Exception
        {
            public Error Error { get; private set; }

            public AllInOneException(string message) : base(message) { }

            public AllInOneException(Error error, Exception innerException = null) : base(error.Message, innerException)
            {
                Error = error;
            }

            public bool IsMatch(string errorCode)
            {
                if (string.IsNullOrEmpty(errorCode))
                    throw new ArgumentException("errorCode cannot be null or empty", "errorCode");

                var currentError = Error;

                if (currentError != null)
                    if (string.Equals(currentError.Code, errorCode, StringComparison.OrdinalIgnoreCase))
                        return true;

                return false;
            }

            public override string ToString()
            {
                return Error?.ToString();
            }
        }
    }
 
