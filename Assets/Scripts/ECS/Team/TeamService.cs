using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeamService
{
    private readonly Dictionary<TeamType, List<Entity>> _teamContainers = new();

    public void Init()
    {
        foreach (TeamType team in Enum.GetValues(typeof(TeamType)))
        {
            _teamContainers[team] = new List<Entity>();
        }
    }

    public void AddToTeam(Entity entity)
    {
        TeamType selfTeam = entity.GetComponent<Team>().value;
        _teamContainers[selfTeam].Add(entity);
    }

    public void RemoveFromTeam(Entity entity)
    {
        TeamType selfTeam = entity.GetComponent<Team>().value;
        _teamContainers[selfTeam].Remove(entity);
    }

    private Entity FindNearest(Vector3 point, List<Entity> members)
    {
        float minSqrDistance = float.MaxValue;
        Entity nearest = null;
        for (int i = 0; i < members.Count; i++)
        {
            float currSqrDistance =
                    Vector3.SqrMagnitude(point - members[i].GetComponent<Position>().value);
            if (currSqrDistance < minSqrDistance)
            {
                minSqrDistance = currSqrDistance;
                nearest = members[i];
            }
        }
        return nearest;
    }

    public Entity FindNearestEnemy(Vector3 point, TeamType selfTeam)
    {
        return FindNearest(point, _teamContainers[(TeamType) (((int)selfTeam + 1)%2)]);
    }
}