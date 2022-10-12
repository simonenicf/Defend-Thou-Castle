using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player_character : MonoBehaviour
{
    //components
    private Rigidbody2D _rigidb;
    
    //Input:
    private float _inputHorizontal;
    private float _inputVertical;
    
    // Movement value's
    private float _MoveSpeed = 4f;
    private float _maxSpeed = 0.5f;
    private Vector2 _playerVelocity;
    
    // Direction facing
    // used for determining were to place towers and which idle animation to play
    private int _facingX;
    private int _facingY;
    
    // Animation and states
    private Animator _animator;
    private string _currentstate;
    
    // Walk animations
    private const string PLAYER_WALK_LEFT = "walkLeft";
    private const string PLAYER_WALK_RIGHT = "walkRight";
    private const string PLAYER_WALK_DOWN = "walk down";
    private const string PLAYER_WALK_UP = "WalkUp";
    private const string PLAYER_WALK_UP_RIGHT = "walkRightUp";
    private const string PLAYER_WALK_UP_LEFT = "WalkLeftup";
    private const string PLAYER_WALK_DOWN_RIGHT = "walkRightDown";
    private const string PLAYER_WALK_DOWN_LEFT = "walkLeftdown";
    
    // Idle animations
    private const string PLAYER_IDLE_RIGHT = "idle_right";
    private const string PLAYER_IDLE_LEFT = "idle_left";
    private const string PLAYER_IDLE_DOWN = "idle_down";
    private const string PLAYER_IDLE_UP = "idle_up";
    private const string PLAYER_IDLE_UP_RIGHT = "idle_rightUp";
    private const string PLAYER_IDLE_UP_LEFT = "idle_LeftUp";
    private const string PLAYER_IDLE_DOWN_RIGHT = "idle_RightDown";
    private const string PLAYER_IDLE_DOWN_LEFT = "idle_leftDown";
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
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
        // checks the Movement function
        Movement();
    }
    
    // Movement of character
    private void Movement()
    {
        if (_inputHorizontal != 0 || _inputVertical != 0)
        {
            if (_inputHorizontal != 0 && _inputVertical != 0)
            {
                _rigidb.velocity = new Vector2(_inputHorizontal * _maxSpeed, _inputVertical * _maxSpeed);
            }
            _rigidb.velocity = new Vector2(_inputHorizontal * _MoveSpeed, _inputVertical * _MoveSpeed);
            AnimationSwitchWalkState();

        }
        else
        {
            _rigidb.velocity = new Vector2(0, 0);
            AnimationSwitchIdle();
        }
    }

    // calls which walk animations to switch to
    private void AnimationSwitchWalkState()
    {
        if(_inputVertical > 0 && _inputHorizontal > 0)
        {
            ChangeAnimationState(PLAYER_WALK_UP_RIGHT);
            _facingX = 1;
            _facingY = 1;
        }
        else if(_inputVertical > 0 && _inputHorizontal < 0)
        {
            ChangeAnimationState(PLAYER_WALK_UP_LEFT);
            _facingX = -1;
            _facingY = 1;
        }
        else if(_inputVertical < 0 && _inputHorizontal > 0)
        {
            ChangeAnimationState(PLAYER_WALK_DOWN_RIGHT);
            _facingX = 1;
            _facingY = -1;
        }
        else if(_inputVertical < 0 && _inputHorizontal < 0)
        {
            ChangeAnimationState(PLAYER_WALK_DOWN_LEFT);
            _facingX = -1;
            _facingY = -1;
        }
        else if (_inputHorizontal > 0)
        {
            ChangeAnimationState(PLAYER_WALK_RIGHT);
            _facingX = 1;
            _facingY = 0;
        }
        else if (_inputHorizontal < 0)
        {
            ChangeAnimationState(PLAYER_WALK_LEFT);
            _facingX = -1;
            _facingY = 0;
        }
        else if(_inputVertical > 0)
        {
            ChangeAnimationState(PLAYER_WALK_UP);
            _facingX = 0;
            _facingY = 1;
        }
        else if(_inputVertical < 0)
        {
            ChangeAnimationState(PLAYER_WALK_DOWN);
            _facingX = 0;
            _facingY = -1;
        }
    }

    private void AnimationSwitchIdle()
    {
        if (_facingX > 0 && _facingY > 0)
        {
            ChangeAnimationState(PLAYER_IDLE_UP_RIGHT);
        }
        else if (_facingX < 0 && _facingY > 0)
        {
            ChangeAnimationState(PLAYER_IDLE_UP_LEFT);
        }
        else if (_facingX > 0 && _facingY < 0)
        {
            ChangeAnimationState(PLAYER_IDLE_DOWN_RIGHT);
        }
        else if (_facingX < 0 && _facingY < 0)
        {
            ChangeAnimationState(PLAYER_IDLE_DOWN_LEFT);
        }
        else if (_facingX > 0)
        {
            ChangeAnimationState(PLAYER_IDLE_RIGHT);
        }
        else if (_facingX < 0)
        {
            ChangeAnimationState(PLAYER_IDLE_LEFT);
        }
        else if (_facingY > 0)
        {
            ChangeAnimationState(PLAYER_IDLE_UP);
        }
        else if (_facingY < 0)
        {
            ChangeAnimationState(PLAYER_IDLE_DOWN);
        }
    }
    // Changing animation states
    private void ChangeAnimationState(string newState)
    {
        // Check if a different animation is playing
        // Else continue this animation
        if (_currentstate == newState) return;
        
        // Play new animation
        _animator.Play(newState);
        
        // update current state
        _currentstate = newState;
    }
}
