using System;
using LittleSocialNetwork.Common.Definitions.Enums;

namespace LittleSocialNetwork.Common.Definitions.Results
{
    public class ServiceResult : IResult
    {
        public ServiceResult()
        {
            Status = ResultStatus.Error;
        }

        public bool IsSuccessed => Status == ResultStatus.Success;

        public string ErrorMessage { get; set; }
        public ResultStatus Status { get; set; }
    }

    public class ServiceResult<TDto> : ServiceResult, IDtoResult<TDto> where TDto : class
    {
        private TDto _serviceResult;

        public TDto Result
        {
            get
            {
                //if (!IsSuccessed)
                //{
                //    throw new ArgumentException("Cannot retrieve value because of empty result or error status");
                //}

                return _serviceResult;
            }
            set => _serviceResult = value;
        }
    };
}