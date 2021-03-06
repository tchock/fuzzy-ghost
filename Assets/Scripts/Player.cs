using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public float health;
	public bool visible;
	
	public float attentionToPlayer;
	public const float MAX_ATTENTION = 100.0f;
	private GameObject ghostHunter;

	public RoomInventory currentLocation;
	public bool usesStairs = false;

	// Use this for initialization
	void Start ()
	{
		GameObject.FindGameObjectWithTag("layer_mid").GetComponent<Layer>().changeVisibilityByDependence();
		health = 100f;
		ghostHunter = GameObject.FindGameObjectWithTag("ghost_hunter");
		if(ghostHunter!=null)
			ghostHunter.SetActive(false);
	}
	
	public void setCurrentLocation(RoomInventory location) {
		currentLocation = location;
	}
	
	void Update ()
	{
		if(attentionToPlayer>=MAX_ATTENTION && !ghostHunter.activeSelf){
			ghostHunter.SetActive(true);
			ghostHunter.GetComponent<Character>().stateMachine.changeState(StateType.WANDER_STATE);
		}
	}
	/// 
	/// Macht Spieler sichtbar
	///
	public void showPlayer () {
		BroadcastMessage("show", 1f);
		visible = true;
	}
	
	/// 
	/// Macht Spieler unsichtbar
	///
	public void hidePlayer () {
		BroadcastMessage("makeTransparent", 1f);
		visible = false;
	}
	
	/// 
	/// Gibt true zurück, wenn Spieler sichtbar ist
	///
	public bool canBeSeen () {
		return visible;
	}
	
	/// 
	/// Zieht HP vom Spieler ab
	/// Zieht die angebebene Menge HP vom Spieler ab
	/// @param damage Schaden, der abgezogen werden soll
	///
	public void applyDamage (float damage) {
		health -= Mathf.Abs(damage);
	}
	
	/// <summary>
	/// Fügt Aufmerksamkeit auf den Spieler hinzu
	/// </summary>
	/// <param name='amount'>
	/// Hinzugefügter Wert
	/// </param>
	public void raiseAttention(float amount){
		attentionToPlayer += amount;	
		if (attentionToPlayer >= 100f)
			attentionToPlayer = 100f;
	}
	
	/// <summary>
	/// Resets the attention.
	/// </summary>
	public void resetAttention(){
		attentionToPlayer = 0.0f;
	}
	
}

