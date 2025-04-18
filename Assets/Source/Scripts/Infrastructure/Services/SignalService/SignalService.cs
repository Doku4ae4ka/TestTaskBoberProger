﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Infrastructure.Services.SignalService
{
    public class SignalService : ISignalRegister, ISignalSubscriber
    {
        public SignalService(bool isLogsEnable = false)
        {
            IsLogEnabled = isLogsEnable;
        }
        
        private bool IsLogEnabled { get; set; }

        private readonly Dictionary<Type, object> _listeners = new ();

        /// <summary>
        /// Вызывает сигнал 
        /// </summary>
        public void RegistryRaise<T>(T data) where T : struct
        {
            var type = typeof(T);
            if (IsLogEnabled) Debug.Log($"{type}");

            if (_listeners.TryGetValue(type, out var actionList))
            {
                var actions = (List<Action<T>>)actionList;
                for (var index = actions.Count - 1; index >= 0; index--)
                {
                    actions[index].Invoke(data);
                }
            }
        }
        /// <summary>
        /// Вызывает сигнал без создания копии на входе
        /// </summary>
        public void RegistryRaise<T>(ref T data) where T : struct
        {
            var type = typeof(T);
            if (IsLogEnabled) Debug.Log($"{type}");

            if (_listeners.TryGetValue(type, out var actionList))
            {
                var actions = (List<Action<T>>)actionList;
                for (var index = actions.Count - 1; index >= 0; index--)
                {
                    actions[index].Invoke(data);
                }
            }
        }

        public void Subscribe<T>(Action<T> action) where T : struct
        {
            var type = typeof(T);
            if (!_listeners.TryGetValue(type, out var actionList))
            {
                actionList = new List<Action<T>>();
                _listeners[type] = actionList;
            }
            ((List<Action<T>>)actionList).Add(action);
        }

        public void Unsubscribe<T>(Action<T> action) where T : struct
        {
            var type = typeof(T);
            if (_listeners.TryGetValue(type, out var actionList))
            {
                var actions = (List<Action<T>>)actionList;
                actions.Remove(action);
            }
        }
    }
}