using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiveSpreadBullet : MonoBehaviour
{
    [SerializeField] float _speed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
