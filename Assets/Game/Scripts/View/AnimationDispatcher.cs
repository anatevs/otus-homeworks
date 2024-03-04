using UnityEngine;

public class AnimationDispatcher : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private const string SHOOT = "Shoot";

    public void RecieveEvent(string eventType)
    {
        if (eventType == SHOOT)
        {
            _player.ShootEvent?.Invoke();
        }
    }
}
