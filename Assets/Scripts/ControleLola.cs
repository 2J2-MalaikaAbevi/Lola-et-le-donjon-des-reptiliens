using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControleLola : MonoBehaviour
{
    public float vitesse;      //vitesse horizontale actuelle

    float positionX; //Position de Lola � l'horizontale
    float positionY; //Position de Lola � la verticale

    float lesVies = 3; //Le nombre de vie de Lola, on commence avec 3 vies

    bool partieTerminee;
    public bool attaque;

    bool enAttaque = false; //Variable pour savoir si Lola est en attaque ou non
    bool enAttaqueArme = false; //Variable pour savoir si Lola est en attaque avec son arme

    int compteurCle = 0; //Variable pour enregistrer le nombre de cl�s amass�es

    //public TagHandle leReptilienNormal; //Variable pour le tag qui identifie les reptiliens


    /* D�tection des touches et modification de la vitesse de d�placement;
       "a" et "d" pour avancer et reculer, "w" pour sauter
    */
    void Update()
    {

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
            // sauter l'objet � l'aide la touche "w"
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


            if (Input.GetKeyDown(KeyCode.Space) && attaque == false)
            {

                attaque = true;
                Invoke("AnnulerAttaque", 0.4f);
                GetComponent<Animator>().SetTrigger("attaqueAnim");
                GetComponent<Animator>().SetBool("saut", false);
            }

            /*if (attaque == true && vitesseX <= vitesseXMax && vitesseX >= -vitesseXMax)
            {

                vitesseX *= 4;

            }*/

            //**************************Gestion des animaitons de course et de repos********************************
            //Active l'animation de course si la vitesse de d�placement n'est pas 0, sinon le repos sera jouer par Animator



            /*if (vitesseX > 0.1f || vitesseX < -0.1f)
            {

                GetComponent<Animator>().SetBool("course", true);
            }
            else
            {

                GetComponent<Animator>().SetBool("course", false);
            }*/


        }


    }

    /******************************GESTION DES ATTAQUES, DES POTIONS ET DE LA VIE DE LOLA******************************/
    
    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        /*Gestion des attaques donn�es et re�ues*/
        if(infoCollision.gameObject.tag == "reptileNormal" && !enAttaque && !enAttaqueArme)
        {
            lesVies -= 0.5f;
            print(lesVies);
            infoCollision.gameObject.GetComponent<Animator>().SetBool("attaque", true);

        }
        
        //Si Lola est en attaque lors de la collision avec un reptiliens, elle fera perd 1 vie au reptilien touch�, en accedeant au script qui g�re les reptiliens et en changeant la variable de vie 
        if(infoCollision.gameObject.tag == "reptileNormal" && enAttaque)
        {
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies -= 1;
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
            print(lesVies);
        }

        /*GESTION DES CL�S FINALE AVEC BOSS*/
        if( infoCollision.gameObject.name == "Cle")
        {
            Destroy(infoCollision.gameObject);

            compteurCle += 1;
            print(compteurCle);
        }
    }

    /*********************************************R**************************************/
    
}
