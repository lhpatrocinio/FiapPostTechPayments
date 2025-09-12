﻿using System;

namespace Payments.Api.Extensions.Logs.Models
{
    public class LogGuid
    {
        private LogGuid(Guid value) => GuidValue = value;

        public static LogGuid GetLog(Guid value) => new LogGuid(value);

        public Guid GuidValue { get; set; }
    }
}
