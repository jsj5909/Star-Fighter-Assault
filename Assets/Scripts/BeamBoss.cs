using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBoss : MonoBehaviour
{

    [SerializeField] private Vector3 _position1;

    [SerializeField] private Vector3 _Position2;

    [SerializeField] private int _fiveShotMax = 10;

    [SerializeField] private int _maxBeams = 10;

    [SerializeField] private float _fiveSpreadShotDelay = 5f;

    [SerializeField] private float _missileFireDelay = 0.3f;

    [SerializeField] private float _beamDelay = 2.0f;

    [SerializeField] private GameObject[] _ammoTypes;

    [SerializeField] private float _speed = 2;

    [SerializeField] private Vector3 _top;

    [SerializeField] private Vector3 _bottom;

    [SerializeField] private int _maxFiredPerType = 20;
    

    private Vector3 _destination;

    private bool _atPos1 = false;

    private bool _moving = false;

    private bool _movingUp = false;

    private int _fiveShotFired = 0;

   [SerializeField] private int _currentAmmoType = 0;

    private float _nextFireTime = -1;

    private int _beamsFired = 0;

    private int _totalFired = 0;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _moving = true;
        _destination = _position1;
        _atPos1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_moving)
        {
            if(_atPos1 == false)
            {
                if (Vector3.Distance(transform.position, _destination) < 0.5f)
                {
                    _atPos1 = true;
                    _moving = false;
                    return;
                }
                else
                {
                    transform.Translate(Vector3.down * _speed * Time.deltaTime);
                    return;
                }
            }

            MoveUpAndDown();
            
        }

        //Debug.Log("Game Time:  " + Time.time + " Next Fire Time: " + _nextFireTime);


        if (Time.time >= _nextFireTime)
        {
           
            FireWeapons();
        }
        
    }

    private void MoveUpAndDown()
    {
        if(_movingUp) 
                {
                    transform.Translate(Vector3.left * _speed * Time.deltaTime);

                    if (Vector3.Distance(transform.position, _top) < 0.5f)
                    {
                        
                        
                        _movingUp = false;
                        _moving = false;
                        if(_currentAmmoType == 0) 
                            _fiveShotFired = 0;
                    }    

                }
                else //moving down
                {
                    //Debug.Log("Moving Down");
                   // Debug.Log("Distance: " + Vector3.Distance(transform.position, _bottom));
                    transform.Translate(Vector3.right * _speed * Time.deltaTime);

                    if (Vector3.Distance(transform.position, _bottom) < 0.5f)
                    {
                        _movingUp = true;
                        _moving = false;
                        if(_currentAmmoType == 0)
                            _fiveShotFired = 0;
                    }   
                }
    }

    private void FireWeapons()
    {


       switch(_currentAmmoType)
        {
            case 0://5 spread

                if (_moving == false)
                {
                    if (_totalFired >= _maxFiredPerType)
                    {
                        _moving = true;
                        _fiveShotFired = 0;
                        
                    }
                    else
                    {
                        Instantiate(_ammoTypes[0], transform.position + Vector3.left, Quaternion.AngleAxis(-180, Vector3.forward));
                        _fiveShotFired++;
                        _nextFireTime = Time.time + _fiveSpreadShotDelay;
                    }
                }
                break;

            case 1: //beam weapons

                Instantiate(_ammoTypes[1], transform.position + (Vector3.left * 5), Quaternion.AngleAxis(90, Vector3.forward));
                //_beamsFired++;
                _nextFireTime = Time.time + _beamDelay;
                _moving = true;
                break;

                
            case 2: //rockets

                GameObject topRocket = Instantiate(_ammoTypes[2], transform.position, Quaternion.AngleAxis(90,Vector3.forward));
                topRocket.GetComponent<BossMissile>().MoveUp();

                GameObject bottomRocket = Instantiate(_ammoTypes[2], transform.position, Quaternion.AngleAxis(90, Vector3.forward));
                bottomRocket.GetComponent<BossMissile>().MoveDown();

                _nextFireTime = Time.time + _missileFireDelay;
                _moving = true;
                break;

            default:
                break;

              

        }
        _totalFired++;
        if(_totalFired >= _maxFiredPerType)
        {
            _totalFired = 0;
            _currentAmmoType++;
            if(_currentAmmoType > _ammoTypes.Length-1)
            {
                Debug.Log("Resetting fire cycle");
                _currentAmmoType = 0;
                _moving = false;
            }
        }
    }
}
