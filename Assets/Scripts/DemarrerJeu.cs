using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Fonctionnement et utilit� g�n�rale du script:
   Gestion du d�marrage de la partie
   Par : Mala�ka Abevi
   Derni�re modification : 15/04/2024
*/

public class DemarrerJeu : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        //Si on clique sur la barre d'espace, on appelle la fonction pour d�marrer le jeu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CommencerJeu();
        }
    }

    //Fonction pour d�marrer le jeu 
    void CommencerJeu()
    {
        //Charger la sc�ne "1", la sc�ne principale de jeu
        SceneManager.LoadScene(1);
    }
}
