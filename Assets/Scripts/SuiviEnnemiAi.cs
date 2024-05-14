using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Script pour le suivie des ennemis vers le joueur

 */

//Ce script était d'abord inspiré de la démo vu en classe sur le suivi AI avec les balles vertes et la cible rouge.
//Le souci etait que l'ennemi se retrouvait à faire une rotation sur lui-même, tandis que ce script permet le suivi tout en bloquant la rotation en z
//Source d'inspiration pour ce code:
//https://forum.unity.com/threads/making-enemy-ai-follow-script-without-rotating-the-gameobject.255760/
public class SuiviEnnemiAi : MonoBehaviour
{
	public GameObject laCibleJoueur; //Variable pour enregistrwer la cible, c'est-à-dire le joueur
	public float vitesseMin;
	public float vitesseMax;
	float vitesse;


	void Start()
	{
		vitesse = Random.Range(vitesseMin, vitesseMax); //On donne une vitesse aléatoire à l'ennemi
	}


	void Update()
	{
		Vector2 direction = laCibleJoueur.transform.position - transform.position;

		GetComponent<Rigidbody2D>().velocity = direction.normalized * vitesse;

		//On détermine la vitesse de rapprochement de l'ennemi sur le temps
		/*float etape = vitesse * Time.deltaTime;

		print(etape);

		//On attribue une nouvelle position à l'ennemi, une position qui fait un suivi jusqu'à la position actuelle de la cible avec une vitesse de rapprochement (etape)
		transform.position = Vector3.MoveTowards(transform.position, laCibleJoueur.transform.position, etape);

		GetComponent<Animator>().SetBool("marche", true); */

		if(GetComponent<Rigidbody2D>().velocity.x > Mathf.Abs(0.9f))
        {
			GetComponent<Animator>().SetBool("marche", true);
		}
        else
        {
			GetComponent<Animator>().SetBool("marche", false);
		}

		
		if(GetComponent<Rigidbody2D>().velocity.x > 0)
        {
			GetComponent<SpriteRenderer>().flipX = false;
        }
		else if (GetComponent<Rigidbody2D>().velocity.x < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
	}

    void OnCollisionEnter2D(Collision2D infoCollision)
    {
		if(infoCollision.gameObject.name == "Lola") {
			vitesse = 0;
			//GetComponent<Animator>().SetBool("marche", false);
		}    
    }

    void OnCollisionExit2D(Collision2D infoCollision)
    {
		if (infoCollision.gameObject.name == "Lola")
		{
			Invoke("Start", 1f);
			//GetComponent<Animator>().SetBool("marche", true);
		}
	}

    //Code vu dans le cadre du cours que je voulais utiliser initialement:
    /*public class AiBouleVerte : MonoBehaviour
	{

		public GameObject laCible;
		float vitesse;

		void Start()
		{
			vitesse = Random.Range(0.1f, 2f);
		}


		void Update()
		{

			Vector2 direction = laCible.transform.position - transform.position;

			transform.right = direction;

			GetComponent<Rigidbody2D>().velocity = direction.normalized * vitesse;
		}
	}*/
}

