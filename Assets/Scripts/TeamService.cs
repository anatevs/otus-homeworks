using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

public sealed class TeamService<T> where T : ITeam
{
    private readonly List<Entity> _members = new List<Entity>();

    private readonly T _team;

    public TeamService(T team)
    {
        _team = team;
    }

    public void AddToTeam(Entity entity)
    {
        if (entity.GetComponent<Team>().value != _team.TeamType)
        {
            Debug.Log($"try to add entity of team " +
                $"{entity.GetComponent<Team>().value}" +
                $" to the TeamService with team {_team.TeamType}");
            return;
        }
        else
        {
            _members.Add(entity);
        }
    }

    public void AddToTeam(IEnumerable<Entity> entities)
    {
        foreach (Entity entity in entities)
        {
            _members.Add(entity);
        }
    }

    public void RemoveFromTeam(Entity entity)
    {
        _members.Remove(entity);
    }

    public Entity FindNearest(Vector3 point)
    {
        float minSqrDistance = float.MaxValue;
        Entity nearest = null;
        for (int i = 0; i < _members.Count; i++)
        {
            float currSqrDistance =
                    Vector3.SqrMagnitude(point - _members[i].GetComponent<Position>().value);
            if (currSqrDistance < minSqrDistance)
            {
                minSqrDistance = currSqrDistance;
                nearest = _members[i];
            }
        }
        return nearest;
    }
}