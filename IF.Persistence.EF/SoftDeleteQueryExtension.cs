﻿using IF.Core.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace IF.Persistence.EF
{
    public static class SoftDeleteQueryExtension
    {

        public  static void SoftDeleteEnabled(this ModelBuilder modelBuilder)
        {

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }
        }
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension)
                .GetMethod(nameof(GetSoftDeleteFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall.Invoke(null, new object[] { });
            entityData.SetQueryFilter((LambdaExpression)filter);
            entityData.AddIndex(entityData.
                 FindProperty(nameof(ISoftDelete.SoftDeleted)));
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : class, ISoftDelete
        {
            Expression<Func<TEntity, bool>> filter = x => !x.SoftDeleted;
            return filter;
        }
    }
}
