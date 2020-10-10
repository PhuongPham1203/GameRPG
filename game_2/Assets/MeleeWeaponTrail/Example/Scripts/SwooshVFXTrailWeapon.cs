using UnityEngine;
using System.Collections;
using System.Linq;

public class SwooshVFXTrailWeapon : MonoBehaviour
{
    /*
	[SerializeField]
	AnimationClip _animation;
	AnimationState _animationState;
	
	[SerializeField]
	int _start = 0;
	
	[SerializeField]
	int _end = 0;
	
	float _startN = 0.0f;
	float _endN = 0.0f;
	
	float _time = 0.0f;
	float _prevTime = 0.0f;
	float _prevAnimTime = 0.0f;
	*/
    [SerializeField]
    MeleeWeaponTrail[] trailL;
	[SerializeField]
	MeleeWeaponTrail[] trailR;

	//bool _firstFrame = true;
	public bool trail;

    void Start()
    {
        /*
		float frames = _animation.frameRate * _animation.length;
		_startN = _start/frames;
		_endN = _end/frames;
		_animationState = GetComponent<Animation>()[_animation.name];
		_trail.Emit = false;
		*/
        //_trail.Emit = trail;

		foreach(MeleeWeaponTrail m in trailL)
        {
			m.Emit = trail;
        }
		foreach (MeleeWeaponTrail m in trailR)
		{
			m.Emit = trail;
		}

	}
	
	public void SetTrailR(int type)
    {

		bool status;

		if (type == 0)
		{
			status = false;
		}
		else
		{
			status = true;
		}

		for (int i = 0; i < this.trailR.Length; i++)
		{
			this.trailR[i].Emit = status;
		}
	}

	public void SetTrailL(int type)
    {


		bool status;

		if (type == 0)
		{
			status = false;
		}
		else
		{
			status = true;
		}

		for (int i = 0; i < this.trailL.Length; i++)
		{
			this.trailL[i].Emit = status;
		}
	}

	public void SetTrailAll(int type)
    {
		bool status;

		if (type == 0)
		{
			status = false;
		}
		else
		{
			status = true;
		}

		for (int i = 0; i < trailL.Length; i++)
		{
			trailL[i].Emit = status;
		}
		for (int i = 0; i < trailR.Length; i++)
		{
			trailR[i].Emit = status;
		}
	}


	/*
	void Update()
	{

		
		_time += _animationState.normalizedTime - _prevAnimTime;
		if (_time > 1.0f || _firstFrame)
		{
			if (!_firstFrame)
			{
				_time -= 1.0f;
			}
			_firstFrame = false;
		}
		
		if (_prevTime < _startN && _time >= _startN)
		{
			_trail.Emit = true;
		}
		else if (_prevTime < _endN && _time >= _endN)
		{
			_trail.Emit = false;
		}
		
		_prevTime = _time;
		_prevAnimTime = _animationState.normalizedTime;
		
	}
	*/
}
