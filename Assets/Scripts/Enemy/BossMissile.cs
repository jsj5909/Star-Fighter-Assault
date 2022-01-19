using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : MonoBehaviour
{
    private bool _moveUp = false;
    private bool _moveDown = false;
    private bool _positioning = true;

    private Vector3 _top;
    private Vector3 _bottom;
    private Vector3 _target;

    private Player _player;

    private float _speed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        _top = transform.position + new Vector3(0, 4, 0);
        _bottom = transform.position - new Vector3(0, 4, 0);

        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player reference is null in Boss Missile Script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_positioning == true)
        {
            if (_moveUp)
            {
                transform.position = Vector3.MoveTowards(transform.position, _top, _speed * Time.deltaTime);

                if(Vector3.Distance(transform.position, _top) < 0.3)
                {
                    _positioning = false;
                    _target = _player.transform.position;

                }
            }

            if(_moveDown)
            {
                transform.position = Vector3.MoveTowards(transform.position, _bottom, _speed * Time.deltaTime);
                
                if(Vector3.Distance(transform.position, _bottom) < 0.3)
                {
                    _positioning = false;
                    _target = _player.transform.position;

                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);



            if (Vector3.Distance(transform.position, _target) < 0.3)
            {
                Destroy(this.gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }


    public void MoveUp()
    {
        _moveUp = true;

    }
    public void MoveDown()
    {
        _moveDown = true;
    }

}
