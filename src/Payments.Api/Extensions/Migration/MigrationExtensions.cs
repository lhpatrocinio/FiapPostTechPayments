﻿using Microsoft.EntityFrameworkCore;
using Payments.Infrastructure.DataBase.EntityFramework.Context;
using System.Diagnostics.CodeAnalysis;

namespace Payments.Api.Extensions.Migration
{
    [ExcludeFromCodeCoverage]
    public static class MigrationExtensions
    {
        public static void ExecuteMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }
        }
    }
}
