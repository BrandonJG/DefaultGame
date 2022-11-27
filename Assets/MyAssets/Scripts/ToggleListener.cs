using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleListener : MonoBehaviour
{
    Toggle tgl1, tgl2;
    [SerializeField]
    Image _background;
    [SerializeField]
    Sprite nivel1, nivel2;
    Scene_Manager sm;
    
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("Scene Manager").GetComponent<Scene_Manager>();
        tgl1 = GameObject.Find("Toggle1").GetComponent<Toggle>();
        tgl1.onValueChanged.AddListener(delegate
        {
            NivelCambio();
        });
    }

    void NivelCambio()
    {
        if (tgl1.isOn)
        {
            sm.setSelectedLevel("ESCape");
            _background.color = Color.white;
            _background.sprite = nivel1;
        }
        else
        {
            sm.setSelectedLevel("GoldRush");
            _background.color = Color.white;
            _background.sprite = nivel2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
