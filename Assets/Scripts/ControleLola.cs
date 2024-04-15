using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ControleLola : MonoBehaviour
{
    public float vitesseMax; //vitesse horizontale actuelle

    float vitesseX; //Position de Lola � l'horizontale
    float vitesseY; //Position de Lola � la verticale

    int lesVies = 6; //Le nombre de vie de Lola, on commence avec 3 vies

    public GameObject coeur1; //Variable pour le premier coeur de vie de Lola
    public GameObject coeur2; //Variable pour le deuxi�me coeur de vie de Lola
    public GameObject coeur3; //Variable pour le troisi�me coeur de vie de Lola
    public GameObject laPorteBoss; //Variable pour la porte vers la finale avec le boss

    public Sprite coeurPlein; //Variable pour l'image du coeur plein
    public Sprite coeurMoitie; //Variable pour l'image du coeur � moiti� plein
    public Sprite coeurFini; //Variable pour l'image du coeur vide/fini
    public Sprite porteOuverte; //Variable pour l'image de la porte vers le boss finale, ouverte

    public TextMeshProUGUI affichageCompteurCle; //Variable pour le texte pour le nombre de cl�s pour la porte de boss amass�es

    bool partieTerminee;
    public bool attaque;

    bool enAttaque = false; //Variable pour savoir si Lola est en attaque ou non
    bool enAttaqueArme = false; //Variable pour savoir si Lola est en attaque avec son arme ou non
    bool vitesseAugmentee = false; //Variable pour savoir si Lola � sa vitesse augment�e ou non

    int compteurCle = 0; //Variable pour enregistrer le nombre de cl�s amass�es


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
                vitesseX = -vitesseMax;
                GetComponent<SpriteRenderer>().flipX = true;
                //GetComponent<Animator>().SetBool("marche", true);
            }
            //d�placement vers la droite
            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))   
            {
                vitesseX = vitesseMax;
                GetComponent<SpriteRenderer>().flipX = false;
                //GetComponent<Animator>().SetBool("marche", true);
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
               // GetComponent<Animator>().SetBool("marche", false);
            }
            
            //sauter l'objet � l'aide la touche "w"
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                vitesseY = vitesseMax;
                //GetComponent<Animator>().SetBool("marche", true);
            }
            else if(Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                vitesseY = -vitesseMax;
                //GetComponent<Animator>().SetBool("marche", true);
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;
                //GetComponent<Animator>().SetBool("marche", false);
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);

            //Gestion de la touche pour l'attaque avec la barre d'espace et le mode attaque OU attaque arm�e � "true"
            if (Input.GetKeyDown(KeyCode.Space) && attaque == false)
            {
                enAttaque = true;
                Invoke("AnnulerAttaque", 0.5f);
                GetComponent<Animator>().SetBool("attaque", true);
                print("coucou");
            }

            if(vitesseX > 0.9 || -vitesseX < 0.9)
            {

            }
        }


        /*Gestion des images des coeurs selon le nombre de vies*
         * Gestion de la mort de Lola
         */
        //�tablir la contrainte pour le nombre de vie, il ne faut pas que ce soit au dessus de 6 points;
        else if(lesVies > 6)
        {
            lesVies = 6;
        }

        if (lesVies <= 0)
        {
            //Gestion des images de coeurs
            coeur1.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur2.GetComponent<SpriteRenderer>().sprite = coeurFini;
            coeur3.GetComponent<SpriteRenderer>().sprite = coeurFini;

            //Gestion de l'animation de mort de Lola
            GetComponent<Animator>().SetBool("mort", true);

            //On enregistre que la partie est termin�e
            partieTerminee = true;
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


        //GESTION DE L'OUVERTURE DE LA PORTE DU BOSS ET DU CHANGEMENT DE SC�NE

        if(compteurCle == 5)
        {
        //On change l'image de la porte ferm�e pour une image de porte ouverte
        laPorteBoss.GetComponent<SpriteRenderer>().sprite = porteOuverte;   
            
        print(compteurCle); 
        }

        //On permet l'ouverture de la porte pour atteindre le boss
    }

    /******************************GESTION DES ATTAQUES, DES POTIONS ET DE LA VIE DE LOLA AVEC COLLISIONS******************************/
    
    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        /*Gestion des attaques donn�es et re�ues*/
        if(infoCollision.gameObject.tag == "reptileNormal" && !enAttaque && !enAttaqueArme)
        {
            lesVies -= 1;
            //print(lesVies);
            infoCollision.gameObject.GetComponent<Animator>().SetBool("attaque", true);
            //Indiquer que le reptilien est en attaque (qui aura une incidence dans le script de la gestion des reptiliens
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().enAttaque = true;
        }
        
        //Si Lola est en attaque lors de la collision avec un reptilien, elle fera perd 1 vie au reptilien touch�, en accedeant au script qui g�re les reptiliens et en changeant la variable de vie 
        if(infoCollision.gameObject.tag == "reptileNormal" && enAttaque)
        {
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies -= 1;
            
            //Animation de d�gat du reptiliens
            infoCollision.gameObject.GetComponent<Animator>().SetBool("degat", true);
        }

        //Si Lola est en attaque et arm�e lors de la collision avec un reptilien, elle fera perd 2 points de vie au reptilien touch�, en accedeant au script qui g�re les reptiliens et en changeant la variable de vie 
        if (infoCollision.gameObject.tag == "reptileNormal" && enAttaqueArme)
        {
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies -= 2;
        }

        //Gestion de la potion de vie
        if(infoCollision.gameObject.name == "PotionVie")
        {
            //Lola ne prendra de la vie et la potion disparaitra que si elle a moins de 6 point de vie
            if(lesVies < 6)
            {
            //On redonne un point de vie � Lola
            lesVies += 1;
            //On d�sactive la potion de vie 
            infoCollision.gameObject.SetActive(false);
            }
        }

        //Gestion de la potion de vitesse
        if(infoCollision.gameObject.name == "PotionVitesse")
        {   //Lola ne prendra de la vitesse et la potion disparaitra que si sa vitesse n'est initialement pas augment�e
            if (!vitesseAugmentee)
            {
                //On augmente la vitesse de Lola
                vitesseMax *= 1.5f;
                //Enregistrer que la vitesse de Lola a �t� acc�l�r�e
                vitesseAugmentee = true;
                //Puis on invoque une fonction qui va annuler les effets de la potion apr�s 8sec
                Invoke("AnnulerPotionVitesse", 8f);
            }
        }

        /*GESTION DES CL�S pour la FINALE AVEC BOSS*/
        if(infoCollision.gameObject.name == "Cle")
        {
            //On d�truit les cl�s r�colt�es
            Destroy(infoCollision.gameObject);

            //On additionne 1 points � chaque cl�s amass�es
            compteurCle += 1;

            //On affiche la valeur du compteur dans l'interface du jeu
            affichageCompteurCle.text = compteurCle.ToString();

        }
    }

    //Fonction pour terminer l'attaque
   void AnnulerAttaque()
    {
        //Rendre l'attaque fausse
        enAttaque = false;

        GetComponent<Animator>().SetBool("attaque", false);
    }

    //Fontion pour redonner une vitesse normale � Lola
    void AnnulerPotionVitesse()
    {
        vitesseAugmentee = false;
        vitesseMax /= 1.5f;
    }

    void ApparitionPotionVitesse()
    {

    }
}
