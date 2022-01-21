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
            if (_instance == null)
            {
                Debug.LogError("Can't find WaveManager instance");
            }

            return _instance;
        }
    }



    [SerializeField] private Wave[] _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _topSpawnPoint;
    [SerializeField] private Transform _bottomSpawnPoint;
    [SerializeField] private double _spawnDelay = 1;
    [SerializeField] private EnemyRoute[] _topRoutes;
    [SerializeField] private EnemyRoute[] _bottomRoutes;

    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private EnemyRoute _EmptyRoute;

    private int _currentWave = 0;
    private int _currentEnemy = 0;
    private int _topSpawnCount = 0;
    private int _bottomSpawnCount = 0;


    private EnemyRoute _currentTopRoute;
    private EnemyRoute _currentBottomRoute;

    private bool _spawningComplete = false;
    private bool _waveComplete = false;
    private bool _gameOver = false;
    private bool _spawningPaused = false;

    private double _nextSpawnTime = -1;
    

    public int kills { get; set; } = 0;
    public int maxBullets { get; set; } = 3;
    public int currentBullets { get; set; } = 0;

    private PlayableDirector _director;

    private int _currentWaveIterations = 1;
    private int _currentIteration = 0;
    private int _totalKills = 0;
    private int _checkPoint = 0;

    

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

        _currentWaveIterations = _waves[0].iterations;

        SignalWaveStart();
        _spawningPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log("Kills: " + kills + " enemies: " + _waves[_currentWave].enemies.Length + " Current Wave: " + _currentWave);

        if(_spawningPaused != true)
            Spawn();

        if(_currentWave >= _waves.Length)
        {
            Debug.Log("GAME WON");
            _gameOver = true;
            return;
        }

        if (_currentIteration >= _currentWaveIterations)
        {
            StartNextWave();
            return;
        }
        

        if(kills >= (_waves[_currentWave].topEnemies.Length + _waves[_currentWave].bottomEnemies.Length ))
        {

            _totalKills += kills;

                _currentIteration++;
                _currentEnemy = 0;
                _topSpawnCount = 0;
                _bottomSpawnCount = 0;
                kills = 0;
            
           
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            StartNextWave();
        }

        

    }

   

    public void Spawn()
    {
        int spawnLocation = Random.Range(0, _spawnPoints.Length);
        
        //Debug.Log("TimeLine Time: " + time);
        if (_currentWave < _waves.Length)
        {
            if (Time.time > _nextSpawnTime)
            {
               // Debug.Log("TRying to Spawn");
               // Debug.Log("Director Time: " + _director.time);
                if (_topSpawnCount < _waves[_currentWave].topEnemies.Length)
                {
                    if(_waves[_currentWave].bossWave == true)
                    {
                        Instantiate(_waves[_currentWave].topEnemies[_currentEnemy], _spawnPoints[spawnLocation].position, Quaternion.AngleAxis(-90,Vector3.forward));
                    }
                    else
                    {
                        //GameObject enemy =  Instantiate(_waves[_currentWave].enemies[_currentEnemy], _spawnPoint.position, Quaternion.identity);
                        // Instantiate(_waves[_currentWave].topEnemies[_currentEnemy], _spawnPoint.position, Quaternion.identity).GetComponent<Enemy>().route = _currentTopRoute;
                        Instantiate(_waves[_currentWave].topEnemies[_currentEnemy], _spawnPoints[spawnLocation].position, Quaternion.identity).GetComponent<Enemy>().route = _currentTopRoute;
                    }
                    _topSpawnCount++;

     
                }
                if (_bottomSpawnCount < _waves[_currentWave].bottomEnemies.Length)
                {
                   // Instantiate(_waves[_currentWave].bottomEnemies[_currentEnemy], _spawnPoint.position, Quaternion.identity).GetComponent<Enemy>().route = _currentBottomRoute;
                    Instantiate(_waves[_currentWave].bottomEnemies[_currentEnemy], _spawnPoints[spawnLocation].position, Quaternion.identity).GetComponent<Enemy>().route = _currentBottomRoute;
                    //_nextSpawnTime = _director.time + _spawnDelay;

                    _bottomSpawnCount++;
                }
                _nextSpawnTime = _director.time + _spawnDelay;
                _currentEnemy++;

                _nextSpawnTime = Time.time + _spawnDelay;

            }
        }
       

    }

    private void StartNextWave()
    {

        if (!_gameOver)
        {
            _currentWave++;
            _spawningPaused = true;
            SignalWaveStart();
            // _waveComplete = true;
            
            _currentEnemy = 0;
            _topSpawnCount = 0;
            _bottomSpawnCount = 0;
            kills = 0;

            _currentTopRoute = _topRoutes[(Random.Range(0, _topRoutes.Length))];
            _currentBottomRoute = _bottomRoutes[(Random.Range(0, _bottomRoutes.Length))];

            Debug.Log("Wave Complete");

            _director.time = 0f;
            _nextSpawnTime = -1;
            
            if(_currentWave<_waves.Length - 1)
                _currentWaveIterations = _waves[_currentWave].iterations;
            _currentIteration = 0;
            _totalKills = 0;

            if(_currentWave == 5)
            {
                _checkPoint = 5;
            }
            if(_currentWave == 11)
            {
                _checkPoint = 11;
            }
        }
    }

    private void SignalWaveStart()
    {
        UI.Instance.NextWave(_currentWave + 1);
        StartCoroutine(PauseForFlash());
    }
   
    IEnumerator PauseForFlash()
    {
        yield return new WaitForSeconds(2f);
        _spawningPaused = false;
    }

    public void StartFromCheckpoint()
    {
        _currentWave = _checkPoint-1;

        StartNextWave();
      
    }
}
