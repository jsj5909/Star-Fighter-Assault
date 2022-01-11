using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private int _score = 0;
    private int _power = 0;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 0";
        _powerText.text = "Power: 0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int amount)
    {
        _score += amount;
        _scoreText.text = "Score: " + _score;

        Debug.Log("Score: " + _score);
    }

    public void ChangePower(int amount)
    {
        _power += amount;
        _powerText.text = "Power: " + _power;
        Debug.Log("Power: " + _power);
    }

}
