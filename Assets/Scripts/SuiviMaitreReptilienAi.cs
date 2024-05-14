using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuiviMaitreReptilienAi : MonoBehaviour
{
	public GameObject laCibleJoueur; //Variable pour enregistrwer la cible, c'est-�-dire le joueur
	public float vitesseMin;
	public float vitesseMax;
	float vitesse;


	void Start()
	{
		vitesse = Random.Range(vitesseMin, vitesseMax); //On donne une vitesse al�atoire � l'ennemi
	}


	void Update()
	{
		//On d�termine la vitesse de rapprochement de l'ennemi sur le temps
		float etape = vitesse * Time.deltaTime;

		//On attribue une nouvelle position � l'ennemi, une position qui fait un suivi jusqu'� la position actuelle de la cible avec une vitesse de rapprochement (etape)
		transform.position = Vector3.MoveTowards(transform.position, laCibleJoueur.transform.position, etape);

		GetComponent<Animator>().SetBool("marche", true);
	}

	void OnCollisionEnter2D(Collision2D infoCollision)
	{
		if (infoCollision.gameObject.name == "Lola")
		{
			vitesse = 0;
			GetComponent<Animator>().SetBool("marche", false);
		}
	}

	void OnCollisionExit2D(Collision2D infoCollision)
	{
		if (infoCollision.gameObject.name == "Lola")
		{
			Invoke("Start", 1f);
			GetComponent<Animator>().SetBool("marche", true);
		}
	}
}
