using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        if(_playerAnimator==null)
        {
            Debug.LogWarning("El primero hijo del jugador no tiene componente animator");
        }
    }

    public void SetSpeed(float speed)
    {
        _playerAnimator.SetFloat("Speed", speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
