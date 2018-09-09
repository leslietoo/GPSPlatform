using GPS.ServiceGrpcBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.GrpcServiceBase.Extensions
{
    public static class ResultReplyExtensions
    {
        public static bool IsSuccess(this ResultReply resultReply)
        {
            return ResultReply.Types.StatusCode.Success == resultReply.Code;
        }

        public static ResultReply Success(string msg="")
        {
            return new ResultReply() { Code = ResultReply.Types.StatusCode.Success, Msg= msg };
        }

        public static ResultReply Failure(string msg = "")
        {
            return new ResultReply() { Code = ResultReply.Types.StatusCode.Failure, Msg = msg };
        }

        public static ResultReply Unauthorized(string msg = "")
        {
            return new ResultReply() { Code = ResultReply.Types.StatusCode.Unauthorized, Msg = msg };
        }

        public static ResultReply InnerError(string msg = "")
        {
            return new ResultReply() { Code = ResultReply.Types.StatusCode.InnerError, Msg = msg };
        }
    }
}
