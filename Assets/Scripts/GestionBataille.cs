using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionBataille : MonoBehaviour
{
    //Ce sont les variable statique pour enregistrer le nombre de reptiliens tu�s dans chaque bataille
    public static int reptileBataille1 = 0;
    public static int reptileBataille2 = 0;
    public static int reptileBataille3 = 0;
    public static int reptileBataille4 = 0;
    public static int reptileBataille5 = 0;

    public GameObject barriere1; //Premi�re barri�re
    public GameObject barriere2; //
    public GameObject barriere3; //
    public GameObject barriere4; //
    public GameObject barriere5; //

    public GameObject cleBoss1; //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si le nombre d'ennemi qu'il faut tuer est atteint
        if(reptileBataille1 == 2)
        {
            //La cl� pour d�verouiller la barri�re et qui servira ulterierement � ouvrir la porte de la salle du boss apparait
            cleBoss1.SetActive(true);
        }

        //Si Lola attrape une cl�, la premiere barri�re s'ouvre
        if (ControleLola.compteurCle == 1) barriere1.SetActive(false);
    }
}
