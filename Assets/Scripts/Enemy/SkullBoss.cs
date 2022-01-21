using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBoss : MonoBehaviour
{

    [SerializeField] private GameObject[] _skulls;

    [SerializeField] private Vector3 []_rightPositions;
    [SerializeField] private Vector3 [] _leftPositions;
    [SerializeField] private Vector3 _startingPoint;

    [SerializeField] private float _speed = 5;

    [SerializeField] private int _health = 100;

    //-2.61
    private int _currentSKull = 0;
    private bool _movingLeft = false;
    private bool _enteringScene = true;

    private bool _alive = true;

    // Start is called before the first frame update
    void Start()
    {
        UI.Instance.SetBossHealthActive();
        UI.Instance.UpdateBossHealthSlider(_health);
    }

    // Update is called once per frame
    void Update()
    {
       if(_alive)
        {
            if (_enteringScene == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startingPoint, _speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _startingPoint) < 0.2f)
                {
                    _enteringScene = false;
                    _movingLeft = true;
                    _speed = 18;
                }
            }
            else
            {
                if (_movingLeft == true)
                {


                    _skulls[_currentSKull].transform.position = Vector3.MoveTowards(_skulls[_currentSKull].transform.position, _leftPositions[_currentSKull], _speed * Time.deltaTime);

                    if (Vector3.Distance(_skulls[_currentSKull].transform.position, _leftPositions[_currentSKull]) < 0.2f)
                    {
                        _movingLeft = false;
                        _speed = 30;
                    }
                }
                else
                {


                    _skulls[_currentSKull].transform.position = Vector3.MoveTowards(_skulls[_currentSKull].transform.position, _rightPositions[_currentSKull], _speed * Time.deltaTime);

                    if (Vector3.Distance(_skulls[_currentSKull].transform.position, _rightPositions[_currentSKull]) < 0.2f)
                    {
                        _movingLeft = true;
                        //  _currentSKull++;
                        //   Debug.Log("current skull: " + _currentSKull);
                        //  if (_currentSKull > _skulls.Length - 1)
                        //      _currentSKull = 0;
                        _speed = 18;
                        _currentSKull = Random.Range(0, _skulls.Length);
                    }
                }
            }
        }


    }
    public void Damage()
    {
        _health--;
        
        UI.Instance.UpdateBossHealthSlider(_health);
        
        if (_health < 1)
        {
            foreach(Animator anim in GetComponentsInChildren<Animator>())
            {
                anim.SetTrigger("Explode");
              
            }
            _alive = false;
            Destroy(this.gameObject, 2.1f);
           
        }
    }


}
