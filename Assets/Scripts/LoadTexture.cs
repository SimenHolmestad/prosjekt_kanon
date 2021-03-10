using UnityEngine;
using UnityEngine.UI;
 
public class LoadTexture : MonoBehaviour {
	Texture2D myTexture;
 
	// Use this for initialization
	void Start () {
		// load texture from resource folder
		myTexture = Resources.Load ("Images/hello-world") as Texture2D;
 
		GameObject rawImage = GameObject.Find ("RawImage");
		rawImage.GetComponent<RawImage> ().texture = myTexture;
	}
}