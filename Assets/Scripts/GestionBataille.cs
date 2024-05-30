using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* Fonctionnement et utilit� g�n�rale du script:
   Gestion de l'apparition des cl�s
   Gestion de l'ouverture des barrieres
   Par : Mala�ka Abevi
   Derni�re modification : 29/05/2024
*/

public class GestionBataille : MonoBehaviour
{
    //Ce sont les variable statiques pour enregistrer le nombre de reptiliens tu�s dans chaque bataille
    public static int reptileBataille1 = 0;
    public static int reptileBataille2 = 0;
    public static int reptileBataille3 = 0;
    public static int reptileBataille4 = 0;
    public static int reptileBataille5 = 0;
    public static int reptileBataille6 = 0;
    public static int reptileBataille7 = 0;

    //Les gameObject des barri�res + la barri�re de la zone bonus
    public GameObject barriere1;
    public GameObject barriere2; 
    public GameObject barriere3; 
    public GameObject barriere4; 
    public GameObject barriere5; 
    public GameObject barriere6; 
    public GameObject barriereBonus;

    //Les gameObject des cl�s des barri�res et du boss + la cl� bonus
    public GameObject cleBoss1; 
    public GameObject cleBoss2; 
    public GameObject cleBoss3; 
    public GameObject cleBoss4; 
    public GameObject cleBoss5; 
    public GameObject cleBoss6; 
    public GameObject cleBoss7;
    public GameObject cleBonus; 


    // Update is called once per frame
    void Update()
    {   
        //La cl� pour d�verouiller la barri�re et qui servira ulterierement � ouvrir la porte de la salle du boss apparait
        //Si le nombre d'ennemi qu'il faut tuer est atteint
        if (reptileBataille1 == 2) cleBoss1.SetActive(true);
        if (reptileBataille2 == 3) cleBoss2.SetActive(true);
        if (reptileBataille3 == 4) cleBoss3.SetActive(true);
        if (reptileBataille4 == 5) cleBoss4.SetActive(true);
        if (reptileBataille5 == 5) cleBoss5.SetActive(true);
        if (reptileBataille6 == 6) cleBoss6.SetActive(true);
        if (reptileBataille7 == 6) cleBoss7.SetActive(true);


        //Si Lola attrape une cl�, la premiere barri�re s'ouvre
        if (ControleLola.compteurCle == 1) barriere1.SetActive(false);
        if (ControleLola.compteurCle == 2) barriere2.SetActive(false);
        if (ControleLola.compteurCle == 3) barriere3.SetActive(false);
        if (ControleLola.compteurCle == 4) barriere4.SetActive(false);
        if (ControleLola.compteurCle == 5) barriere5.SetActive(false);
        if (ControleLola.compteurCle == 6) barriere6.SetActive(false);
        if (ControleLola.compteurCle == 7) barriere6.SetActive(false);

        //On ouvre la barriere de la salle bonus si la cl� bonus est attrap�e
        if (ControleLola.compteurCleBonus == 1) barriereBonus.SetActive(false);
    }
}
