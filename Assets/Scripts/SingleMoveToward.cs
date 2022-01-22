using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMoveToward : MonoBehaviour
{
    [SerializeField] private Vector3 _destination;
    [SerializeField] private float _speed = 5;

   

    private bool _atDestination = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_atDestination == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);


         
            if (Vector2.Distance(transform.position, _destination) < 0.1)
                _atDestination = true;

        }
    }
}
