using System;
using System.Collections.Generic;
using System.Text;
using Application.Helpers;

namespace Application.Common
{
    public static class LoggingExtensions
    {
        public static string GetLoggingData(this Params request, string callerName, int level = 0)
        {
            var requestLogDataBuilder = new StringBuilder();

            requestLogDataBuilder.Indent(level++).Append($" {callerName} - {nameof(request)} Data: ");
            requestLogDataBuilder
                .AppendLine().Indent(level).Append($"{nameof(request.Category)}: {request?.Category}")
                .AppendLine().Indent(level).Append($"{nameof(request.PageNumber)}: {request?.PageNumber}")
                .AppendLine().Indent(level).Append($"{nameof(request.PageSize)}: {request?.PageSize}");
            //request?.FieldGroups?.ToList().ForEach(fieldGroup =>
            //{
            //    requestLogDataBuilder
            //        .AppendLine().Indent(level).Append(value: $"[FieldGroup: {fieldGroup}]");
            //});

            return requestLogDataBuilder.ToString();
        }

        public static string GetLoggingData(this Posts.Details.Query request, string callerName, int level = 0)
        {
            var requestLogDataBuilder = new StringBuilder();

            requestLogDataBuilder.Indent(level++).Append($" {callerName} - {nameof(request)} Data: ");
            requestLogDataBuilder
                .AppendLine().Indent(level).Append($"{nameof(request.Id)}: {request?.Id}");

            return requestLogDataBuilder.ToString();
        }

        public static StringBuilder Indent(this StringBuilder builder, int level, string padding = "  ")
        {
            while (0 <= --level)
                builder.Append(padding);

            return builder;
        }
    }
}
