using System;

namespace Source.Scripts.Infrastructure.Services.SignalService
{
    public interface ISignalSubscriber : ISignalHandler
    {
        void Subscribe<T>(Action<T> action) where T : struct;
        
        void Unsubscribe<T>(Action<T> action) where T : struct;
    }

    public interface ISignalRegister : ISignalHandler
    {
        
        void RegistryRaise<T>(T data) where T : struct;
        void RegistryRaise<T>(ref T data) where T : struct;
    }

    public interface ISignalHandler : IService { }
}