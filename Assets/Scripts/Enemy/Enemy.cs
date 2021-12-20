using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private float _speed;

    [SerializeField] private float _weaponCoolDown = 2.0f;

    [SerializeField] GameObject _bullet;

    private GameObject _player;
   
    private float _nextShootTime = -1;

    public EnemyRoute route { get; set; }

    private int _currentWaypoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        if (_player == null)
            Debug.LogError("Player is null");
    }

    // Update is called once per frame
    void Update()
    {

        int maxBullets = WaveManager.Instance.maxBullets;
        int currentBullets = WaveManager.Instance.currentBullets;

        Movement();

        if(Time.time > _nextShootTime && (Vector3.Distance(_player.transform.position,transform.position) < 5.0f) && currentBullets<maxBullets)
        {
            //shoot
            //reset timer
            // Vector3 direction = (_player.transform.position - transform.position).normalized;
            
           Instantiate(_bullet, transform.position, Quaternion.identity);
            _nextShootTime = Time.time + _weaponCoolDown;

        }
       
    }

    private void Movement()
    {

        if (route == null)
            Debug.LogError("Route is Null");
        
        if (Vector3.Distance(transform.position, route.waypoints[_currentWaypoint].transform.position) < 1f)
        {
            _currentWaypoint++;
            if (_currentWaypoint >= route.waypoints.Length)
                _currentWaypoint = 0;

        }

        Vector3 direction = (route.waypoints[_currentWaypoint].position - transform.position);

        transform.up = Vector3.Lerp(transform.up, -direction, Time.deltaTime * _speed / 2);

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet" || other.tag == "Player")
        {
            WaveManager.Instance.kills++;
            Destroy(this.gameObject);
        }

    }


}