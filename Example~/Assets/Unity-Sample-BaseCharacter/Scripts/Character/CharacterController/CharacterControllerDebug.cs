﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

public class CharacterControllerDebug : MonoBehaviour
{
    [SerializeField] private Mesh mesh;

    //public static List<ColliderCastInput> inputs = new List<ColliderCastInput>();
    //public static List<ColliderCastHit> hits = new List<ColliderCastHit>();

    public static ColliderCastInput input;
    public static ColliderCastHit hit;

    EntityQuery CharacterControllerQuery;


    bool Simulating;

    private void Start() {
        CharacterControllerQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(
            typeof(CharacterControllerComponentData),
            typeof(LocalToWorld));

        Simulating = true;
    }

    private void OnDrawGizmos() {
        if (!Simulating) return;

        if (CharacterControllerQuery.CalculateEntityCount() == 0) return;

        var localToWorldArray = CharacterControllerQuery.ToComponentDataArray<LocalToWorld>(Unity.Collections.Allocator.TempJob);
        var localToWorld = localToWorldArray[0];

        //开始查询位置
        Gizmos.color = new Color(0.94f, 0.35f, 0.15f, 0.75f);
        Gizmos.DrawLine(input.Start, input.End - input.End);
        Debug.Log(input.End - input.End);
        Gizmos.DrawWireMesh(mesh, localToWorld.Position, quaternion.identity);

        //击中位置
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(hit.Position, 0.02f);
        Gizmos.DrawWireMesh(mesh,
            math.lerp(input.Start, input.End, hit.Fraction),
            input.Orientation
        );

        localToWorldArray.Dispose();

    }
}
