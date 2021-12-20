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
    [SerializeField] private EnemyRoute [] _topRoutes;
    [SerializeField] private EnemyRoute[] _bottomRoutes;

    private int _currentWave = 0;
    private int _currentEnemy = 0;
    private int _topSpawnCount = 0;
    private int _bottomSpawnCount = 0;

    private EnemyRoute _currentTopRoute;
    private EnemyRoute _currentBottomRoute;

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


        _currentTopRoute = _topRoutes[(Random.Range(0, _topRoutes.Length))];
        _currentBottomRoute = _bottomRoutes[(Random.Range(0, _bottomRoutes.Length))];
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

        if(kills >= (_waves[_currentWave].topEnemies.Length + _waves[_currentWave].bottomEnemies.Length ))
        {
            StartNextWave();
        }
    }

   

    public void Spawn()
    {
        //Debug.Log("TimeLine Time: " + time);
        if (_currentWave < _waves.Length)
        {
            if (_director.time > _nextSpawnTime)
            {
                if (_topSpawnCount < _waves[_currentWave].topEnemies.Length)
                {

                    //GameObject enemy =  Instantiate(_waves[_currentWave].enemies[_currentEnemy], _spawnPoint.position, Quaternion.identity);
                    Instantiate(_waves[_currentWave].topEnemies[_currentEnemy], _spawnPoint.position, Quaternion.identity).GetComponent<Enemy>().route = _currentTopRoute;
                   
                   
                    _topSpawnCount++;

                    // Debug.Log("Spawn Count: " + _spawnCount + " Time: " + _director.time + " next Spawn: " + _nextSpawnTime);



                }
                if (_bottomSpawnCount < _waves[_currentWave].bottomEnemies.Length)
                {
                    Instantiate(_waves[_currentWave].bottomEnemies[_currentEnemy], _spawnPoint.position, Quaternion.identity).GetComponent<Enemy>().route = _currentBottomRoute;
                    //_nextSpawnTime = _director.time + _spawnDelay;
                   
                    _bottomSpawnCount++;
                }
                _nextSpawnTime = _director.time + _spawnDelay;
                _currentEnemy++;

            }
        }
       

    }

    private void StartNextWave()
    {
        _waveComplete = true;
        _currentWave++;
        _currentEnemy = 0;
        _topSpawnCount = 0;
        _bottomSpawnCount = 0;
        kills = 0;

        _currentTopRoute = _topRoutes[(Random.Range(0, _topRoutes.Length))];
        _currentBottomRoute = _bottomRoutes[(Random.Range(0, _bottomRoutes.Length))];

        Debug.Log("Wave Complete");

        _director.time = 0f;
        _nextSpawnTime = -1;
    }

   
}
