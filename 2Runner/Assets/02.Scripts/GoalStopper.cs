using UnityEngine;

public class GoalStopper : MonoBehaviour
{
	private int _count;
	private void OnTriggerEnter(Collider other){

		// component를 가져오려고 시도하는 과정
		if(other.TryGetComponent(out Runner runner))
		{
			//runner.isMovable = false;
			
			runner.Finish(_count++);
			
		}
	}
}