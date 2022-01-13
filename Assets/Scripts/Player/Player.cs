using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //ship control specifics
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _weaponCoolDown = 0.5f;
    [SerializeField] private GameObject[] mainGunObjects;
    [SerializeField] private GameObject _beamWeapon;
    [SerializeField] private GameObject _shields;

    private bool _machineGunActive = false;
    private bool _laserRayActive = false;
    private bool _beamWeaponActive = false;
    private bool _shieldsActive = false;
    private bool _alive = true;
    
    private Animator _animator;
    private Collider _collider;

    private float _nextFireTime = -1;

    private float _startingWeaponCooldown;

    public int upgradeLevel { get; set; }  = 0;

   
    // Start is called before the first frame update
    void Start()
    {
        _startingWeaponCooldown = _weaponCoolDown;

        _animator = GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("Player's animator reference is null");
        _collider = GetComponent<Collider>();
        if (_collider == null)
            Debug.LogError("Player's collider reference is null");

    }

    //1.5 -7.4

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {
            Movement();

            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (upgradeLevel >= 0)
            {
                if (Time.time > _nextFireTime)
                {

                    Debug.Log("Fire");

                    _nextFireTime = Time.time + _weaponCoolDown;

                    if (_machineGunActive)
                    {
                        Instantiate(mainGunObjects[3], transform.position + Vector3.right, Quaternion.AngleAxis(90, Vector3.forward));
                    }
                    else if (_laserRayActive)
                    {
                        Instantiate(mainGunObjects[4], transform.position + Vector3.right, Quaternion.AngleAxis(90, Vector3.forward));
                    }
                    //else if(_beamWeaponActive)
                    //{
                    //    return;
                    //}
                    else
                    {
                        Instantiate(mainGunObjects[upgradeLevel], transform.position + Vector3.right, Quaternion.identity);
                    }

                }
            }
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            //_animator.SetBool("Thrust", true);
            _renderer.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            //_animator.SetBool("Thrust", false);
            _renderer.gameObject.SetActive(false);
        }

        float clampedYPos = Mathf.Clamp(transform.position.y, -6.2f, 3.0f);
        float clampedXpos = Mathf.Clamp(transform.position.x, -3.3f, 3.8f);
        transform.position = new Vector3(clampedXpos, clampedYPos, 0);



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if (_shieldsActive)
            {
                DeactivateShields();
                return;
            }


            
            upgradeLevel--;
            UI.Instance.ChangePower(-1);
            ClearPowerUps();

            if(upgradeLevel<0)
            {
                _alive = false;
                _collider.enabled = false;
                _animator.SetTrigger("Explode");
                Destroy(this.gameObject, 2);
                UI.Instance.GameOver();
            }

        }
    }

    public void PowerUp(int powerUpType)
    {
        switch(powerUpType)
        {
            case 0:  //basic gun upgrade
                upgradeLevel++;
                UI.Instance.ChangePower(1);
                if (upgradeLevel > 2)
                {
                    upgradeLevel = 2;
                    UI.Instance.ChangePower(-1);
                }
               
                break;
            case 1: //machine gun upgrade
                ClearPowerUps();
                _machineGunActive = true;
                _weaponCoolDown = 0.1f;
                break;
            case 2: //laser ray upgrade
                ClearPowerUps();
                _laserRayActive = true;
                _weaponCoolDown = 0.3f;
                break;
            case 3: //beam weapon
                ClearPowerUps();
                ActivateBeam();
                break;
            case 4: //shield
                ActivateShields();
                break;
            default:
                break;
        }
        
    }

    private void ActivateShields()
    {
        _shields.SetActive(true);
        _shieldsActive = true;
    }

    private void DeactivateShields()
    {
        _shields.SetActive(false);
        _shieldsActive = false;
    }

    private void ClearPowerUps()
    {
        _machineGunActive = false;
        _laserRayActive = false;
        _beamWeaponActive = false;
        _beamWeapon.SetActive(false);

        _weaponCoolDown = _startingWeaponCooldown;
    }

    public void ActivateBeam()
    {
        _beamWeaponActive = true;
        _beamWeapon.SetActive(true);
    }

   
}
