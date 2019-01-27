using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] Rigidbody Rigidbody;
    public float Speed = 5f;

    private Vector3 _Move;

    public bool IsMoving
    {
        get
        {
            return _Move.magnitude > 0f;
        }
    }    

    private void Update()
    {
        Vector3 forward = this.transform.forward;
        Vector3 right = this.transform.right;

        forward.y = 0;
        right.y = 0;

        this._Move = Vector3.zero;
        this._Move += forward * Input.GetAxis("Vertical");
        this._Move += right * Input.GetAxis("Horizontal");       
    }

    // Update is called once per frame
    void FixedUpdate () {
        Rigidbody.MovePosition(this.transform.position + this._Move * this.Speed);
    }
}