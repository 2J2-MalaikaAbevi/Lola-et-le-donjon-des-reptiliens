using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionReptiliensNormal : MonoBehaviour
{
    public float lesVies = 2; //Variable pour la vie des reptiliens ordinaires
    public GameObject joueur; //Variable pour enregistrer Lola

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(lesVies);
        if(lesVies == 0)
        {
            gameObject.SetActive(false);
            Invoke("ApparitionReptilien", 10f);
        }
    }

    //Fonction pour faire réapparaitre le reptilien avec de la vie
    void ApparitionReptilien()
    {
        lesVies = 2;
        gameObject.SetActive(true);
    }
}
