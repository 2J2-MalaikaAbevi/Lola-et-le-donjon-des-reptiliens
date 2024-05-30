using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Fonctionnement et utilité générale du script:
   Gestion des animations
   Gestion des vies des reptiliens 
   Par : Malaïka Abevi
   Dernière modification : 29/05/2024
*/

public class GestionReptiliensNormal : MonoBehaviour
{
    public float lesVies = 2; //Variable pour la vie des reptiliens ordinaires
    public GameObject joueur; //Variable pour enregistrer Lola
    public bool enAttaque = false; //Variable pour enregistrer si le reptilien est en attaque ou non
    public bool degat = false; //Variable pour enregistrer si le reptilien subit du dégat
    public bool estMort = false; //Variable pour enregistrer si le reptilien est mort 


    // Update is called once per frame
    void Update()
    {
        //GESTION DE LA VIE DU REPTILIEN
        //Si la vie est à 0, on gère la mort du reptilien
        if(lesVies <= 0)
        {
            //On fait jouer l'animation de mort
            GetComponent<Animator>().SetBool("mort", true);

            //On empeche une animation d'attaque
            GetComponent<Animator>().SetBool("attaque", false);

            //On empeche une animation de degat
            GetComponent<Animator>().SetBool("degat", false);

            //Desactiver le collider
            GetComponent<CapsuleCollider2D>().enabled = false;

            //On s'assure qu'il arrete d'avancer
            GetComponent<SuiviEnnemiAi>().enabled = false;

            //On rend la variable pour enregistrer la mort à true
            estMort = true;

            //Et on invoque une fonction qui s'occupe de la réapparition après 10 sec
            Invoke("TuerReptilien", 1f);
        }

        //GESTION DE L'ANIMATION D'ATTAQUE DU REPTILIEN
        //Si le reptilien est en attaque
        if(enAttaque)
        {
            //On appelle une fonction qui désactivera ce qui est lié à l'attaque apres 0,5sec
            Invoke("DesactiverAttaque", 1f);
        }

        if(degat)
        {
            Invoke("DesactiverDegat", 1f);
        }

        //Appel de la fonctionn pour gérér les compteurs pour les reptiliens morts
        GestionPointBataille();
    }

    //Fonction pour faire réapparaitre le reptilien avec de la vie
    void TuerReptilien()
    {
        //On désactive le reptilien pour qu'il disparaisse
        gameObject.SetActive(false);
    }

    //Fonction pour désactivation de l'attaque
    void DesactiverAttaque()
    {
        //Rendre l'attaque fausse
        enAttaque = false;
        //Arreter l'animation d'attaque du reptilien
        GetComponent<Animator>().SetBool("attaque", false);
    }

    //Fonction pour la désactivation du dégat
    void DesactiverDegat()
    {
        //Rendre l'attaque fausse
        degat = false;
        //Arreter l'animation d'attaque du reptilien
        GetComponent<Animator>().SetBool("degat", false);
    }

    //Fonction pour la gestion des points dans les batailles
    void GestionPointBataille()
    {
        if (estMort)
        {
            //On remet la vie a 2 et on rend la mort fausse pour éviter l'incrémentation continuelle
            lesVies = 2;
            estMort = false;

            //Puis on incrémenter le compteur pour le nombre de reptiliens spécifiques tués
            if (gameObject.name == "Reptilien1") GestionBataille.reptileBataille1++;
            if (gameObject.name == "Reptilien2") GestionBataille.reptileBataille2++;
            if (gameObject.name == "Reptilien3") GestionBataille.reptileBataille3++;
            if (gameObject.name == "Reptilien4") GestionBataille.reptileBataille4++;
            if (gameObject.name == "Reptilien5") GestionBataille.reptileBataille5++;
            if (gameObject.name == "Reptilien6") GestionBataille.reptileBataille6++;
            if (gameObject.name == "Reptilien7") GestionBataille.reptileBataille7++;
        }
    }
}
