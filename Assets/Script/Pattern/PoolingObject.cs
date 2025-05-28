using System;
using UnityEngine;

[Serializable]
public class PoolingObject : MonoBehaviour
{
    [HideInInspector][SerializeField] public Pooling pool;
}
