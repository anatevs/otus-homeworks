using UnityEngine;

public class PlayerAnimationDispatcher : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private const string SHOOT = "Shoot";
    private const string DESTROY = "Destroy";

    public void RecieveEvent(string eventType)
    {
        if (eventType == SHOOT)
        {
            _player.ShootEvent?.Invoke();
        }

        if (eventType == DESTROY)
        {
            _player.onDestroy.Value = true;
        }
    }
}
