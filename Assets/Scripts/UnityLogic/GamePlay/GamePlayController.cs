using UnityEngine;
using UnityLogic.GamePlay.Player;

namespace UnityLogic.GamePlay
{
    public class GamePlayController : Manager
    {
        [SerializeField] private GameObject gamePlayController;
        [SerializeField] private PlayerController playerController;
        
        protected override void Initialize()
        {
            base.Initialize();
            playerController.Initialize();
        }
        public void StartGame()
        {
            gamePlayController.SetActive(true);
            playerController.SetBehaviourActivity(true);
        }
    }
}