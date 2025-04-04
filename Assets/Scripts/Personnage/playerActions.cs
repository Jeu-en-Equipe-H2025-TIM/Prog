using System;
using System.Collections;
using UnityEngine;

public class playerActions : MonoBehaviour
{
    // Valeur pour les upgrades
    private bool upgradeDebloquer = true;

    // Valeur pour la flashlight
    public GameObject flashlight;
    private float tempsAttente;
    private float qteFlickers;

    // Update is called once per frame

    private void Start()
    {
        Invoke("determinerFlashlightVariables", 0f);
    }
    void Update()
    {
        // Flashlight
        if (upgradeDebloquer && Input.GetKeyDown(KeyCode.F))
        {
            Invoke("controleFlashlight", 0f);
        }
        //
    }


    void controleFlashlight()
    {
        // Si on, turn off. Si off, turn on (avec setActive)
        if (flashlight.activeSelf)
        {
            flashlight.SetActive(false);
            StopCoroutine("FlashlightFlicker");
        }
        else
        {

            StartCoroutine("FlashlightFlicker");
            flashlight.SetActive(true);

        }
            
    }

    void determinerFlashlightVariables()
    {
        tempsAttente = UnityEngine.Random.Range(1f, 10f);
        qteFlickers = UnityEngine.Random.Range(1f, 3f);

        Debug.Log("Flashlight: Nouvelles valeurs: "+ tempsAttente + "s, " + qteFlickers + " flickers. ");
    }

    IEnumerator FlashlightFlicker()
    {
        Debug.Log("Flicker lancer!");
        yield return new WaitForSeconds(tempsAttente);
        Invoke("determinerFlashlightVariables", 0f);

        // Active le flicker 
        yield return StartCoroutine("FlashlightFlickerAction", qteFlickers);

        Debug.Log("Flicker en cours, on continue");
        if (flashlight.activeSelf)
        {
            Debug.Log("On relance le flickering");
            yield return FlashlightFlicker();
        }
        else
        {
            Debug.Log("On arrete le flickering");
            yield return default;
        }
    }

    IEnumerator FlashlightFlickerAction(float qteFlickers)
    {
        Debug.Log("Flicker Action lancer!");
        // Flicker X fois
        for (int i = 0; i < qteFlickers; i++)
        {
            Debug.Log("Flicker " + i);

            flashlight.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            flashlight.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Flicker Action fini!");
        yield return default;
    }
}
