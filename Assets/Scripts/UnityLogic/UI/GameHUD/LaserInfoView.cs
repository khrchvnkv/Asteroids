using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityLogic.UI.GameHUD
{
    public class LaserInfoView : MonoBehaviour
    {
        [SerializeField] private Slider reloadSlider;
        [SerializeField] private GameObject laserShotSlot;

        private List<GameObject> _slots = new List<GameObject>();
        
        public void Inject(in int shotCount)
        {
            reloadSlider.gameObject.SetActive(false);
            var slotParent = laserShotSlot.transform.parent;
            if (shotCount < 1)
            {
                Debug.LogWarning($"Not correct laser slots count: {shotCount}");
            }
            if (_slots.Count < shotCount)
            {
                for (int i = 0; i < shotCount; i++)
                {
                    if (_slots.Count < i + 1)
                    {
                        _slots.Add(null);
                    }
                    if (i == 0)
                    {
                        _slots[i] = laserShotSlot;
                    }
                    else
                    {
                        _slots[i] = Instantiate(laserShotSlot, slotParent);
                    }
                }
            }
        }
        public void EnableSlider()
        {
            reloadSlider.value = 0.0f;
            reloadSlider.gameObject.SetActive(false);
        }
        public void DisableSlider()
        {
            reloadSlider.gameObject.SetActive(false);
        }
        public void SetShootCount(int shootCount)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].SetActive(i + 1 <= shootCount);
            }   
        }
        public void SetReloadProgressValue(float progress)
        {
            if (!reloadSlider.gameObject.activeInHierarchy) reloadSlider.gameObject.SetActive(true);
            reloadSlider.value = progress;
        }
    }
}