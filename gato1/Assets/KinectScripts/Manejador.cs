using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vuforia;
using System.IO;
using UnityEngine.UI;

public class Manejador : MonoBehaviour {

  //Indica si se va a ejecutar la animación del animator
  //The Animator component is used to assign animation to a GameObject in your scene. 
  //The Animator component requires a reference to an Animator Controller
  private Animator armando;
  private Animator anim;
  //String que contiene el nombre de la animación a ejecutar y servirá para modificar el estado del animatorController
  private AudioSource[] audios;
  private AudioSource audioArticulo;
  private AudioSource audioVerbo;
  private AudioSource audioSujeto;
  private AudioSource audioAdjetivo;

  private GameObject confetti;
  public Text fraseText;
    public Text animoText;
    public List<GameObject> modelos;

    private string frase;
	private int rand;


  //Inicializador de la clase 
  void Start(){
    //anim = GetComponent<Animator>();




  }
  // Update is called once per frame
  void Update () {
    // Get the StateManager, sirve para manipular el estado de los objetos que pueden ser rastreados por vuforia
    StateManager sm = TrackerManager.Instance.GetStateManager ();

    // Query the StateManager to retrieve the list of
    // currently 'active' trackables 
    //(i.e. the ones currently being tracked by Vuforia)
    //Lista de marcas que están siendo vistas por la cámara, ordenadas según la distancia euclideana al origen.
    //El origen está configurado en el componente de la cámara de vuforia "Vuforia Behaviour" en la opción "World Center Mode"
    //con la constante SPECIFIC_TARGET, usando en el  "World Center" el Object "FrameMarker0"
	IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ().ToList();
    //Iterador para recorrer la lista ordenada de trackables y obtener de cada uno su significado guardado en su nombre
    //para ir construyendo la oración
    //en la variable frase al final del iterador contendrá la concatenación de todos los nombres según el orden en 
    //que se encuentran las marcas respecto al origen

	foreach (TrackableBehaviour tb in activeTrackables) {
        for (int i = 0; i < tb.transform.childCount; i++)
        {
                if (tb.transform.GetChild(i).gameObject.activeInHierarchy) {
                    modelos.ForEach(delegate (GameObject item)
                    {
                        item.SetActive(false);
                    });
                    GameObject model = modelos.Find(item => item.name == tb.transform.GetChild(i).tag);
                    model.SetActive(true);
                    
                }
        }

	
        /*if ((!audioAR_Si.isPlaying) && frase != frase_anterior) {
		    rand = Random.Range(0, 4);
		    animo = fraseSI[rand];
		    StartCoroutine(PlaySound(audioAR_Si,activeTrackables));
	    }*/
			//confetti.SetActive (true);

	}

  }


	IEnumerator PlaySound(AudioSource sonidoFinal, IEnumerable<TrackableBehaviour> tbs){
		foreach (TrackableBehaviour tb in tbs) {
			if (tb.tag == "Inicio")
				continue;
			AudioSource audio = tb.GetComponent<AudioSource> ();
			audio.Play ();
			yield return new WaitForSeconds(audio.clip.length);
		}
		sonidoFinal.Play ();
		confetti.SetActive (true);
	}

}
