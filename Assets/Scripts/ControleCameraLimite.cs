using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Fonctionnement et utilit� g�n�rale du script:
   Gestion des limites verticales et horizontales de la cam�ra
   Par : Mala�ka Abevi
   Derni�re modification : 15/04/2024
*/

public class ControleCameraLimite : MonoBehaviour
{
    /*SCRIPT QUI VA POSSIBLEMENT �TRE SUPPRIM�*/
    public GameObject cible;

    public float limiteHaut;
    public float limiteBas;
    public float limiteGauche;
    public float limiteDroite;


    // Update is called once per frame
    void Update()
    {

        Vector3 positionActuelle = cible.transform.position;


        /*if (positionActuelle.x < limiteGauche) positionActuelle.x = limiteGauche;

        if (positionActuelle.x > limiteDroite) positionActuelle.x = limiteDroite;

        if (positionActuelle.y < limiteBas) positionActuelle.y = limiteBas;

        if (positionActuelle.y > limiteHaut) positionActuelle.y = limiteHaut;

        positionActuelle.z = -10;

        transform.position = positionActuelle;*/

    }
}
