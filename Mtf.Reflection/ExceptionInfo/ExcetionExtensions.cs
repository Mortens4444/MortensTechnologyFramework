using System;
using System.Runtime.InteropServices;

namespace Mtf.Reflection.ExceptionInfo
{
    public static class ExcetionExtensions
    {
        public static int GetErrorCode(this Exception ex)
        {
            return Marshal.GetHRForException(ex);
        }

        public static string GetDetails(this Exception ex)
        {
            return new ExceptionDetails(ex).Details;
        }
    }
}
