using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private static UI _instance;

    public static UI Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Can't find UI instance");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _powerText;
    [SerializeField] private Text _waveText;
    [SerializeField] private Text _restartText;
    [SerializeField] private Slider _bossHealthSlider;
    [SerializeField] private float _flashTime = 2f;
    [SerializeField] private Player _player;

    [SerializeField] private Text _debugText;


    private int _score = 0;
    private int _power = 0;
    private bool _gameOver = false;

    public bool _checkpointReached = false;
    

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
        _powerText.text = "Power: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (_debugText.gameObject.activeInHierarchy == false)
                _debugText.gameObject.SetActive(true);
            else
                _debugText.gameObject.SetActive(false);
        }
            if (_gameOver)
            {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if(Input.GetKeyDown(KeyCode.C))
            {
                _player.RestartFromCheckpoint();
                _waveText.gameObject.SetActive(false);
                Debug.Log("Restart from checkpoint");
                _restartText.gameObject.SetActive(false);
                _gameOver = false;
            }
        }
    }

    public void AddScore(int amount)
    {
        _score += amount;
        _scoreText.text = "Score: " + _score;

       // Debug.Log("Score: " + _score);
    }

    public void ChangePower(int amount)
    {
        _power += amount;
        _powerText.text = "Power: " + _power;
       // Debug.Log("Power: " + _power);
    }

    public void GameOver()
    {
        _gameOver = true;
        
        _waveText.text = "GAME OVER";

        _bossHealthSlider.gameObject.SetActive(false);
        
        StartCoroutine(FlashGameOver());

        _restartText.gameObject.SetActive(true);        
    }

    IEnumerator FlashGameOver()
    {
        while (_gameOver == true)
        {
            _waveText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_flashTime);
            _waveText.gameObject.SetActive(false);
            yield return new WaitForSeconds(_flashTime);
           
        }
    }

    public void UpdateBossHealthSlider(int currentHealth)
    {
        
        
        _bossHealthSlider.value = currentHealth;

        if(_bossHealthSlider.value < 1)
        {
            _bossHealthSlider.gameObject.SetActive(false); 
        }
    }

    public void SetBossHealthActive()
    {
        _bossHealthSlider.gameObject.SetActive(true);
    }

    public void DisableBossHealth()
    {
        _bossHealthSlider.gameObject.SetActive(false);
    }

    public void NextWave(int currentWave)
    { 
        
        if(currentWave == 6 || currentWave == 12)
        {
            _waveText.text = "Boss Wave";
        }
        else
            _waveText.text = "Wave " + currentWave;

        StartCoroutine(FlashNextWave());
    }

    public void CheckpointReached()
    {
        _debugText.text = "Checkpoint Reached";

        StartCoroutine(FlashCheckpoint());
    }


    IEnumerator FlashCheckpoint()
    {
        _debugText.gameObject.SetActive(true);
        yield return new WaitForSeconds(_flashTime);
        _debugText.gameObject.SetActive(false); 
        yield return new WaitForSeconds(_flashTime);
        _debugText.gameObject.SetActive(true);
        yield return new WaitForSeconds(_flashTime);
        _debugText.gameObject.SetActive(false);
    }

    IEnumerator FlashNextWave()
    {
        _waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(_flashTime);
        _waveText.gameObject.SetActive(false);
        yield return new WaitForSeconds(_flashTime);
        _waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(_flashTime);
        _waveText.gameObject.SetActive(false);
        yield return new WaitForSeconds(_flashTime);
        Debug.Log("---------------Done----------flashing");
      //  _waveText.gameObject.SetActive(true);
      //  yield return new WaitForSeconds(_flashTime);
     //   _waveText.gameObject.SetActive(false);
      //  yield return new WaitForSeconds(_flashTime);
    }

    

    public void UpdateDebugText(string text)
    {
        _debugText.text = text;
    }
}
