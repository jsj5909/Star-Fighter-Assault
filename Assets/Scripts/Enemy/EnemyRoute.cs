using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Route", menuName = "Routes")]
public class EnemyRoute : ScriptableObject
{
    public Transform[] waypoints;
    public bool line = false;

    

}
