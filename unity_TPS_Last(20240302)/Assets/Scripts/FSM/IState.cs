using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
	public void EnterState(Enemy enemy);
	public void UpdateState();
	public void ExitState();
}
