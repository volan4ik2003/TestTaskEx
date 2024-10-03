using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IState
    {
        private const string spawnPointTag = "HeroSpawnPoint";
        private const string virtualCameraTag = "VirtualCamera";
        private GameStateMachine _gameStateMachine;
        private IGameFactory _gameFactory;
        private Vector3 _spawnPoint;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            SceneManager.LoadSceneAsync("SampleScene").completed += OnLoaded;
        }
        
        private void OnLoaded(AsyncOperation operation)
        {
            InitGameWorld();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            var spawnPointObject = GameObject.FindWithTag(spawnPointTag);
            if (spawnPointObject != null)
            {
                _spawnPoint = spawnPointObject.transform.position;
            }
            else
            {
                _spawnPoint = Vector3.zero;
            }

            GameObject hero = _gameFactory.SpawnHero(_spawnPoint);

            var virtualCameraObject = GameObject.FindWithTag(virtualCameraTag);
            if (virtualCameraObject != null)
            {
                _cinemachineVirtualCamera = virtualCameraObject.GetComponent<CinemachineVirtualCamera>();
                _cinemachineVirtualCamera.Follow = hero.transform;
            }
        }

        public void Exit()
        {
           
        }
    }
}