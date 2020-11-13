﻿using Autofac;
using EasySurvey.Core.Entities;
using EasySurvey.Core.Events;
using EasySurvey.Infrastructure;
using EasySurvey.Infrastructure.DomainEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace EasySurvey.UnitTests.Core.DomainEvents
{
    public class DomainEventDispatcherShould
    {
        [Fact]
        public void NotReturnAnEmptyListOfAvailableHandlers()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DefaultInfrastructureModule(isDevelopment: true));
            builder.RegisterType<NullLoggerFactory>().As<ILoggerFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
            var container = builder.Build();

            var domainEventDispatcher = new DomainEventDispatcher(container);
            var toDoItemCompletedEvent = new ToDoItemCompletedEvent(new ToDoItem());

            var handlersForEvent = domainEventDispatcher.GetWrappedHandlers(toDoItemCompletedEvent);

            Assert.NotEmpty(handlersForEvent);
        }
    }
}
