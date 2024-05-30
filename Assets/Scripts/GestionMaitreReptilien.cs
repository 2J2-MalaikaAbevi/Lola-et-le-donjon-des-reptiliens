using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* Fonctionnement et utilit� g�n�rale du script:
   Gestion des animations
   Gestion des vies du Maitre Reptilien
   Par : Mala�ka Abevi
   Derni�re modification : 29/05/2024
*/

public class GestionMaitreReptilien : MonoBehaviour
{
    public float lesVies = 36; //Variable pour la vie des reptiliens ordinaires
    public bool enAttaque = false; //Variable pour enregistrer si le reptilien est en attaque ou non
    public bool degat = false; //Variable pour enregistrer si le reptilien subit du d�gat
    public bool estMort = false; //Variable pour enregistrer si le reptilien est mort 

    //GameObject pour la vie du Maitre reptilien lors du combat finale (UI)
    public GameObject coeur1;
    public GameObject coeur2; 
    public GameObject coeur3; 
    public GameObject coeur4; 
    public GameObject coeur5; 
    public GameObject coeur6;
    public GameObject LesCoeursMaitreReptilien; //Pour g�rer la visibilit� des vies du Maitre Reptilien

    public TextMeshProUGUI zuckerborgUI; //Pour g�rer du texte pour le nom du boss associ� � sa vie

    public GameObject boussole; //Pour rendre invisible la boussole, on l'enregistre pour la manipuler plus tard

    //Les sprites pour les coeurs et le stade de vie
    public Sprite coeurPlein; //Variable pour l'image du coeur plein
    public Sprite coeurMoitie; //Variable pour l'image du coeur � moiti� plein
    public Sprite coeurFini; //Variable pour l'image du coeur vide/fini

    // Update is called once per frame
    void Update()
    {
        //GESTION DE LA VIE DU REPTILIEN
        //Si la vie est � 0, on g�re la mort du reptilien
        if (lesVies == 0)
        {

            //On fait jouer l'animation de mort
            GetComponent<Animator>().SetBool("mort", true);

            //On rend la variable pour enregistrer la mort � true
            estMort = true;

            //Et on invoque une fonction qui s'occupe de la r�apparition apr�s 10 sec
            Invoke("TuerReptilien", 1f);
        }

        //GESTION DE L'ANIMATION D'ATTAQUE DU REPTILIEN
        //Si le reptilien est en attaque
        if (enAttaque)
        {
            //On appelle une fonction qui d�sactivera ce qui est li� � l'attaque apres 1sec
            Invoke("DesactiverAttaque", 1f);
        }

        //Si le reptilien subit du d�gat
        if (degat)
        {   //Appel de la fonction pour d�sactiver ce qui est li� au d�gat
            Invoke("DesactiverDegat", 1f);
        }

        //GERER LE UI POUR LA VIE DU MAITRE REPTILIEN------------------------------------------
        //Rendre visible le UI pour les vies du boss
        if (ActiverCombatLola.batailleBoss)
        {
            LesCoeursMaitreReptilien.SetActive(true);
            zuckerborgUI.GetComponent<TextMeshProUGUI>().enabled = true;
            boussole.SetActive(false);
        }

        if (lesVies <= 0)
        {
            //Gestion des images de coeurs
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 3 && lesVies > 0)
        {
            //Gestion des images de coeurs
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 6 && lesVies > 3)
        {
            //Gestion des images de coeurs
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 9 && lesVies > 6)
        {
            //Gestion des images de coeurs
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 12 && lesVies > 9)
        {
            //Gestion des images de coeurs
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 15 && lesVies > 12)
        {
            //Gestion des images de coeurs
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 18 && lesVies > 15)
        {
            //Gestion des images de coeurs
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 21 && lesVies > 18)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 24 && lesVies > 21)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 27 && lesVies > 24)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 30 && lesVies > 27)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies <= 33 && lesVies > 30)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
        }

        if (lesVies <= 36 && lesVies > 33)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur4.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur5.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur6.GetComponent<SpriteRenderer>().sprite = coeurPlein;
        }
    }

    //Fonction pour faire r�apparaitre le reptilien avec de la vie
    void TuerReptilien()
    {
        //On d�sactive le Maitre reptilien pour qu'il disparaisse
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
}
