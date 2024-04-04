using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public sealed class WeaponProvider : UniversalProvider
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<UniversalProvider>(out var provider))
        {
            Entity other = provider.Entity;

            if (Entity.GetComponent<Team>().value != other.GetComponent<Team>().value
                && other.Has<Health>())
            {
                int damage = Entity.GetComponent<Damage>().value;
                if (other.Has<TakeDamageEvent>())
                {
                    other.GetComponent<TakeDamageEvent>().value += damage;
                }
                else
                {
                    other.AddComponent<TakeDamageEvent>().value = damage;
                }

                if (Entity.Has<ProjectileFlag>() && !Entity.Has<UnspawnRequest>())
                {
                    Entity.AddComponent<UnspawnRequest>();
                }
            }
        }
    }
}