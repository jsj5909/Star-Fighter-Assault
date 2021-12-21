using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Sprite [] _powerUps;
    [SerializeField] private float _speed = 3;
    
    private Player _player;

    private int _powerUpType;

    //power ups will be basic gun up to level 2
    //machine gun
    //lazer ray
    //beam weapon 
    //shield
    
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        if (_player == null)
            Debug.LogError("Player is Null from Powerup Script");

        SetPowerUpType();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _player.PowerUp(_powerUpType);
            Destroy(this.gameObject);
        }
        if(other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }

    private void SetPowerUpType()
    {
        int playersCurrentLevel = _player.upgradeLevel;
        if (playersCurrentLevel < 2)
        {
            _powerUpType = 0;  //basic weapon upgrade
        }
        else if(_powerUps.Length > 1)
        {
            
            _powerUpType = Random.Range(1, _powerUps.Length);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = _powerUps[_powerUpType];
    }
}
