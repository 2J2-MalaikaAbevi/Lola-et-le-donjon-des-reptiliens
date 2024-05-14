using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Fonctionnement et utilité générale du script:
   Gestion des déplacements horizontaux et verticaux de Lola à l'aide des touches : Left (ou A), Right (ou D), Up (ou W) et Down (ou S).
   Gestion des détections des collisions entre Lola et les objets du jeu.
   Gestion des animations
   Gestion des fins de partie
   Par : Malaïka Abevi
   Dernière modification : 06/05/2024
*/

public class ControleLola : MonoBehaviour
{
    public float vitesseMax; //vitesse désirée pour Lola
    public float vitesseLimite; //La limite de vitesse de Lola

    float vitesseX; //Vitesse de Lola à l'horizontale
    float vitesseY; //Vitesse de Lola à la verticale


    int lesVies = 6; //Le nombre de vie de Lola, on commence avec 3 vies

    //Interface utilisateur*********************************************************************
    public GameObject coeur1; //Variable pour le premier coeur de vie de Lola
    public GameObject coeur2; //Variable pour le deuxième coeur de vie de Lola
    public GameObject coeur3; //Variable pour le troisième coeur de vie de Lola
    public GameObject poingUI; //Variable pour l'affichage du poing de Lola (mode de d'attaque)
    public GameObject armeMagiqueUI; //Variable pour l'affichage de l'arme magique attrapée

    public GameObject laPorteBoss; //Variable pour la porte vers la finale avec le boss
    public GameObject redemarrerPartie; //Variable pour le gameObject qui contient le script pour relancer la partie

    public Sprite coeurPlein; //Variable pour l'image du coeur plein
    public Sprite coeurMoitie; //Variable pour l'image du coeur à moitié plein
    public Sprite coeurFini; //Variable pour l'image du coeur vide/fini
    public Sprite porteOuverte; //Variable pour l'image de la porte vers le boss finale, ouverte

    public TextMeshProUGUI affichageCompteurCle; //Variable pour le texte pour le nombre de clés pour la porte de boss amassées

    public TextMeshProUGUI affichageRecommencerPartie; //Variable pour le texte qui indique de presser sur la barre d'espace pour rejouer

    bool partieTerminee; //Variable pour enregistrer si la partie est terminée ou non
    
    //public bool attaque; Variable possiblement utile pour plus tard

    bool enAttaque = false; //Variable pour savoir si Lola est en attaque ou non
    bool enAttaqueArme = false; //Variable pour savoir si Lola est en attaque avec son arme ou non
    bool vitesseAugmentee = false; //Variable pour savoir si Lola à sa vitesse augmentée ou non

    public static int compteurCle = 0; //Variable pour enregistrer le nombre de clés amassées


    void Update()
    {
        /*Détection des touches pour controler Lola
         * "a" ou fleche gauche pour aller à gauche
         * "d" ou fleche droite pour aller à droite
         * "w" ou fleche haut pour aller en haut
         * "s" ou fleche bas pour aller en bas
         */
        if (!partieTerminee)
        {
            //déplacement vers la gauche
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                vitesseX = -vitesseMax;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            //déplacement vers la droite
            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))   
            {
                vitesseX = vitesseMax;
                GetComponent<SpriteRenderer>().flipX = false;               
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;
                
            }
            
            //déplacement vers le haut
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                vitesseY = vitesseMax;                
            }
            //déplacement vers le bas
            else if(Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                vitesseY = -vitesseMax;               
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;                
            }


            //Gestion de la touche pour l'attaque avec la barre d'espace et le mode attaque OU attaque armée à "true"
            if (Input.GetKeyDown(KeyCode.Space) && enAttaque == false)
            {
                //Enregistrer Lola comme etant en attaque classique
                enAttaque = true;

                //Faire jouer l'animation de l'attaque classique
                GetComponent<Animator>().SetBool("attaque", true);

                //Invoquer la fonction pour arreter l'attaque après 0,5sec, le temps que l'animation joue
                Invoke("AnnulerAttaque", 0.5f);
                
                

                //On augmente la vitesse le Lola pour la propulser lors de l'attaque si elle n'est pas déja à la vitesse limite
                if(GetComponent<SpriteRenderer>().flipX)
                {
                    vitesseX = -20f;
                }
                else if (!GetComponent<SpriteRenderer>().flipX)
                {
                    vitesseX = 20f;
                }

            }

            //Puis on met les valeurs de vitesses pour la vélocité
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


            //On active la marche seulement si la vitesse en X ou en Y est au dela de 0,9
            if(Mathf.Abs(vitesseX) > 0.9 || Mathf.Abs(vitesseY) > 0.9)
            {
                GetComponent<Animator>().SetBool("marche", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("marche", false);
            }
        }


        /***Gestion des images des coeurs selon le nombre de vies et gestion de la mort de Lola***/
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

            //Activer le gameObject qui s'occupe du redemarrage de la partie et du texte l'accompagnant
            redemarrerPartie.SetActive(true);
            //Activer le texte de redémarrage de partie
            affichageRecommencerPartie.gameObject.SetActive(true);

            Invoke("DemarrerGulag", 3f);
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

        //Si on a récuperé 5 clés, la porte s'ouvre
        if(compteurCle == 5)
        {
            //On change l'image de la porte fermée pour une image de porte ouverte
            laPorteBoss.GetComponent<SpriteRenderer>().sprite = porteOuverte;
            laPorteBoss.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    /******************************GESTION DES ATTAQUES, DES POTIONS ET DE LA VIE DE LOLA AVEC COLLISIONS******************************/
    
    void OnCollisionEnter2D(Collision2D infoCollision)
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

            //On indique dans un autre script que le reptilien est bien attaqué, qu'il subit du dégat
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().degat = true;
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
            if (lesVies < 6)
            {
            //On redonne un point de vie à Lola    
            lesVies += 1;
            //On désactive la potion de vie 
            infoCollision.gameObject.SetActive(false);
            }
        }

        //Gestion pour l'arme magique
        if(infoCollision.gameObject.name == "ArmeMagique")
        {
            //On affiche l'arme comme étant acquise
            armeMagiqueUI.SetActive(true);
            //On fait disparaitre l'arme au sol
            Destroy(infoCollision.gameObject); /**Éventuellement, animation de l'arme, donc délai avait avant destroy**/
        }

        //Gestion de la potion de vitesse
        if(infoCollision.gameObject.name == "PotionVitesse")
        {   //Lola ne prendra de la vitesse et la potion disparaitra que si sa vitesse n'est initialement pas augmentée
            if (!vitesseAugmentee)
            {
                //On désactive la potion de vitesse
                infoCollision.gameObject.SetActive(false);
                
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

        //Ramener la vitesse de Lola à la normale
        //vitesseMax /= 1.5f;
        
        //On désactive l'animation d'attaque
        GetComponent<Animator>().SetBool("attaque", false);
    }

    //Fonction pour redonner une vitesse normale à Lola
    void AnnulerPotionVitesse()
    {
        vitesseAugmentee = false;
        vitesseMax /= 1.5f;
    }


    // ********Fonctions qui serviront plus tard pour la réapparition des potions de vie et de vitesse*********
    void ApparitionPotionVie()
    {

    }

    void ApparitionPotionVitesse()
    {

    }

    //Fonction pour le démarrage de la scène où il y a le gulag         ---À travailler---
    void DemarrerGulag()
    {
        SceneManager.LoadScene("GrandChoix");    
    }
}

