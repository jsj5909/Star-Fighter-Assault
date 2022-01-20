using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public int iterations = 1;
    public bool bossWave = false;
    
    public GameObject[] topEnemies;
    public GameObject[] bottomEnemies;
}
