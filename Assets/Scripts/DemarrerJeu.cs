using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Fonctionnement et utilité générale du script:
   Gestion du démarrage de la partie et des changements de scènes
   Par : Malaïka Abevi
   Dernière modification : 29/05/2024
*/

public class DemarrerJeu : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        //Récuperer la scène actuelle dans une variable
        Scene sceneActuelle = SceneManager.GetActiveScene();

        //Enregistrer le nom de cette scène dans une variable string (chaîne de caractères)
        string nomScene = sceneActuelle.name;

        //Si on clique sur la barre d'espace, on appelle la fonction pour démarrer le jeu
        if (Input.GetKeyDown(KeyCode.Space))
        {   if(nomScene == "FinMort" || nomScene == "FinVictoire")
            {
                SceneManager.LoadScene("Introduction");
            }
            else if(nomScene != "Principale") //Pour ne pas reload la scène pendant la partie
            {
                SceneManager.LoadScene("Principale");
            }

        }

        //Si on clique sur O, on passe à la scène suivante
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

        //Si on clique sur P, on retourne à la scène précédente
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
