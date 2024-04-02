﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace imdb_app.Models
{
    // Extension methods for DbContext
    public static class DbContextExtensions
    {
        // Asynchronously executes a raw SQL query and returns the results as a list of entities
        public static async Task<List<T>> SqlQueryAsync<T>(this DbContext db, string sql, object[] parameters = null, CancellationToken cancellationToken = default) where T : class
        {
            // If parameters are not provided, initialize as an empty array
            if (parameters is null)
            {
                parameters = new object[] { };
            }

            // If the type T has properties, perform a query and return the result as a list of entities
            if (typeof(T).GetProperties().Any())
            {
                return await db.Set<T>().FromSqlRaw(sql, parameters).ToListAsync(cancellationToken);
            }
            // Otherwise, execute the SQL query without returning any results
            else
            {
                await db.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
                return default;
            }
        }
    }

    // Class for handling output parameters
    public class OutputParameter<TValue>
    {
        private bool _valueSet = false;

        public TValue _value;

        // Property to access the output parameter value
        public TValue Value
        {
            get
            {
                // If the value is not set, throw an exception
                if (!_valueSet)
                    throw new InvalidOperationException("Value not set.");

                return _value;
            }
        }

        // Sets the value of the output parameter
        internal void SetValue(object value)
        {
            _valueSet = true;

            // If the value is null or DBNull, set the default value for the type TValue, otherwise cast and set the value
            _value = null == value || Convert.IsDBNull(value) ? default(TValue) : (TValue)value;
        }
    }
}
