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

    [SerializeField] public int _points;

    [SerializeField] private GameObject _coin;

    private Player _player;
   
    private float _nextShootTime = -1;

    private int _currentWaypoint = 0;

    private Animator _animator;

    private bool _alive = true;

    private Collider _collider;

    public EnemyRoute route { get; set; }

    

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        if (_player == null)
            Debug.LogError("Enemy: Player reference is null");

        _animator = GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("Enemy Animator is Null");

        _collider = GetComponent<Collider>();
        if (_collider == null)
            Debug.LogError("Enemy Collider is Null");
    }

    // Update is called once per frame
    void Update()
    {

        int maxBullets = WaveManager.Instance.maxBullets;
        int currentBullets = WaveManager.Instance.currentBullets;

        Movement();

        if (_alive && _player != null)
        {
            if (Time.time > _nextShootTime && (Vector3.Distance(_player.transform.position, transform.position) < 5.0f) && currentBullets < maxBullets)
            {

                Instantiate(_bullet, transform.position, Quaternion.identity);
                _nextShootTime = Time.time + _weaponCoolDown;

            }
        }
    }

    private void Movement()
    {

        if (route == null)
            Debug.LogError("Route is Null");

        if (_alive)
        {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet" || other.tag == "Player")
        {
            _health--;
            if (_health <= 0)
            {
                _alive = false;
                _animator.SetTrigger("Explode");

                // UI.Instance.AddScore(_points);

                Coin coin = Instantiate(_coin, transform.position, Quaternion.identity).GetComponent<Coin>();
                coin.value = _points;

                _collider.enabled = false;

                WaveManager.Instance.kills++;

                SpawnPowerUp();

                Destroy(this.gameObject,2);
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
            if (upgradeChance < 2)//lower after testing is done
            {
                Instantiate(_powerUp, transform.position, Quaternion.identity);
            }
        }
    }

   

}
