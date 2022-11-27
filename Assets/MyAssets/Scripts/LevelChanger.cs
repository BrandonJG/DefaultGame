using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    Button _btn;
    // Start is called before the first frame update
    void Start()
    {
        _btn = GameObject.Find("PlayBtn").GetComponent<Button>();
        _btn.onClick.AddListener(LoadLevel);
    }

    public void LoadLevel()
    {
        var sm = GameObject.Find("Scene Manager").GetComponent<Scene_Manager>();
        sm.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
