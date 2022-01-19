using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    public int value = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Border")
        {
            Destroy(this.gameObject);
        }

        if(other.tag == "Player")
        {
            UI.Instance.AddScore(value);
            Destroy(this.gameObject);
        }
    }

}
