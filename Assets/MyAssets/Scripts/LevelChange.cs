using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var sm = GameObject.Find("Scene Manager").GetComponent<Scene_Manager>();
        sm.LoadStage("MainMenu");
    }
}
