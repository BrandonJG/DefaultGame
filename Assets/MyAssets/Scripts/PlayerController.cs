using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    ///public Rigidbody theRB;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    float CurrentFallTime;
    float MaxFallTime = 2;
    bool PlayerFalling;
    bool justRespawned = false;
    // Start is called before the first frame update
    void Start()
    {
        //theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (justRespawned && controller.isGrounded)
        {
            FindObjectOfType<GameManager>().LoseLife();
            justRespawned = false;
        }
        //Preguntar si el contador del golpe es menor de cero para saber si recibio daño el jugador
        if (knockBackCounter <= 0)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;
            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    moveDirection.y = jumpForce;
                }
                PlayerFalling = false;
                CurrentFallTime = 0;
            }
            if(controller.isGrounded== false)
            {
                PlayerFalling = true;
            }
            if (PlayerFalling)
            {
                CurrentFallTime += Time.deltaTime;
            }
        }else
        {
            knockBackCounter -= Time.deltaTime;
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        //Mover el jugador en diferentes direcciones de acuerdo a donde este apuntando la direccion de la camara
        if(Input.GetAxis("Vertical") !=0 || Input.GetAxis("Horizontal") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        if(CurrentFallTime >= MaxFallTime)
        {
            FindObjectOfType<HealthManager>().Respawn();
            justRespawned = true;
        }
    }

    public void KnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }
}
