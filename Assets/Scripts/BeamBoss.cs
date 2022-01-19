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

    [SerializeField] private float _fiveSpreadShotDelay = 0.5f;

    [SerializeField] private float _beamDelay = 2.0f;

    [SerializeField] private GameObject[] _ammoTypes;

    [SerializeField] private float _speed = 2;

    [SerializeField] private Vector3 _top;

    [SerializeField] private Vector3 _bottom;

    

    private Vector3 _destination;

    private bool _atPos1 = false;

    private bool _moving = false;

    private bool _movingUp = false;

    private int _fiveShotFired = 0;

   [SerializeField] private int _currentAmmoType = 0;

    private float _nextFireTime = -1;

    private int _beamsFired = 0;
    
    
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
            
            if(_currentAmmoType == 0)//if shooting the five shot move up and down between firing.
            {
                MoveUpAndDown();
            }
            else if(_currentAmmoType ==1 ) //beam weapon
            {
                MoveUpAndDown();
            }
            
        }

        if (Time.time > _nextFireTime)
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
                    Debug.Log("Moving Down");
                    Debug.Log("Distance: " + Vector3.Distance(transform.position, _bottom));
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
                    if (_fiveShotFired >= _fiveShotMax)
                    {
                        _moving = true;
                        
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
                _beamsFired++;
                _nextFireTime = Time.time + _beamDelay;
                _moving = true;
                break;

            default:
                break;
        }
    }
}
