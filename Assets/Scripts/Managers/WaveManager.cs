using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class WaveManager : MonoBehaviour
{
    private static WaveManager _instance;

    public static WaveManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Can't find WaveManager instance");
            }

            return _instance;
        }
    }

    
    
    [SerializeField] private Wave[] _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private double _spawnDelay = 1;
    [SerializeField] private EnemyRoute [] _routes;

    private int _currentWave = 0;
    private int _currentEnemy = 0;
    private int _spawnCount = 0;

    private EnemyRoute _currentRoute;

    private bool _spawningComplete = false;
    private bool _waveComplete = false;

    private double _nextSpawnTime = -1;

    public int kills { get; set; } = 0;

    private PlayableDirector _director;

    private void Awake()
    {
        _instance = this;

       
    }

    // Start is called before the first frame update
    void Start()
    {
        _director = GetComponent<PlayableDirector>();

        if (_director == null)
            Debug.LogError("Director is null");


        _currentRoute = _routes[(Random.Range(0, _routes.Length))];
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log("Kills: " + kills + " enemies: " + _waves[_currentWave].enemies.Length + " Current Wave: " + _currentWave);

        Spawn();

        if(_currentWave >= _waves.Length)
        {
            Debug.Log("GAME WON");
            return;
        }

        if(kills >= _waves[_currentWave].enemies.Length )
        {
            StartNextWave();
        }
    }

   

    public void Spawn()
    {
        //Debug.Log("TimeLine Time: " + time);
        if (_currentWave < _waves.Length)
        {

            if (_spawnCount < _waves[_currentWave].enemies.Length)
            {
                if (_director.time > _nextSpawnTime)
                {
                   //GameObject enemy =  Instantiate(_waves[_currentWave].enemies[_currentEnemy], _spawnPoint.position, Quaternion.identity);
                  Instantiate(_waves[_currentWave].enemies[_currentEnemy], _spawnPoint.position, Quaternion.identity).GetComponent<Enemy>().route = _currentRoute;
                    _nextSpawnTime = _director.time + _spawnDelay;
                    _currentEnemy++;
                    _spawnCount++;

                   // Debug.Log("Spawn Count: " + _spawnCount + " Time: " + _director.time + " next Spawn: " + _nextSpawnTime);
                }

            }
        }
       

    }

    private void StartNextWave()
    {
        _waveComplete = true;
        _currentWave++;
        _currentEnemy = 0;
        _spawnCount = 0;
        kills = 0;

        _currentRoute = _routes[Random.Range(0, _routes.Length)];

        Debug.Log("Wave Complete");

        _director.time = 0f;
        _nextSpawnTime = -1;
    }

   
}
