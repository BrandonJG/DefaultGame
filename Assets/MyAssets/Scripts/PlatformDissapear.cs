using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDissapear : MonoBehaviour
{
    private GameObject _parent;
    private MeshCollider _platformBase;
    private Renderer _platformRender;
    private bool _canDetect;


    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent.gameObject; //PlataformaFísica (Desaparecer)
        _platformBase = this.GetComponent<MeshCollider>(); // Dejar caer al jugador (desactivar)
        _platformRender = this.GetComponent<Renderer>(); // Cambiar color de material a invisible
        _canDetect = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canDetect)
        {
            _canDetect = false;
            StartCoroutine("Dissapear");
        }
    }

    IEnumerator Dissapear()
    {
        Color c = _platformRender.materials[1].color;
        for (float x = 1f; x >= 0.1f; x -= 0.1f)//10 veces
        {
            c.a = x;//Cambiar el alfa poco a poco hasta que llegue a 0
            _platformRender.materials[1].color = c;
            yield return new WaitForSeconds(.1f);
        }
        _platformBase.enabled = false; // Desactivar colisionador para que el personaje caiga
        yield return new WaitForSeconds(2f); // Tiempo en que la plataforma es invisible
        for (float x = 0f; x <= 1f; x += 0.1f)//10 veces
        {
            c.a = x;
            _platformRender.materials[1].color = c;
            yield return new WaitForSeconds(.05f); // Espera menos para hacer el cambio
        }
        _platformBase.enabled = true; // Activar el colisionador para que el jugador pueda pisarlo

        _canDetect = true; // Volver a detectar para hacer invisible
    }

}
