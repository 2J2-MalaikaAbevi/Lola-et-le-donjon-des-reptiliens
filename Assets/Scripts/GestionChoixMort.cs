using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionChoixMort : MonoBehaviour
{
    public GameObject portailAbandon;
    public GameObject portailCombat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D infoCollision)
    {
        if (infoCollision.gameObject.name == "PortailAbandon")
        {
            print("Salut");
            Invoke("GererSceneMort", 5f);
        }

        if (infoCollision.gameObject.name == "PortailCombat")
        {
            print("Coucou");
            Invoke("GererSceneGoulag", 5f);
        }
    }


    void GererSceneMort()
    {
        SceneManager.LoadScene("FinMort");   
    }

    void GererSceneGoulag()
    {
        SceneManager.LoadScene("Goulag");
    }
}

