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
                    // SetDestination を呼ぶ前にエージェントが NavMesh 上にあるか確認する
                    if (!agent.isOnNavMesh)
                    {
                        // 近傍の NavMesh 位置を探索して見つかれば Warp して配置する
                        NavMeshHit hit;
                        // まずはエージェントの現在位置周辺を試す
                        if (NavMesh.SamplePosition(agent.transform.position, out hit, 2.0f, NavMesh.AllAreas))
                        {
                            agent.Warp(hit.position);
                        }
                        else if (NavMesh.SamplePosition(position, out hit, 2.0f, NavMesh.AllAreas))
                        {
                            // フォールバック: 目標位置近傍に NavMesh があればそちらに Warp する
                            agent.Warp(hit.position);
                        }
                        else
                        {
                            UnityEngine.Debug.LogWarning($"NavMeshMoveService.Move: id={id} のエージェントは NavMesh 上に存在せず、近傍にも NavMesh が見つかりませんでした。Move をスキップします。");
                            return;
                        }
                    }

                    agent.SetDestination(position);
                }
                catch (System.Exception ex)
                {
                    UnityEngine.Debug.LogWarning($"NavMeshMoveService.Move: エージェント id={id} の移動処理中に例外が発生しました: {ex.Message}");
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
