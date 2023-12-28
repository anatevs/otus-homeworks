using System;
using UnityEngine.UI;

namespace ShootEmUp
{
    [Serializable]
    public class StartGameManagerParams
    {

        public Button startButton;

        public StartCountdownComponent startCountdown;

        public int secondsToStart;

        public int deltaCount;
    }
}
