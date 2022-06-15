using System;
namespace TariffComparison.LogicLayer.Model
{
    public class ResponseObject<T>
    {
        public T data { get; set; }
        public int errorcode { get; set; }
        public string errormessage { get; set; }

        public ResponseObject()
        {
        }

        public static ResponseObject<T> Success(T data)
        {
            return new ResponseObject<T> { data = data, errorcode = 0, errormessage = "" };
        }

        public static ResponseObject<T> Error(int errorcode, string message)
        {
            return new ResponseObject<T> { errorcode = errorcode, errormessage = message };
        }

    }
}
