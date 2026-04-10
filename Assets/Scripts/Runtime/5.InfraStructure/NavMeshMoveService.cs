using Application;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace InfraStructure
{
    public class NavMeshMoveService : IMoveService
    {
        public void Register(Guid id, NavMeshAgent agent)
        {
            _agents[id] = agent;
        }

        public void Move(Guid id, Vector3 position)
        {
            if (_agents.TryGetValue(id, out var agent))
            {
                try
                {
                    // Ensure agent is on NavMesh before calling SetDestination
                    if (!agent.isOnNavMesh)
                    {
                        // Try to place agent onto nearest NavMesh position
                        NavMeshHit hit;
                        // First try near agent current position
                        if (NavMesh.SamplePosition(agent.transform.position, out hit, 2.0f, NavMesh.AllAreas))
                        {
                            agent.Warp(hit.position);
                        }
                        else if (NavMesh.SamplePosition(position, out hit, 2.0f, NavMesh.AllAreas))
                        {
                            // fallback: warp to near target position
                            agent.Warp(hit.position);
                        }
                        else
                        {
                            UnityEngine.Debug.LogWarning($"NavMeshMoveService.Move: Agent for id={id} is not on a NavMesh and no nearby NavMesh position found. Skipping Move.");
                            return;
                        }
                    }

                    agent.SetDestination(position);
                }
                catch (System.Exception ex)
                {
                    UnityEngine.Debug.LogWarning($"NavMeshMoveService.Move: Exception when moving agent id={id}: {ex.Message}");
                }
            }
        }

        public void Stop(Guid id)
        {
            if (_agents.TryGetValue(id, out var agent))
            {
                agent.ResetPath();
            }
        }

        public Vector3 GetPosition(Guid id)
        {
            if (_agents.TryGetValue(id, out var agent))
            {
                return agent.transform.position;
            }

            return Vector3.zero;
        }

        private readonly Dictionary<Guid, NavMeshAgent> _agents = new();
    }
}
