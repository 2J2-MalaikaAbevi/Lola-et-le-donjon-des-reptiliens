using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuiviMaitreReptilienAi : MonoBehaviour
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


		if(GetComponent<Rigidbody2D>().velocity.x > Mathf.Abs(0.01f))
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
