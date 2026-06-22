using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;

    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;

    public GameObject Bullet;
    public float BulletSpeed = 100f;
    private bool _isShooting;

    private float _vInput;
    private float _hInput;

    public float JumpVelocity = 5f;
    private bool _isJumping;

    private Rigidbody _rb;

    private GameBehavior _gameManager;

    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        _isJumping |= Input.GetKeyDown(KeyCode.J);
        _isShooting |= Input.GetKeyDown(KeyCode.Space);
        _vInput = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;
        //this.transform.Translate(Vector3.forward* _vInput*Time.deltaTime);
        //this.transform.Rotate(Vector3.up * _hInput*Time.deltaTime);

    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(angleRot * _rb.rotation);
        if (_isJumping && IsGrounded())
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);

            playerJump();
        }
        _isJumping = false;

        if (_isShooting)
        {
            GameObject newBullet = Instantiate(Bullet, this.transform.position + new Vector3(0, 0, 1), this.transform.rotation) as GameObject;
            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>();
            BulletRB.velocity=this.transform.forward * BulletSpeed;
        }
        _isShooting = false;
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer);
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="Enemy")
        {
            _gameManager.HP -= 1;
        }
    }
}

