using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionReptiliensNormal : MonoBehaviour
{
    public float lesVies = 2; //Variable pour la vie des reptiliens ordinaires
    public GameObject joueur; //Variable pour enregistrer Lola
    public bool enAttaque = false; //Variable pour enregistrer si le reptilien est en attaque ou non


    // Update is called once per frame
    void Update()
    {
        //GESTION DE LA VIE DU REPTILIEN
        if(lesVies == 0)
        {
            gameObject.SetActive(false);
            Invoke("ApparitionReptilien", 10f);
        }

        //GESTION DE L'ANIMATION D'ATTAQUE DU REPTILIEN
        if(enAttaque)
        {
            Invoke("DesactiverAttaque", 0.5f);
        }
    }

    //Fonction pour faire réapparaitre le reptilien avec de la vie
    void ApparitionReptilien()
    {
        lesVies = 2;
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
