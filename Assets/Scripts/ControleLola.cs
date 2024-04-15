using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ControleLola : MonoBehaviour
{
    public float vitesseMax; //vitesse horizontale actuelle

    float vitesseX; //Position de Lola à l'horizontale
    float vitesseY; //Position de Lola à la verticale

    int lesVies = 6; //Le nombre de vie de Lola, on commence avec 3 vies

    public GameObject coeur1; //Variable pour le premier coeur de vie de Lola
    public GameObject coeur2; //Variable pour le deuxième coeur de vie de Lola
    public GameObject coeur3; //Variable pour le troisième coeur de vie de Lola
    public GameObject laPorteBoss; //Variable pour la porte vers la finale avec le boss

    public Sprite coeurPlein; //Variable pour l'image du coeur plein
    public Sprite coeurMoitie; //Variable pour l'image du coeur à moitié plein
    public Sprite coeurFini; //Variable pour l'image du coeur vide/fini
    public Sprite porteOuverte; //Variable pour l'image de la porte vers le boss finale, ouverte

    public TextMeshProUGUI affichageCompteurCle; //Variable pour le texte pour le nombre de clés pour la porte de boss amassées

    bool partieTerminee;
    public bool attaque;

    bool enAttaque = false; //Variable pour savoir si Lola est en attaque ou non
    bool enAttaqueArme = false; //Variable pour savoir si Lola est en attaque avec son arme ou non
    bool vitesseAugmentee = false; //Variable pour savoir si Lola à sa vitesse augmentée ou non

    int compteurCle = 0; //Variable pour enregistrer le nombre de clés amassées


    /* Détection des touches et modification de la vitesse de déplacement;
       "a" et "d" pour avancer 
    */
    void Update()
    {
        /*Gestion des touches pour controler Lola
         * "a" ou fleche gauche pour aller à gauche
         * "d" ou fleche droite pour aller à droite
         * "w" ou fleche haut pour aller en haut
         * "s" ou fleche bas pour aller en bas
         */
        if (!partieTerminee)
        {
            // déplacement vers la gauche
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                vitesseX = -vitesseMax;
                GetComponent<SpriteRenderer>().flipX = true;
                //GetComponent<Animator>().SetBool("marche", true);
            }
            //déplacement vers la droite
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
            
            //sauter l'objet à l'aide la touche "w"
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

            //Gestion de la touche pour l'attaque avec la barre d'espace et le mode attaque OU attaque armée à "true"
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
        //Établir la contrainte pour le nombre de vie, il ne faut pas que ce soit au dessus de 6 points;
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

            //On enregistre que la partie est terminée
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


        //GESTION DE L'OUVERTURE DE LA PORTE DU BOSS ET DU CHANGEMENT DE SCÈNE

        if(compteurCle == 5)
        {
        //On change l'image de la porte fermée pour une image de porte ouverte
        laPorteBoss.GetComponent<SpriteRenderer>().sprite = porteOuverte;   
            
        print(compteurCle); 
        }

        //On permet l'ouverture de la porte pour atteindre le boss
    }

    /******************************GESTION DES ATTAQUES, DES POTIONS ET DE LA VIE DE LOLA AVEC COLLISIONS******************************/
    
    private void OnCollisionEnter2D(Collision2D infoCollision)
    {
        /*Gestion des attaques données et reçues*/
        if(infoCollision.gameObject.tag == "reptileNormal" && !enAttaque && !enAttaqueArme)
        {
            lesVies -= 1;
            //print(lesVies);
            infoCollision.gameObject.GetComponent<Animator>().SetBool("attaque", true);
            //Indiquer que le reptilien est en attaque (qui aura une incidence dans le script de la gestion des reptiliens
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().enAttaque = true;
        }
        
        //Si Lola est en attaque lors de la collision avec un reptilien, elle fera perd 1 vie au reptilien touché, en accedeant au script qui gère les reptiliens et en changeant la variable de vie 
        if(infoCollision.gameObject.tag == "reptileNormal" && enAttaque)
        {
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies -= 1;
            
            //Animation de dégat du reptiliens
            infoCollision.gameObject.GetComponent<Animator>().SetBool("degat", true);
        }

        //Si Lola est en attaque et armée lors de la collision avec un reptilien, elle fera perd 2 points de vie au reptilien touché, en accedeant au script qui gère les reptiliens et en changeant la variable de vie 
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
            //On redonne un point de vie à Lola
            lesVies += 1;
            //On désactive la potion de vie 
            infoCollision.gameObject.SetActive(false);
            }
        }

        //Gestion de la potion de vitesse
        if(infoCollision.gameObject.name == "PotionVitesse")
        {   //Lola ne prendra de la vitesse et la potion disparaitra que si sa vitesse n'est initialement pas augmentée
            if (!vitesseAugmentee)
            {
                //On augmente la vitesse de Lola
                vitesseMax *= 1.5f;
                //Enregistrer que la vitesse de Lola a été accélérée
                vitesseAugmentee = true;
                //Puis on invoque une fonction qui va annuler les effets de la potion après 8sec
                Invoke("AnnulerPotionVitesse", 8f);
            }
        }

        /*GESTION DES CLÉS pour la FINALE AVEC BOSS*/
        if(infoCollision.gameObject.name == "Cle")
        {
            //On détruit les clés récoltées
            Destroy(infoCollision.gameObject);

            //On additionne 1 points à chaque clés amassées
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

    //Fontion pour redonner une vitesse normale à Lola
    void AnnulerPotionVitesse()
    {
        vitesseAugmentee = false;
        vitesseMax /= 1.5f;
    }

    void ApparitionPotionVitesse()
    {

    }
}
