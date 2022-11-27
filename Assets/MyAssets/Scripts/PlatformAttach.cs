using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    [SerializeField]
    GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
            _player.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
            _player.transform.parent = null;
    }
}
