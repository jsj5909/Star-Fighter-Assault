using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    private SpriteRenderer _sr;

    private AudioSource _audio;

    public int value = 100;
    // Start is called before the first frame update
    void Start()
    {

        _audio = GetComponent<AudioSource>();
        if (_audio == null)
            Debug.LogError("Audio reference on coin script is null");
        _audio.volume = GameManager.Instance.volume;

        _sr = GetComponent<SpriteRenderer>();
        if (_sr == null)
            Debug.LogError("Sprite renderer ref on coin is null");

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
            _audio.Play();
            _sr.enabled = false;
            UI.Instance.AddScore(value);
            Destroy(this.gameObject,1f);
          
        }
    }

}
