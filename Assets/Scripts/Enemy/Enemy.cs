using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private float _speed;

    [SerializeField] private float _weaponCoolDown = 2.0f;

    [SerializeField] GameObject _bullet;

    [SerializeField] GameObject _powerUp;

    [SerializeField] private int _health;

    private Player _player;
   
    private float _nextShootTime = -1;

    private int _currentWaypoint = 0;



    public EnemyRoute route { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
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
            _health--;
            if (_health <= 0)
            {
                WaveManager.Instance.kills++;

                SpawnPowerUp();

                Destroy(this.gameObject);
            }
        }

    }

    private void SpawnPowerUp()
    {
        float upgradeChance = Random.Range(1, 10);
        
        if(_player.upgradeLevel < 2)
        { 
            if(upgradeChance < 5)
            {
                Instantiate(_powerUp, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (upgradeChance < 5)//lower after testing is done
            {
                Instantiate(_powerUp, transform.position, Quaternion.identity);
            }
        }
    }

   

}
