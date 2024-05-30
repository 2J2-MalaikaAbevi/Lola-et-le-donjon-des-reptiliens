using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Fonctionnement et utilit� g�n�rale du script:
   Gestion des animations
   Gestion des vies des reptiliens 
   Par : Mala�ka Abevi
   Derni�re modification : 29/05/2024
*/

public class GestionReptiliensNormal : MonoBehaviour
{
    public float lesVies = 2; //Variable pour la vie des reptiliens ordinaires
    public GameObject joueur; //Variable pour enregistrer Lola
    public bool enAttaque = false; //Variable pour enregistrer si le reptilien est en attaque ou non
    public bool degat = false; //Variable pour enregistrer si le reptilien subit du d�gat
    public bool estMort = false; //Variable pour enregistrer si le reptilien est mort 


    // Update is called once per frame
    void Update()
    {
        //GESTION DE LA VIE DU REPTILIEN
        //Si la vie est � 0, on g�re la mort du reptilien
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

            //On rend la variable pour enregistrer la mort � true
            estMort = true;

            //Et on invoque une fonction qui s'occupe de la r�apparition apr�s 10 sec
            Invoke("TuerReptilien", 1f);
        }

        //GESTION DE L'ANIMATION D'ATTAQUE DU REPTILIEN
        //Si le reptilien est en attaque
        if(enAttaque)
        {
            //On appelle une fonction qui d�sactivera ce qui est li� � l'attaque apres 0,5sec
            Invoke("DesactiverAttaque", 1f);
        }

        if(degat)
        {
            Invoke("DesactiverDegat", 1f);
        }

        //Appel de la fonctionn pour g�r�r les compteurs pour les reptiliens morts
        GestionPointBataille();
    }

    //Fonction pour faire r�apparaitre le reptilien avec de la vie
    void TuerReptilien()
    {
        //On d�sactive le reptilien pour qu'il disparaisse
        gameObject.SetActive(false);
    }

    //Fonction pour d�sactivation de l'attaque
    void DesactiverAttaque()
    {
        //Rendre l'attaque fausse
        enAttaque = false;
        //Arreter l'animation d'attaque du reptilien
        GetComponent<Animator>().SetBool("attaque", false);
    }

    //Fonction pour la d�sactivation du d�gat
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
            //On remet la vie a 2 et on rend la mort fausse pour �viter l'incr�mentation continuelle
            lesVies = 2;
            estMort = false;

            //Puis on incr�menter le compteur pour le nombre de reptiliens sp�cifiques tu�s
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
