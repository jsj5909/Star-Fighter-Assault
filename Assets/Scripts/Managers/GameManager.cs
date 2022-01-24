using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
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

        DontDestroyOnLoad(this.gameObject);
    }



    public float brightness;

    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
