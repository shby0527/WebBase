using System;
using Microsoft.AspNetCore.Builder;

namespace SampleMiddleware.Middleware
{
    public static class SamplewareExtention
    {
        public static IApplicationBuilder UseSampleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Sampleware>();
        }
    }
}