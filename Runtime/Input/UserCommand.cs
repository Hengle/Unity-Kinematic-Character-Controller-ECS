﻿using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// 每个角色都有一个UserCommand?
/// </summary>
[Serializable]
[GenerateAuthoringComponent]
public struct UserCommand : IComponentData
{
    public float lookPitch;
    public float lookYaw;
    public float moveYaw;
    public float moveMagnitude;

    public float3 LookDir {
        get { return math.mul(quaternion.Euler(new float3(math.radians(-lookPitch), math.radians(lookYaw), 0)), new float3(0, -1, 0)); }
    }

    public quaternion LookRotation {
        get { return quaternion.Euler(new float3(math.radians(90 - lookPitch), math.radians(lookYaw), 0)); }
    }
}
