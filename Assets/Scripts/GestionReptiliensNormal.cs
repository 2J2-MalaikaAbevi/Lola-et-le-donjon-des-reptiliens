using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Fonctionnement et utilité générale du script:
   Gestion des déplacements des reptiliens (à venir)
   Gestion des animations
   Gestion des vies des reptiliens 
   Par : Malaïka Abevi
   Dernière modification : 15/04/2024
*/

public class GestionReptiliensNormal : MonoBehaviour
{
    public float lesVies = 2; //Variable pour la vie des reptiliens ordinaires
    public GameObject joueur; //Variable pour enregistrer Lola
    public bool enAttaque = false; //Variable pour enregistrer si le reptilien est en attaque ou non
    public bool degat = false; //Variable pour enregistrer si le reptilien subit du dégat
    public bool estMort = false; //Variable pour enregistrer si le reptilien est mort 

    void Start()
    {
        //On vérifie le tag du reptilien et on incrémente la variable des reptiliens appropriés
        if(!estMort)
        {   
            print("coucou");
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(estMort);
        //GESTION DE LA VIE DU REPTILIEN
        //Si la vie est à 0, on gère la mort du reptilien
        if(lesVies == 0)
        {
            //On désactive le reptilien pour qu'il disparaisse
            //gameObject.SetActive(false);

            //On fait jouer l'animation de mort
            GetComponent<Animator>().SetBool("mort", true);

            //On rend la variable pour enregistrer la mort à true
            estMort = true;

            //Et on invoque une fonction qui s'occupe de la réapparition après 10 sec
            Invoke("ApparitionReptilien", 10f);
        }

        //GESTION DE L'ANIMATION D'ATTAQUE DU REPTILIEN
        //Si le reptilien est en attaque
        if(enAttaque)
        {
            //On appelle une fonction qui désactivera ce qui est lié à l'attaque apres 0,5sec
            Invoke("DesactiverAttaque", 0.5f);
        }

        if(degat)
        {
            Invoke("DesactiverDegat", 0.5f);
        }

        GestionPointBataille();
    }

    //Fonction pour faire réapparaitre le reptilien avec de la vie
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

    void DesactiverDegat()
    {
        //Rendre l'attaque fausse
        degat = false;
        //Arreter l'animation d'attaque du reptilien
        GetComponent<Animator>().SetBool("degat", false);
    }

    void GestionPointBataille()
    {
        if (estMort)
        {
            lesVies = 2;
            estMort = false;
            if (gameObject.name == "Reptilien1") GestionBataille.reptileBataille1++;
            if (gameObject.name == "Reptilien2") GestionBataille.reptileBataille2++;
            if (gameObject.name == "Reptilien3") GestionBataille.reptileBataille3++;
            if (gameObject.name == "Reptilien4") GestionBataille.reptileBataille4++;
            if (gameObject.name == "Reptilien5") GestionBataille.reptileBataille5++;
        }
    }
}
