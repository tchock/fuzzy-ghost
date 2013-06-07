using System.Collections;
using UnityEngine;


public class WanderState : AIState
{
	private Moving movingComoponent;
	private bool movingToTransition;
	private GameObject nextTarget;
	
	public WanderState (StateMachine sm) : base(sm)
	{
		movingComoponent = stateMachine.Enemy.getMovingComponent();
	}
	
	public override void enterState(){
		if(stateMachine.Enemy.getCharacterPath().Length == 0){
			stateMachine.Enemy.setCharacterPath();
		}
		movingComoponent = stateMachine.Enemy.getMovingComponent();
	}
	
	public override void updateAI(){
		
		if(!stateMachine.Enemy.EnemyDetected){
			if(stateMachine.Enemy.getCharacterPath().Length == 0){
				stateMachine.changeState(StateType.IDLE_STATE);
			}
			else{
				if(movingComoponent.finishedAction)
				{
					Debug.Log ("new action");
					if(!movingToTransition){
						nextTarget = stateMachine.Enemy.popNextTarget();
						if(nextTarget.GetComponent<Door>()!=null || nextTarget.GetComponent<Stairs>()!=null)
						{
							movingToTransition = true;
						}
						
						movingComoponent.goToObject(nextTarget);
						Debug.Log("moving to");
					} 
					else
					{
						Debug.Log("else case");
						if(nextTarget.GetComponent<Door>()!=null){
							Door door = nextTarget.GetComponent<Door>();
							door.use(movingComoponent);
							for(int i = 0; i<door.connectedRooms.Length; ++i)
							{
								if(!door.connectedRooms[i].Equals(stateMachine.Enemy.currentLocation)){
									stateMachine.Enemy.currentLocation = door.connectedRooms[i];
									break;
									Debug.Log("fuck");
								}
							}
							movingToTransition = false;
						}
						else{
							Stairs stairs = nextTarget.GetComponent<Stairs>();
							if(stairs.level == 1){
								stairs.goUpstairs(stateMachine.Enemy.gameObject);
								stateMachine.Enemy.currentLocation = stairs.upperMainFloor;
							}
							else{
								stairs.goDownstairs(stateMachine.Enemy.gameObject);
								stateMachine.Enemy.currentLocation = stairs.lowerMainFloor;
							}
							movingToTransition = false;
						}
					}
				}
			}
		}
		else{
			stateMachine.changeState(StateType.ENEMY_DETECTED_STATE);
		}
	
	}
	
	public override void exitState()
	{	
		stateMachine.Enemy.resetCurrentValue();
	}
}


