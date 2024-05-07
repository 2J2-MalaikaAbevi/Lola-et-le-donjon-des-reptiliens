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
	public GameObject laCibleJoueur;
	float vitesse;

	void Start()
	{
		vitesse = Random.Range(0.1f, 2f);
	}


	void Update()
	{		
		float etape = vitesse * Time.deltaTime;
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

