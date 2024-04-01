using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class ProjectileProvider : MovableProvider
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collision!");
        if (collider.gameObject.TryGetComponent<UniversalProvider>(out var provider))
        {
            Entity other = provider.Entity;
            //Debug.Log($"collision with {other}");

            if (Entity.GetComponent<Team>().value != other.GetComponent<Team>().value
                && other.Has<Health>())
            {
                if (other.Has<HealthChangeRequest>())
                {
                    other.GetComponent<HealthChangeRequest>().value +=
                        Entity.GetComponent<Damage>().value;
                }
                else
                {
                    other.AddComponent<HealthChangeRequest>().value =
                        Entity.GetComponent<Damage>().value;

                    //Debug.Log($"changed health of {other}");
                }

                Entity.AddComponent<UnspawnRequest>();
            }
        }
    }
}