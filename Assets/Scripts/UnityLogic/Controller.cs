using System;
using CoreLogic;
using UnityEngine;

namespace UnityLogic
{
    public abstract class Manager : MonoBehaviour
    {
        protected EventManager EventManager => GameCore.Instance.EventManager;
        
        private void Awake()
        {
            Inject();
        }
        private void OnEnable()
        {
            Subscribe();
        }
        private void OnDisable()
        {
            Unsubscribe();
        }
        protected virtual void Inject()
        {
            GameCore.Instance.RegisterManager(this);
        }
        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }
    }
}