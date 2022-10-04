using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player_character : MonoBehaviour
{
    //componets
    private Rigidbody2D _rigidb;
    
    //Input:
    private float _inputHorizontal;

    private float _inputVertical;
    // Value's
    private float _MoveSpeed = 4f;
    private float _maxSpeed = 0.5f;
    private Vector2 _playerVelocity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement input
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Movement();
    }
    
    private void Movement()
    {
        if (_inputHorizontal != 0 || _inputVertical != 0)
        {
            if (_inputHorizontal != 0 && _inputVertical != 0)
            {
                _rigidb.velocity = new Vector2(_inputHorizontal * _maxSpeed, _inputVertical * _maxSpeed);
            }
            _rigidb.velocity = new Vector2(_inputHorizontal * _MoveSpeed, _inputVertical * _MoveSpeed);
        }
        else
        {
            _rigidb.velocity = new Vector2(0, 0);
        }
    }
}
