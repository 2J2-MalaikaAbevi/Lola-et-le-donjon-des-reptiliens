using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionMaitreReptilien : MonoBehaviour
{
    public float lesVies = 2; //Variable pour la vie des reptiliens ordinaires
    public GameObject joueur; //Variable pour enregistrer Lola
    public bool enAttaque = false; //Variable pour enregistrer si le reptilien est en attaque ou non
    public bool degat = false; //Variable pour enregistrer si le reptilien subit du d�gat
    public bool estMort = false; //Variable pour enregistrer si le reptilien est mort 

    void Start()
    {

    }

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
            Invoke("TuerReptilien", 0.5f);
        }

        //GESTION DE L'ANIMATION D'ATTAQUE DU REPTILIEN
        //Si le reptilien est en attaque
        if (enAttaque)
        {
            //On appelle une fonction qui d�sactivera ce qui est li� � l'attaque apres 0,5sec
            Invoke("DesactiverAttaque", 0.5f);
        }

        if (degat)
        {
            Invoke("DesactiverDegat", 0.5f);
        }
    }

    //Fonction pour faire r�apparaitre le reptilien avec de la vie
    void TuerReptilien()
    {
        //On d�sactive le reptilien pour qu'il disparaisse
        gameObject.SetActive(false);
        //On redonne 2 points de vie au reptilien
        //lesVies = 2;
        //On le rend actif
        //gameObject.SetActive(true);
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
}
