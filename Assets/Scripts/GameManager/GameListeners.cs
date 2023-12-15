using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
{
    public class GameListeners : MonoBehaviour
    {
        public interface IGameListener
        {

        }

        public interface IStartGame : IGameListener
        {
            public void OnStart();

        }

        public interface IFinishGame : IGameListener
        {
            public void OnFinish();
        }

        public interface IPauseGame : IGameListener
        {
            public void OnPause();
        }
        public interface IResumeGame : IGameListener
        {
            public void OnResume();
        }

        public interface IUpdate : IGameListener
        {
            public bool Enabled { get; }
            public void OnUpdate();
        }
        public interface IFixedUpdate : IGameListener
        {
            public bool Enabled { get; }
            public void OnFixedUpdate();
        }
    }
}