using UnityEngine;
using System.Collections;

public class Curse : MonoBehaviour
{

	public float cursedScareFactor;
	public float cursedAttentionFactor;
	public Font font;
	bool isCursing;
	float completion;
	GameObject player;
	Moving movComp;
	// Use this for initialization
	void Start ()
	{
		isCursing = false;
		completion = 0.0f;
		player = GameObject.FindGameObjectWithTag("Player");
		movComp = GameObject.FindGameObjectWithTag("Player").GetComponent<Moving>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isCursing){
			player.GetComponent<Player>().showPlayer();
			if(movComp.execMovement){
				isCursing = false;
				completion = 0.0f;
				Interactable inter = this.gameObject.GetComponent<Interactable>();
				inter.enabled = true;
				player.GetComponent<Player>().hidePlayer();
				return;
			}
			completion+=Time.deltaTime*7.0f;
			if(completion>=101.0f){
				Item item = this.gameObject.GetComponent<Item>();
				item.scareFactor = cursedScareFactor;
				item.attentionFactor = cursedAttentionFactor;
				item.curse(true);
				isCursing = false;
				completion = 0.0f;
				player.GetComponent<Player>().hidePlayer();
			}
		}
	}
	
	void curseObject()
	{
		movComp.goToObject(this.gameObject,curse);
	}
	
	void curse(){
		BroadcastMessage("playAnimation", "curse");
		
		Item item = this.gameObject.GetComponent<Item>();
		isCursing = true;
		Interactable inter = this.gameObject.GetComponent<Interactable>();
			inter.enabled = false;
		
	}
	
	void OnGUI(){
		if(isCursing){
			GUI.skin.font = font;
			GUI.color = Color.red;
			GUI.Label(new Rect(Camera.mainCamera.WorldToScreenPoint(player.transform.position).x,
				Camera.mainCamera.WorldToScreenPoint(player.transform.position).y+player.collider.bounds.size.y,
				200,
				200)
				,(int)completion+"%");
			Debug.Log(Camera.mainCamera.WorldToScreenPoint(this.transform.position).x+" "+Camera.mainCamera.WorldToScreenPoint(this.transform.position).y);

		}
	}
}
