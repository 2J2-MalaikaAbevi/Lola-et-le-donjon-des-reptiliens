using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Fonctionnement et utilité générale du script:
   Gestion des zones de batailles et de l'apparition d'ennemis
   Par : Malaïka Abevi
   Dernière modification : 29/05/2024
*/

public class ActiverCombatLola : MonoBehaviour
{
    //Variables pour les groupes de reptiliens de chaque zone 
    public GameObject reptile1;
    public GameObject reptile2;
    public GameObject reptile3; 
    public GameObject reptile4;
    public GameObject reptile5;
    public GameObject reptile6;
    public GameObject reptile7;
    public GameObject reptileBonus;
    public GameObject maitreReptilien; //Variable pour le Maître Reptile 

    public GameObject musiqueJeu; //Ce gameObject servira a changer la musique d'ambiance lors de l'entrée dans la zone avec le boss
    public GameObject musiqueBoss; //C'est la musique pour la zone avec le boss

    public static bool batailleBoss = false; //Pour savoir si la bataille avec le boss à commencer

    void OnTriggerEnter2D(Collider2D infoCollider)
    {
        //Si Lola entre dans la zone de bataille, les reptiliens apparaissent dans leur zones
        if (infoCollider.gameObject.name == "Zone1") reptile1.SetActive(true);
        if (infoCollider.gameObject.name == "Zone2") reptile2.SetActive(true);
        if (infoCollider.gameObject.name == "Zone3") reptile3.SetActive(true);
        if (infoCollider.gameObject.name == "Zone4") reptile4.SetActive(true);
        if (infoCollider.gameObject.name == "Zone5") reptile5.SetActive(true);
        if (infoCollider.gameObject.name == "Zone6") reptile6.SetActive(true);
        if (infoCollider.gameObject.name == "Zone7") reptile7.SetActive(true);
        if (infoCollider.gameObject.name == "ZoneBonus") reptileBonus.SetActive(true);
        
        if (infoCollider.gameObject.name == "ZoneFinale") {
            maitreReptilien.SetActive(true); //Dans ce cas la, c'est le Maitre reptilien qui apparait quand on entre dans la zone finale
            //On enleve la musique de base et on met la musique de la bataille finale
            musiqueJeu.SetActive(false); 
            musiqueBoss.SetActive(true);
            //On dit que la bataille a bien commencée
            batailleBoss = true;
        } 
    }
}
