using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Fonctionnement et utilité générale du script:
   Gestion du suivi de la boussole vers les clés à attrapper
   Par : Malaïka Abevi
   Dernière modification : 29/05/2024
*/

public class SuitBalle : MonoBehaviour
{

    public GameObject laCleCible;
    public GameObject laCle1;
    public GameObject laCle2;
    public GameObject laCle3;
    public GameObject laCle4;
    public GameObject laCle5;
    public GameObject laCle6;
    public GameObject laCle7;

    // Update is called once per frame
    void Update()
    {
        if (ControleLola.compteurCle == 0) laCleCible = laCle1;
        if (ControleLola.compteurCle == 1) laCleCible = laCle2;
        if (ControleLola.compteurCle == 2) laCleCible = laCle3;
        if (ControleLola.compteurCle == 3) laCleCible = laCle4;
        if (ControleLola.compteurCle == 4) laCleCible = laCle5;
        if (ControleLola.compteurCle == 5) laCleCible = laCle6;
        if (ControleLola.compteurCle == 6) laCleCible = laCle7;
        if (ControleLola.compteurCle == 7) laCleCible = null;

        Vector2 direction = laCleCible.transform.position - transform.position;
        transform.right = direction;
    }
}