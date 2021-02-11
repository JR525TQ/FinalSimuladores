using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text TextAltaresG;
    public Text TextAltaresR;
    public GameObject StartText;
    public GameObject win;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActualizarCantidadDePilares();
        Destroy(StartText, 10.0f);
    }

    private void ActualizarCantidadDePilares()
    {
        int cantidadDePilaresG = 0;
        int cantidadDePilaresR = 0;

        CapturePointManager[] pilares = FindObjectsOfType<CapturePointManager>();

        foreach (var pilar in pilares)
        {
            if(pilar.redCaptureStatus)
            {
                cantidadDePilaresR++;
            }

            if (pilar.blueCaptureStatus)
            {
                cantidadDePilaresG++;
            }
        }

        TextAltaresG.text = cantidadDePilaresG.ToString();
        TextAltaresR.text = cantidadDePilaresR.ToString();

        if(pilares.Length == cantidadDePilaresG)
        {
            win.SetActive(true);
            GestorDeAudio.instancia.PausarSonido("BackMusic");
            GestorDeAudio.instancia.ReproducirSonido("WinMusic");
        }

    }
}
