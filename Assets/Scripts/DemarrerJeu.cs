using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Fonctionnement et utilité générale du script:
   Gestion du démarrage de la partie
   Par : Malaïka Abevi
   Dernière modification : 15/04/2024
*/

public class DemarrerJeu : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        //Si on clique sur la barre d'espace, on appelle la fonction pour démarrer le jeu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CommencerJeu();
        }
    }

    //Fonction pour démarrer le jeu 
    void CommencerJeu()
    {
        //Charger la scène "1", la scène principale de jeu
        SceneManager.LoadScene(1);
    }
}
