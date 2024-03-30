using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class ProjectileCollisionManager<Tother> where Tother : UniversalProvider
{
    public void OnCollisionEnter(Entity self, Collision otherCollision)
    {
        if ( otherCollision.gameObject.TryGetComponent<Tother>(out Tother provider))
        {
            Entity other = provider.Entity;

            if (self.GetComponent<Team>().value != other.GetComponent<Team>().value
                && other.Has<Health>())
            {
                if (other.Has<HealthChangeRequest>())
                {
                    other.GetComponent<HealthChangeRequest>().value +=
                        self.GetComponent<Damage>().value;
                }
                else
                {
                    other.AddComponent<HealthChangeRequest>().value =
                        self.GetComponent<Damage>().value;
                }

                self.AddComponent<UnspawnRequest>();
            }
        }
    }
}