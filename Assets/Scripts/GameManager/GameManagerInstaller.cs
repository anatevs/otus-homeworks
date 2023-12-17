using UnityEngine;



namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    
    public class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField] private GameObject[] sceneObjectsWithBehaviour;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
            GameObject[] rootGameObjects = gameObject.scene.GetRootGameObjects();
            _gameManager.AddListeners(rootGameObjects);            
        }
    }
}