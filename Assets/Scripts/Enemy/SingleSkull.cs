using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSkull : MonoBehaviour
{
    private SkullBoss _boss;
    
    // Start is called before the first frame update
    void Start()
    {
        _boss = GetComponentInParent<SkullBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            _boss.Damage();
        }
    }


}
