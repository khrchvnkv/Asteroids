using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityLogic.UnityEventSystem
{
    [RequireComponent(typeof(EventSystem))]
    public class EventSystemController : MonoBehaviour
    {
        private EventSystem _eventSystem;

        private void Awake()
        {
            _eventSystem = gameObject.GetComponent<EventSystem>();
            GameCore.Instance.RegisterEventSystem(this);
        }
        public void SetEventSystemActivity(in bool isActivity)
        {
            _eventSystem.enabled = isActivity;
        }
    }
}