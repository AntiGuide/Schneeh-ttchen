using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] Transform Feet;
    [SerializeField] LayerMask Ground;

    public float Speed = 5f;
    public float GroundDistance = 0.2f;

    private Vector3 _Move;

    private void Update()
    {

        Vector3 forward = this.transform.forward;
        Vector3 right = this.transform.right;

        forward.y = 0;
        right.y = 0;

        this._Move = Vector3.zero;
        this._Move += forward * Input.GetAxis("Vertical");
        this._Move += right * Input.GetAxis("Horizontal");
        this._Move = Vector3.ClampMagnitude(this._Move, 1);

    }

    // Update is called once per frame
    void FixedUpdate () {
        Rigidbody.MovePosition(this.transform.position + this._Move * this.Speed);     
    }
}
