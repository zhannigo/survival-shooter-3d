using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace CodeBase.Enemy
{
  public class AgentMoveToPlayer: MonoBehaviour
  {
    public NavMeshAgent Agent;
    private const float MinimalDistance = 1;
    private Transform _heroTransform;

    public void Construct(Transform heroTransform) => 
      _heroTransform = heroTransform;
    
    private void Update()
    {
      if (_heroTransform!=null && HeroNotReached())
      {
        Agent.destination = _heroTransform.position;
      }
    }

    private bool HeroNotReached()
    {
      return Vector3.Distance(Agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
  }
}