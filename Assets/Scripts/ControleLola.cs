using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Fonctionnement et utilit� g�n�rale du script:
   Gestion des d�placements horizontaux et verticaux de Lola � l'aide des touches : Left (ou A), Right (ou D), Up (ou W) et Down (ou S).
   Gestion des d�tections des collisions entre Lola et les objets du jeu.
   Gestion des animations
   Gestion des fins de partie
   Par : Mala�ka Abevi
   Derni�re modification : 29/05/2024
*/

public class ControleLola : MonoBehaviour
{
    /*D�CLARATIONS DES VARIABLES*/
    public float vitesseMax; //vitesse d�sir�e pour Lola
    public float vitesseLimite; //La limite de vitesse de Lola

    float vitesseX; //Vitesse de Lola � l'horizontale
    float vitesseY; //Vitesse de Lola � la verticale


    int lesVies = 6; //Le nombre de vie de Lola, on commence avec 3 vies

    //Interface utilisateur*********************************************************************
    public GameObject coeur1; //Variable pour le premier coeur de vie de Lola
    public GameObject coeur2; //Variable pour le deuxi�me coeur de vie de Lola
    public GameObject coeur3; //Variable pour le troisi�me coeur de vie de Lola
    public GameObject poingUI; //Variable pour l'affichage du poing de Lola (mode de d'attaque)
    public GameObject armeMagiqueUI; //Variable pour l'affichage de l'arme magique attrap�e

    public GameObject laPorteBoss; //Variable pour la porte vers la finale avec le boss
    public GameObject redemarrerPartie; //Variable pour le gameObject qui contient le script pour relancer la partie
    public GameObject carreFadeOut; //Variable pour le carr� fait pour le fade out en fin de partie
    Color transparenceFadeOut; //Variable pour la couleur du carr� fait pour le fade out en fin de partie

    public Sprite coeurPlein; //Variable pour l'image du coeur plein
    public Sprite coeurMoitie; //Variable pour l'image du coeur � moiti� plein
    public Sprite coeurFini; //Variable pour l'image du coeur vide/fini
    public Sprite porteOuverte; //Variable pour l'image de la porte vers le boss finale, ouverte

    public TextMeshProUGUI affichageCompteurCle; //Variable pour le texte pour le nombre de cl�s pour la porte de boss amass�es
    public TextMeshProUGUI affichageCompteurCleBonus; //Variable pour le texte pour le nombre de cl�s pour la porte de boss amass�es

    public TextMeshProUGUI affichageRecommencerPartie; //Variable pour le texte qui indique de presser sur la barre d'espace pour rejouer

    bool partieTerminee = false; //Variable pour enregistrer si la partie est termin�e ou non (mort)
    bool partieVictoire = false; //Variable pour enregistrer si la partie est gagn�e ou non (victoire)

    public bool enAttaque = false; //Variable pour savoir si Lola est en attaque ou non
    public bool enAttaqueArme = false; //Variable pour savoir si Lola est en attaque avec son arme ou non
    bool estArmee = false; //Variable pour savoir si Lola est arm� ou non
    bool vitesseAugmentee = false; //Variable pour savoir si Lola � sa vitesse augment�e ou non
    bool immunise = false; //Variable pour pr�venir 

    public static int compteurCle = 0; //Variable pour enregistrer le nombre de cl�s amass�es
    public static int compteurCleBonus = 0; //Variable pour enregistrer le nombre de cl�s amass�es

    /*Les audioclip pour les actions et �venements importants*/
    public AudioClip sonAttaqueLola;
    public AudioClip sonDegatLola;

    public AudioClip sonPotionVie;
    public AudioClip sonPotionVitesse;
    public AudioClip sonCle;
    public AudioClip sonCleBonus;
    public AudioClip sonArmeMagique;
        
    public AudioClip sonMortReptilien;
    public AudioClip sonDegatMaitreRept;
    public AudioClip sonAttaqueReptilien;

    public AudioClip sonDegatReptilien;
    public AudioClip sonMortMaitreRept;
    public AudioClip sonAttaqueMaitreRept;

    public AudioClip sonGargouille;


    void Update()
    {
        /*D�tection des touches pour controler Lola
         * "a" ou fleche gauche pour aller � gauche
         * "d" ou fleche droite pour aller � droite
         * "w" ou fleche haut pour aller en haut
         * "s" ou fleche bas pour aller en bas
         */
        if (!partieTerminee && !partieVictoire)
        {
            //d�placement vers la gauche
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                vitesseX = -vitesseMax;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            //d�placement vers la droite
            else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                vitesseX = vitesseMax;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                vitesseX = GetComponent<Rigidbody2D>().velocity.x;

            }

            //d�placement vers le haut
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                vitesseY = vitesseMax;
            }
            //d�placement vers le bas
            else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                vitesseY = -vitesseMax;
            }
            else
            {
                vitesseY = GetComponent<Rigidbody2D>().velocity.y;
            }


            //Gestion de la touche pour l'attaque avec la barre d'espace et le mode attaque OU attaque arm�e � "true"
            if (Input.GetKeyDown(KeyCode.Space) && enAttaque == false && enAttaqueArme == false)
            {
                //On fait jouer le son d'attaque de Lola
                GetComponent<AudioSource>().PlayOneShot(sonAttaqueLola);

                if (!estArmee)
                {
                    //On immunise Lola pour qu'elle ne subisse pas de d�gat malgr� une attaque de sa part
                    immunise = true;

                    //Enregistrer Lola comme etant en attaque classique
                    enAttaque = true;

                    //Faire jouer l'animation de l'attaque classique
                    GetComponent<Animator>().SetBool("attaque", true);
                }
                else if(estArmee)
                {
                    //On immunise Lola pour qu'elle ne subisse pas de d�gat malgr� une attaque de sa part
                    immunise = true;

                    //Enregistrer Lola comme etant en attaque classique
                    enAttaqueArme = true;

                    //Faire jouer l'animation de l'attaque classique
                    GetComponent<Animator>().SetBool("attaque_arme", true);
                }

                //Invoquer la fonction pour arreter l'attaque apr�s 0,5sec, le temps que l'animation joue
                Invoke("AnnulerAttaque", 1.5f);

                //On augmente la vitesse le Lola pour la propulser lors de l'attaque si elle n'est pas d�ja � la vitesse limite
                if (GetComponent<SpriteRenderer>().flipX)
                {
                    vitesseX = -55f;
                }
                else if (!GetComponent<SpriteRenderer>().flipX)
                {
                    vitesseX = 55f;
                }
            }

            //Puis on met les valeurs de vitesses pour la v�locit�
            GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


            //On active la marche seulement si la vitesse en X ou en Y est au dela de 0,9
            if (Mathf.Abs(vitesseX) > 0.9f || Mathf.Abs(vitesseY) > 0.9f)
            {
                GetComponent<Animator>().SetBool("marche", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("marche", false);
            }
        }
        else if (partieTerminee || partieVictoire)
        {
            //On d�sactive le collider pour qu'on ne puisse plus se faire pousser
            GetComponent<CapsuleCollider2D>().enabled = false;

            //On enregistre la couleur du carr� dans la variable Color "transparence"
            transparenceFadeOut = carreFadeOut.GetComponent<SpriteRenderer>().color;
            //Puis, on modifie le niveau de transparence du carr� de transparent � opaque graduellement
            transparenceFadeOut.a += 0.002f;
            //Puis on r�applique la nouvelle valeur de couleur
            carreFadeOut.GetComponent<SpriteRenderer>().color = transparenceFadeOut;

            if (partieTerminee) Invoke("DemarrerSceneMort", 6f); //Appel de la fonction pour la sc�ne de mort
            if (partieVictoire) Invoke("DemarrerSceneVictoire", 6f); //Appel de la fonction pour la sc�ne de victoire

            //On remet le compteur des cl�s � 0
            compteurCle = 0;
            //On met �galement le compteur de la cl� bonus � z�ro
            compteurCleBonus = 0;
            //On remet les compteurs pour les zones de bataille � z�ro
            GestionBataille.reptileBataille1 = 0;
            GestionBataille.reptileBataille2 = 0;
            GestionBataille.reptileBataille3 = 0;
            GestionBataille.reptileBataille4 = 0;
            GestionBataille.reptileBataille5 = 0;
            GestionBataille.reptileBataille6 = 0;
            GestionBataille.reptileBataille7 = 0;
            //On rend fausse le fait que la bataille finale ait commenc�
            ActiverCombatLola.batailleBoss = false;
        }


        /***Gestion des images des coeurs selon le nombre de vies et gestion de la mort de Lola***/
        //�tablir la contrainte pour le nombre de vie, il ne faut pas que ce soit au dessus de 6 points;
        else if (lesVies > 6)
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

            //Activer le gameObject qui s'occupe du redemarrage de la partie et du texte l'accompagnant
            //redemarrerPartie.SetActive(true);
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

        //Si on a r�cuper� 5 cl�s, la porte s'ouvre
        if (compteurCle == 7)
        {
            //On change l'image de la porte ferm�e pour une image de porte ouverte
            laPorteBoss.GetComponent<SpriteRenderer>().sprite = porteOuverte;
            laPorteBoss.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    /******************************GESTION DES ATTAQUES, DES POTIONS ET DE LA VIE DE LOLA AVEC COLLISIONS******************************/

    void OnCollisionEnter2D(Collision2D infoCollision)
    {
        /*Gestion des attaques donn�es et re�ues*/
        if (infoCollision.gameObject.tag == "reptileNormal" && !enAttaque && !enAttaqueArme)
        {
            //On fait perdre une vie � Lola
            lesVies -= 1;
            //Indiquer que le reptilien est en attaque (qui aura une incidence dans le script de la gestion des reptiliens
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().enAttaque = true;

            infoCollision.gameObject.GetComponent<Animator>().SetBool("attaque", true);

            immunise = true;

            //On fait jouer le son de d�gat de Lola
            GetComponent<AudioSource>().PlayOneShot(sonDegatLola);

            //Appel de la fonction qui rendra � nouveau possible de subir du d�gat
            Invoke("ProtectionSurattaque", 3f);

            //On fait jouer le son d'attaque
            GetComponent<AudioSource>().PlayOneShot(sonAttaqueReptilien);
        }

        if (infoCollision.gameObject.name == "MaitreReptilien" && !enAttaque && !enAttaqueArme)
        {
            lesVies -= 1;
            //Indiquer que le reptilien est en attaque (qui aura une incidence dans le script de la gestion du maitre reptilien
            infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().enAttaque = true;


            infoCollision.gameObject.GetComponent<Animator>().SetBool("attaque", true);

            //Appel de la fonction qui rendra � nouveau possible de subir du d�gat
            Invoke("ProtectionSurattaque", 3f);

            //On fait jouer le son d'attaque
            GetComponent<AudioSource>().PlayOneShot(sonAttaqueReptilien);
        }

        //Si Lola est en attaque lors de la collision avec un reptilien, elle fera perd 1 vie au reptilien touch�, en accedeant au script qui g�re les reptiliens et en changeant la variable de vie 
        if (infoCollision.gameObject.tag == "reptileNormal" && enAttaque)
        {
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies -= 1;
                
            print(infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies);

            if (infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies <= 0)
            {
                //On fait jouer le son de mort du  Reptilien
                GetComponent<AudioSource>().PlayOneShot(sonMortReptilien);
            }
            else if (infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies > 0)
            {
                //On indique dans un autre script que le reptilien est bien attaqu�, qu'il subit du d�gat
                infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().degat = true;
                //Animation de d�gat du reptiliens
                infoCollision.gameObject.GetComponent<Animator>().SetBool("degat", true);
                //On fait jouer le son de d�gat du  Reptilien
                GetComponent<AudioSource>().PlayOneShot(sonDegatReptilien);
            }

            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("AnnulerAttaque", 0.5f);
        }

        if (infoCollision.gameObject.name == "MaitreReptilien" && enAttaque)
        {
            infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().lesVies -= 1;

            if (infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().lesVies <= 0)
            {
                //On indique que la partie a �t� gagn�e
                partieVictoire = true;

                //On fait jouer le son de mort du Maitre Reptilien
                GetComponent<AudioSource>().PlayOneShot(sonMortMaitreRept);
            }
            else if (infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().lesVies > 0)
            {
                //Animation de d�gat du Maitre reptilien
                infoCollision.gameObject.GetComponent<Animator>().SetBool("degat", true);

                //On indique dans un autre script que le Maitre reptilien est bien attaqu�, qu'il subit du d�gat
                infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().degat = true;
                    
                //On fait jouer le son de d�gat du Maitre Reptilien
                GetComponent<AudioSource>().PlayOneShot(sonDegatMaitreRept);
            }
        }

        //Si Lola est en attaque et arm�e lors de la collision avec un reptilien ou le maitre reptilien, elle fera perd 2 points de vie au reptilien touch�, en accedeant au script qui g�re les reptiliens et en changeant la variable de vie 
        if (infoCollision.gameObject.tag == "reptileNormal" && enAttaqueArme)
        {
            infoCollision.gameObject.GetComponent<GestionReptiliensNormal>().lesVies -= 2;

            //On fait jouer le son de mort du  Reptilien
            GetComponent<AudioSource>().PlayOneShot(sonMortReptilien);

            Invoke("AnnulerAttaque", 3f);
        }
                
        if (infoCollision.gameObject.name == "MaitreReptilien" && enAttaqueArme)
        {
            infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().lesVies -= 2;

            //Animation de d�gat du Maitre reptilien
            infoCollision.gameObject.GetComponent<Animator>().SetBool("degat", true);

            //On indique dans un autre script que le Maitre reptilien est bien attaqu�, qu'il subit du d�gat
            infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().degat = true;

            if (infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().lesVies <= 0)
            {
                //On fait jouer le son de mort du Maitre Reptilien
                GetComponent<AudioSource>().PlayOneShot(sonMortMaitreRept);
                //On indique que la partie a �t� gagn�e
                partieVictoire = true;
            }
            else if (infoCollision.gameObject.GetComponent<GestionMaitreReptilien>().lesVies > 0)
            {
                //On fait jouer le son de d�gat du Maitre Reptilien
                GetComponent<AudioSource>().PlayOneShot(sonDegatMaitreRept);
            }
        }

        //Gestion pour l'arme magique
        if (infoCollision.gameObject.name == "ArmeMagique")
        {
            //On affiche l'arme comme �tant acquise
            armeMagiqueUI.SetActive(true);

            //On d�sactive le UI du poing
            poingUI.SetActive(false);

            //On fait disparaitre l'arme au sol
            Destroy(infoCollision.gameObject);

            estArmee = true; //On rend vrai que Lola est arm�e
            
            //Jouer le son de l'arme attrap�
            GetComponent<AudioSource>().PlayOneShot(sonArmeMagique);
        }

        //Gestion de la potion de vie
        if (infoCollision.gameObject.name == "PotionVie")
        {
            //Lola ne prendra de la vie et la potion disparaitra que si elle a moins de 6 point de vie
            if (lesVies < 6)
            {
                //On redonne un point de vie � Lola    
                lesVies += 1;
                //On d�sactive la potion de vie 
                infoCollision.gameObject.SetActive(false);

                //On fait jouer le son de la potion de vie
                GetComponent<AudioSource>().PlayOneShot(sonPotionVie);
            }
        }

        //Gestion de la potion de vitesse
        if (infoCollision.gameObject.name == "PotionVitesse")
        {   //Lola ne prendra de la vitesse et la potion disparaitra que si sa vitesse n'est initialement pas augment�e
            if (!vitesseAugmentee)
            {
                //On d�sactive la potion de vitesse
                infoCollision.gameObject.SetActive(false);

                //On augmente la vitesse de Lola
                vitesseMax *= 1.5f;

                //Enregistrer que la vitesse de Lola a �t� acc�l�r�e
                vitesseAugmentee = true;

                //Puis on invoque une fonction qui va annuler les effets de la potion apr�s 8sec
                Invoke("AnnulerPotionVitesse", 8f);

                //On fait jouer le son de la potion de vitesse
                GetComponent<AudioSource>().PlayOneShot(sonPotionVitesse);
            }
        }

        /*GESTION DES CL�S pour la FINALE AVEC BOSS*/
        if (infoCollision.gameObject.name == "Cle")
        {
            //On d�truit les cl�s r�colt�es
            Destroy(infoCollision.gameObject);

            //On additionne 1 points � chaque cl�s amass�es
            compteurCle += 1;

            //On affiche la valeur du compteur dans l'interface du jeu
            affichageCompteurCle.text = compteurCle.ToString();

            //On fait jouer le son de la cle
            GetComponent<AudioSource>().PlayOneShot(sonCle);
        }

        if (infoCollision.gameObject.name == "CleBonus")
        {
            //On d�truit les cl�s r�colt�es
            Destroy(infoCollision.gameObject);

            //On additionne 1 points � chaque cl�s amass�es
            compteurCleBonus += 1;

            //On fait jouer le son de la cle bonus
            GetComponent<AudioSource>().PlayOneShot(sonCleBonus);

            //On affiche la valeur du compteur dans l'interface du jeu
            affichageCompteurCleBonus.text = compteurCleBonus.ToString();
        }
    }

     void OnTriggerEnter2D(Collider2D infoCollision)
    {   
        //Pour avoir un effet sonore lorsqu'elle touche une gargouille
        if (infoCollision.gameObject.tag == "gargouille")
        {
            GetComponent<AudioSource>().PlayOneShot(sonGargouille);
        }
    }
    //Fonction pour terminer l'attaque
    void AnnulerAttaque()
        {
            //Rendre l'attaque fausse
            enAttaque = false;
            enAttaqueArme = false;

            //On d�sactive l'animation d'attaque
            GetComponent<Animator>().SetBool("attaque", false);
            GetComponent<Animator>().SetBool("attaque_arme", false);

            //On r�active le collider
            GetComponent<CapsuleCollider2D>().enabled = true;
        }

        //Fonction pour �viter que subir des d�gats beaucoup trop de fois et inutilement
        void ProtectionSurattaque()
        {
            immunise = false;
        }

        //Fonction pour redonner une vitesse normale � Lola
        void AnnulerPotionVitesse()
        {
            vitesseAugmentee = false;
            vitesseMax /= 1.5f;
        }

        //Fonction pour passer � la sc�ne de mort
        void DemarrerSceneMort()
        {
            SceneManager.LoadScene("FinMort");
        }

        //Fonction pour passer � la sc�ne de victoire
        void DemarrerSceneVictoire()
        {
            SceneManager.LoadScene("FinVictoire");
        }
}

