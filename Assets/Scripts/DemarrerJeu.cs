using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Fonctionnement et utilit� g�n�rale du script:
   Gestion du d�marrage de la partie et des changements de sc�nes
   Par : Mala�ka Abevi
   Derni�re modification : 29/05/2024
*/

public class DemarrerJeu : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        //R�cuperer la sc�ne actuelle dans une variable
        Scene sceneActuelle = SceneManager.GetActiveScene();

        //Enregistrer le nom de cette sc�ne dans une variable string (cha�ne de caract�res)
        string nomScene = sceneActuelle.name;

        //Si on clique sur la barre d'espace, on appelle la fonction pour d�marrer le jeu
        if (Input.GetKeyDown(KeyCode.Space))
        {   if(nomScene == "FinMort" || nomScene == "FinVictoire")
            {
                SceneManager.LoadScene("Introduction");
            }
            else if(nomScene != "Principale") //Pour ne pas reload la sc�ne pendant la partie
            {
                SceneManager.LoadScene("Principale");
            }

        }

        //Si on clique sur O, on passe � la sc�ne suivante
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (nomScene == "Introduction")
            {
                SceneManager.LoadScene("Contexte");
            }

            if (nomScene == "Contexte")
            {
                SceneManager.LoadScene("Instructions");
            }

            if (nomScene == "Instructions")
            {
                SceneManager.LoadScene("ItemsJeu");
            }

            if (nomScene == "ItemsJeu")
            {
                SceneManager.LoadScene("DescriEnnemiVie");
            }
        }

        //Si on clique sur P, on retourne � la sc�ne pr�c�dente
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (nomScene == "DescriEnnemiVie")
            {
                SceneManager.LoadScene("ItemsJeu");
            }

            if (nomScene == "ItemsJeu")
            {
                SceneManager.LoadScene("Instructions");
            }

            if (nomScene == "Instructions")
            {
                SceneManager.LoadScene("Contexte");
            }

            if (nomScene == "Contexte")
            {
                SceneManager.LoadScene("Introduction");
            }
        }
    }

}
