using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Fonctionnement et utilit� g�n�rale du script:
   Gestion des d�placements des reptiliens (� venir)
   Gestion des animations
   Gestion des vies des reptiliens 
   Par : Mala�ka Abevi
   Derni�re modification : 15/04/2024
*/

public class GestionReptiliensNormal : MonoBehaviour
{
    public float lesVies = 2; //Variable pour la vie des reptiliens ordinaires
    public GameObject joueur; //Variable pour enregistrer Lola
    public bool enAttaque = false; //Variable pour enregistrer si le reptilien est en attaque ou non


    // Update is called once per frame
    void Update()
    {
        //GESTION DE LA VIE DU REPTILIEN
        //Si la vie est � 0, on g�re la mort du reptilien
        if(lesVies == 0)
        {
            //On d�sactive le reptilien pour qu'il disparaisse
            gameObject.SetActive(false);
            //Et on invoque une fonction qui s'occupe de la r�apparition apr�s 10 sec
            Invoke("ApparitionReptilien", 10f);
        }

        //GESTION DE L'ANIMATION D'ATTAQUE DU REPTILIEN
        //Si le reptilien est en attaque
        if(enAttaque)
        {
            //On appelle une fonction qui d�sactivera ce qui est li� � l'attaque apres 0,5sec
            Invoke("DesactiverAttaque", 0.5f);
        }
    }

    //Fonction pour faire r�apparaitre le reptilien avec de la vie
    void ApparitionReptilien()
    {
        //On redonne 2 points de vie au reptilien
        lesVies = 2;
        //On le rend actif
        gameObject.SetActive(true);
    }

    void DesactiverAttaque()
    {
        //Rendre l'attaque fausse
        enAttaque = false;
        //Arreter l'animation d'attaque du reptilien
        GetComponent<Animator>().SetBool("attaque", false);
    }
}
