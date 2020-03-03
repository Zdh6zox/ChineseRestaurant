using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public Vector3 SpawnPos;
    public Quaternion SpawnRot;

    public void GetSpawnPosAndRot(out Vector3 pos,out Quaternion rot)
    {
        pos = SpawnPos;
        rot = SpawnRot;
    }
}
