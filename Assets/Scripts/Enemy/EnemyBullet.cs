using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 _shootDirection;

    [SerializeField] private float _speed = 3;

    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
         _player = GameObject.Find("Player").GetComponent<Player>();

        _shootDirection = (_player.transform.position - transform.position).normalized;

        WaveManager.Instance.currentBullets++;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_shootDirection * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            return;
        
        if(other.tag == "Player")
        {
            //harm player

        }
        BulletDeath();
       
    }

    void BulletDeath()
    {
        WaveManager.Instance.currentBullets--;
        Destroy(this.gameObject);
    }

}
