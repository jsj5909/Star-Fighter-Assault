using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField] float _speed = 25f;

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
        if (other.tag == "Enemy")
        {

            // Destroy(other.gameObject);
            Destroy(this.gameObject);
            //do this on player
        }

        if (other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
