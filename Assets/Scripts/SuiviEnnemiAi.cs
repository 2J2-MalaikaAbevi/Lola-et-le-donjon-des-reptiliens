using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Script pour le suivie des ennemis vers le joueur

 */

//Ce script �tait d'abord inspir� de la d�mo vu en classe sur le suivi AI avec les balles vertes et la cible rouge.
//Le souci etait que l'ennemi se retrouvait � faire une rotation sur lui-m�me, tandis que ce script permet le suivi tout en bloquant la rotation en z
//Source d'inspiration pour ce code:
//https://forum.unity.com/threads/making-enemy-ai-follow-script-without-rotating-the-gameobject.255760/
public class SuiviEnnemiAi : MonoBehaviour
{
	public GameObject laCibleJoueur; //Variable pour enregistrwer la cible, c'est-�-dire le joueur
	float vitesse;

	void Start()
	{
		vitesse = Random.Range(0.1f, 2f); //On donne une vitesse al�atoire � l'ennemi
	}


	void Update()
	{	
		//On d�termine la vitesse de rapprochement de l'ennemi sur le temps
		float etape = vitesse * Time.deltaTime;

		//On attribue une nouvelle position � l'ennemi, une position qui fait un suivi jusqu'� la position actuelle de la cible avec une vitesse de rapprochement (etape)
		transform.position = Vector3.MoveTowards(transform.position, laCibleJoueur.transform.position, etape);
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

