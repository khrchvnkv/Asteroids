using System;
using CoreLogic;
using UnityEngine;

namespace UnityLogic
{
    public abstract class Controller : MonoBehaviour
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
            GameCore.Instance.RegisterController(this);
        }
        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }
    }
}