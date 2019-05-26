using System;
using dogma.Backend;
using dogma.Backend.Compiler;
using dogma.Backend.Interpreter;
using dogma.Frontend;
using dogma.Frontend.Pascal;
using dogma.Message;
using dogma.Timing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Backend
{
    [TestClass]
    public class BackendFactoryTest
    {
        [TestMethod]
        public void CreateBackend_ForCompiler()
        {
            var backendFactory = new BackendFactory(Mock.Of<IMessageHandler>(), Mock.Of<ITimeProvider>());
            var backend = backendFactory.CreateBackend("compile");

            backend.Should().BeOfType(typeof(CodeGenerator));
        }
        
        [TestMethod]
        public void CreateBackend_ForExecute()
        {
            var backendFactory = new BackendFactory(Mock.Of<IMessageHandler>(), Mock.Of<ITimeProvider>());
            var backend = backendFactory.CreateBackend("execute");

            backend.Should().BeOfType(typeof(Executor));       }
        
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CreateBackend_ThrowsExceptionForUnknownOperation()
        {
            var backendFactory = new BackendFactory(Mock.Of<IMessageHandler>(), Mock.Of<ITimeProvider>());
            backendFactory.CreateBackend("unknown");
        }
    }
}