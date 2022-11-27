using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    ///Evento cuando se toma un oro, se pasa su valor y se elimina 
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddGold(value);
            Destroy(gameObject);
        }
    }
}
