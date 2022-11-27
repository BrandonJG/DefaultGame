using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comportamiento : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _playerRB;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _maxSpeed = 5f;
    [SerializeField]
    private bool _isRunning;
    [SerializeField]
    private float _horizontalInput;
    [SerializeField]
    private float _forwardInput;

    [SerializeField]
    private int _remainingJumps = 0;
    [SerializeField]
    private int _maxJumps = 1;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private int lifes = 5;
    private bool _availableJump = false;

    private PlayerAnimation _playerAnimation;
    private Vector3 ultimoCheckpoint;
    private Quaternion orientacion;
    private float nivelPiso;
    [SerializeField]
    private Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        if (_playerRB == null)
            Debug.LogWarning("El jugador no tiene rigid body");

        _playerAnimation = GetComponent<PlayerAnimation>();
        if (_playerAnimation == null)
            Debug.LogWarning("El jugador no tiene player animation");
        else
            _playerAnimation.SetSpeed(0);

        ultimoCheckpoint = this.transform.position;
        nivelPiso = this.transform.position.y;
        _speed = _maxSpeed;
        _isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _forwardInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(_horizontalInput, 0, _forwardInput);
        transform.Translate(movement * _speed * Time.deltaTime);

        float _realSpeed = Mathf.Max(Mathf.Abs(_forwardInput), Mathf.Abs(_horizontalInput)) * _speed/_maxSpeed;
        _playerAnimation.SetSpeed(_realSpeed);

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && _remainingJumps > 0)
            _availableJump = true;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            _isRunning = !_isRunning;
            if (_isRunning)
                _speed = _maxSpeed;
            else
                _speed = (float)_maxSpeed / 2f;
        }
        if (this.transform.position.y < nivelPiso - 2 )
            Respawn();
        if(_horizontalInput!=0 || _forwardInput!=0)
        {
            orientacion = this.transform.localRotation;
            this.transform.localRotation = Quaternion.Euler(orientacion.eulerAngles.x, pivot.localRotation.eulerAngles.y, orientacion.eulerAngles.z);
        }
        else
        {
            this.transform.localRotation = orientacion;
        }
    }

    private void FixedUpdate()
    {
        if(_availableJump)
        {
            _playerRB.velocity = Vector3.up * _jumpForce;
            _remainingJumps--;
            _availableJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
            _remainingJumps = _maxJumps;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("DeathTrap"))
            Respawn();
        else if (other.gameObject.CompareTag("EndLevel"))
            ToMainMenu(); 
        else if(other.gameObject.CompareTag("Checkpoint"))
            setCheckpoint();
        
    }

    public void setCheckpoint()
    {
        ultimoCheckpoint = this.transform.position;
    }


    private void Respawn()
    {
        if(lifes == 0)
            ToMainMenu();
        this.transform.position = ultimoCheckpoint;
        lifes--;
    }

    private void ToMainMenu()
    {
        var sm = GameObject.Find("Scene Manager").GetComponent<Scene_Manager>();
        sm.LoadStage("MainMenu");
    }
}
