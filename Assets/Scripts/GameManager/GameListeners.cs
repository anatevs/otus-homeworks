using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShootEmUp
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
        public void OnUpdate();
    }

    public interface IPausedUpdate : IGameListener
    {
        public void OnPausedUpdate();
    }
    public interface IFixedUpdate : IGameListener
    {
        public void OnFixedUpdate();
    }
    public interface IPausedFixedUpdate : IGameListener
    {
        public void OnPausedFixedUpdate();
    }
    
}