﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace LittleSocialNetwork.Common.Definitions.DependencyResolver
{
    public class DependencyResolver : IDependencyResolver
    {
        private Lazy<IServiceProvider> _provider;

        public IDependencyResolver RegisterServiceProvider(IServiceProvider collection)
        {
            _provider = new Lazy<IServiceProvider>(collection);
            return this;
        }

        public TService GetService<TService>() => 
            (TService)_provider.Value.GetService(typeof(TService));
    }
}