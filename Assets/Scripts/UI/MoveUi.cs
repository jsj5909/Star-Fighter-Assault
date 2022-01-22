using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUi : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 _destination;
    [SerializeField] float _speed = 5;

    private RectTransform _rect;

    private bool _atDestination = false;
    
    void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_atDestination == false)
        {
            _rect.anchoredPosition = Vector2.MoveTowards(_rect.anchoredPosition, _destination, _speed * Time.deltaTime);
            
            
            //transform.localPosition = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);

            if (Vector2.Distance(_rect.anchoredPosition, _destination) < 0.1)
                _atDestination = true;
            
        }
    }
}
