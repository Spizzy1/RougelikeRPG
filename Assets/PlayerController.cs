using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rigidbody;
    public Vector2 movement;
    PlayerState _state = new Idle();

    private void Start()
    {

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        _state = _state.HandleInput();
    }

    void FixedUpdate()
    {
        if (!_state.Anchored)
        {
            bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            if (isShiftKeyDown)
            {
                rigidbody.MovePosition(rigidbody.position + movement * (movespeed / 2) * Time.fixedDeltaTime);
            }
            else
            {
                rigidbody.MovePosition(rigidbody.position + movement * movespeed * Time.fixedDeltaTime);
            }
        }
    }
    public abstract class PlayerState
    {
        public abstract bool Anchored
        {
            get;
        } 
        public abstract PlayerState HandleInput();
    }
    /*public class Parrying : PlayerState
    {
        
    }*/
    public class Idle : PlayerState
    {
        public override bool Anchored => false;
        public override PlayerState HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                print("State parrying");
                return new Parrying();
            }
            else
            {
                return this;
            }
        }
    }
    public class Parrying : PlayerState
    {
        float time = 0;
        public override bool Anchored => true;
        public override PlayerState HandleInput()
        {
            time += Time.deltaTime;
            if(time > 0.5)
            {
                print("Going idle");
                return new Idle();
            }
            else
            {
                return this;
            }
        }
    }
}
