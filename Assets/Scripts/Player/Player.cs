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

  

    private int upgradeLevel = 2;
    

    private Animator _animator;

    private float _nextFireTime = -1;


   

    // Start is called before the first frame update
    void Start()
    {
        

    }

    //1.5 -7.4

    // Update is called once per frame
    void Update()
    {
        Movement();

        FireWeapon();
    }

    private void FireWeapon()
    {
        if (Input.GetKey(KeyCode.Space))
        {

            if (Time.time > _nextFireTime)
            {

                Debug.Log("Fire");

                _nextFireTime = Time.time + _weaponCoolDown;

                Instantiate(mainGunObjects[upgradeLevel], transform.position + Vector3.right, Quaternion.identity);

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
            //harm player
            //if player health - 0 then destroy
        }
    }
}
