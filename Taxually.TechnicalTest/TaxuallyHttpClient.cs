﻿using Taxually.TechnicalTest.Interfaces;

namespace Taxually.TechnicalTest
{
    public class TaxuallyHttpClient : IHttpClient
    {
        public Task PostAsync<TRequest>(string url, TRequest request)
        {
            // Actual HTTP call removed for purposes of this exercise
            return Task.CompletedTask;
        }
    }
}
