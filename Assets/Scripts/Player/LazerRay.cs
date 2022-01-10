using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerRay : MonoBehaviour
{
    [SerializeField] float _speed = 17f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
