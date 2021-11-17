using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 5f;
    [SerializeField] private float _width = 10;
    [SerializeField] private float _positionNumber = 0;
    [SerializeField] Transform _firstPieceStart = null;

    private Vector3 _startPos;
    private Vector3 _firstStartingPos;
    private int _totalBackgroundSprites = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _firstStartingPos = _firstPieceStart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        transform.position = transform.position + Vector3.left * Time.deltaTime * _scrollSpeed;

        if (transform.position.x < (_startPos.x - _width * _positionNumber))
        {
          
             transform.position = new Vector3((_firstStartingPos.x) + (_width * (_totalBackgroundSprites-1)), _startPos.y, _startPos.z);
          
        }
    }
}
