using UnityEngine;
using UnityEngine.UI;
 
public class LoadTexture : MonoBehaviour, CannonStateObserver
{
	private Texture2D myTexture;
    public CannonStateHandler stateHandler;

	public AudioSource victorySound; 
	public AudioSource sadSound;   
	public AudioSource hitGroundSound;  

    public void applyChange(CannonState state){
		if(state.hasLanded){
			//TODO: play landing sounds here
			hitGroundSound.Play();
			if(state.isCorrect()){
            	state.taskImagePath = "Images/feedback_images/Gratulerer";
				//TODO: play victory sounds here
				victorySound.Play();
			}
			else{
				state.taskImagePath = "Images/feedback_images/Dessverre";
				//TODO: play victory sounds here
				sadSound.Play();
			}
        }
		myTexture = Resources.Load(state.taskImagePath) as Texture2D;
		gameObject.GetComponent<RawImage>().texture = myTexture;
    }
 
	// Use this for initialization
	void Start () {
        stateHandler.subscribe(this);
        this.applyChange(this.stateHandler.getCannonState());
	}
}
