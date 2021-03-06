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

    private SpriteRenderer[] _renderers;

    [SerializeField] AudioClip _hitByPlayerSound;
    [SerializeField] AudioClip _explosionSound;


    private AudioSource _audio;



    // Start is called before the first frame update
    void Start()
    {
        UI.Instance.SetBossHealthActive();
        UI.Instance.UpdateBossHealthSlider(_health);

        _renderers = GetComponentsInChildren<SpriteRenderer>();

        _audio = GetComponent<AudioSource>();
        if (_audio == null)
            Debug.LogError("Audio reference on enemy is null");

        _audio.volume = GameManager.Instance.volume;



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
        _audio.PlayOneShot(_hitByPlayerSound);
        
        _health--;

        StartCoroutine(FlashDamage());

        UI.Instance.UpdateBossHealthSlider(_health);
        
        if (_health < 1)
        {
            _audio.PlayOneShot(_explosionSound);
            
            foreach(Animator anim in GetComponentsInChildren<Animator>())
            {
                anim.SetTrigger("Explode");
              
            }
            _alive = false;

            WaveManager.Instance.kills++;
            UI.Instance.totalKills++;

            foreach(Collider collider in GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }

            Destroy(this.gameObject, 2.1f);
           
        }
    }
    IEnumerator FlashDamage()
    {
        foreach(SpriteRenderer sr in _renderers)
        {
            sr.color = Color.red;
        }
        
        yield return new WaitForSeconds(0.3f);

        foreach (SpriteRenderer sr in _renderers)
        {
            sr.color = Color.white;
        }


    }

}
