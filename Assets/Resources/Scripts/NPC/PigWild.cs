using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigWild : StrongAnimal
{
	protected override void Update()
	{
		base.Update();
		if (theViewAngle.View())
		{
			Chase(theViewAngle.GetTargetPos());
		}
	}
    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 2); // 대기, 걷기.

        if (_random == 0)
            Wait();
        else if (_random == 1)
            TryWalk();
    }


    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }

}
