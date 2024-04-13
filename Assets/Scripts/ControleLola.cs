using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControleLola : MonoBehaviour
{
    public float vitesse;      //vitesse horizontale actuelle

    float positionX; //Position de Lola � l'horizontale
    float positionY; //Position de Lola � la verticale

    float lesVies = 6; //Le nombre de vie de Lola, on commence avec 3 vies

    public GameObject coeur1; //Variable pour le premier coeur de vie de Lola
    public GameObject coeur2; //Variable pour le deuxi�me coeur de vie de Lola
    public GameObject coeur3; //Variable pour le troisi�me coeur de vie de Lola

    public Sprite coeurPlein; //Variable pour le premier coeur de vie de Lola
    public Sprite coeurMoitie; //Variable pour le deuxi�me coeur de vie de Lola
    public Sprite coeurFini; //Variable pour le troisi�me coeur de vie de Lola

    bool partieTerminee;
    public bool attaque;

    bool enAttaque = false; //Variable pour savoir si Lola est en attaque ou non
    bool enAttaqueArme = false; //Variable pour savoir si Lola est en attaque avec son arme

    int compteurCle = 0; //Variable pour enregistrer le nombre de cl�s amass�es

    //public TagHandle leReptilienNormal; //Variable pour le tag qui identifie les reptiliens


    /* D�tection des touches et modification de la vitesse de d�placement;
       "a" et "d" pour avancer 
    */
    void Update()
    {
        /*Gestion des touches pour controler Lola
         * "a" ou fleche gauche pour aller � gauche
         * "d" ou fleche droite pour aller � droite
         * "w" ou fleche haut pour aller en haut
         * "s" ou fleche bas pour aller en bas
         */
        if (!partieTerminee)
        {
            // d�placement vers la gauche
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                positionX -= vitesse;
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<Animator>().SetBool("marche", true);
            }
            //d�placement vers la droite
            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))   
            {
                positionX += vitesse;
                GetComponent<SpriteRenderer>().flipX = false;
                GetComponent<Animator>().SetBool("marche", true);
            }
            //sauter l'objet � l'aide la touche "w"
            else if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                positionY += vitesse;
                GetComponent<Animator>().SetBool("marche", true);
            }
            else if(Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                positionY -= vitesse;
                GetComponent<Animator>().SetBool("marche", true);
            }
            else
            {
                positionY = transform.position.y;
                positionX = transform.position.x;
                GetComponent<Animator>().SetBool("marche", false);
            }

            transform.position = new Vector2 (positionX, positionY);

            //Gestion de la touche pour l'attaque avec la barre d'espace et le mode attaque � "true"
            if (Input.GetKeyDown(KeyCode.Space) && attaque == false)
            {
                enAttaque = true;
                Invoke("AnnulerAttaque", 0.5f);
                GetComponent<Animator>().SetBool("attaque", true);
                print("coucou");
            }
        }

        //Gestion des images des coeurs selon le nombre de vies******************************
        if (lesVies == 0)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies == 1)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies == 2)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies == 3)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies == 4)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;
        }

        if (lesVies == 5)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurMoitie;
        }

        if (lesVies == 6)
        {
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurPlein;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurPlein;
        }
    }

    /******************************GESTION DES ATTAQUES, DES POTIONS ET DE LA VIE DE LOLA AVEC COLLISIONS******************************/
    
    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        /*Gestion des attaques donn�es et re�ues*/
        if(infoCollision.gameObject.tag == "reptileNormal" && !enAttaque && !enAttaqueArme)
        {
            lesVies -= 0.5f;
            //print(lesVies);
            infoCollision.gameObject.GetComponent<Animator>().SetBool("attaque", true);

        }
        
        //Si Lola est en attaque lors de la collision avec un reptiliens, elle fera perd 1 vie au reptilien touch�, en accedeant au script qui g�re les reptiliens et en changeant la variable de vie 
        if(infoCollision.gameObject.tag == "reptileNormal" && enAttaque)
        {
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies -= 1;
            //Animation de d�gat du reptiliens
            infoCollision.gameObject.GetComponent<Animator>().SetBool("degat", true);
        }

        //Si Lola est en attaque et arm�e lors de la collision avec un reptiliens, elle fera perd 1 vie au reptilien touch�
        if (infoCollision.gameObject.tag == "reptileNormal" && enAttaqueArme)
        {
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies -= 2;
        }

        /*Gestion de la potion de vie*/
        if(infoCollision.gameObject.name == "PotionVie")
        {
            lesVies += 1f;
            //print(lesVies);
        }

        if(infoCollision.gameObject.name == "PotionVitesse")
        {
            vitesse *= 1.5f;
            Invoke("AnnulerPotionVitesse", 8f);
        }

        /*GESTION DES CL�S FINALE AVEC BOSS*/
        if(infoCollision.gameObject.name == "Cle")
        {
            Destroy(infoCollision.gameObject);

            compteurCle += 1;
            print(compteurCle);
        }
    }

    //Fonction pour terminer l'attaque
   void AnnulerAttaque()
    {
        enAttaque = false;
        GetComponent<Animator>().SetBool("attaque", false);
    }

    //Fontion pour redonner une vitesse normale � Lola
    void AnnulerPotionVitesse()
    {
        vitesse /= 1.5f;
    }
}
