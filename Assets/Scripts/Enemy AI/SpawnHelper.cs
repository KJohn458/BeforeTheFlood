using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelper : MonoBehaviour
{
    [Tooltip("Close comes first, then medium, and far last.")]
    public Transform[] spawnPoints;

    public Transform wayPoint;
}
