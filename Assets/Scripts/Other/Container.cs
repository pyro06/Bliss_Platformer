
using System.Collections.Generic;

using UnityEngine;
/// <summary>
/// container for ghosting sprites. triggers a new ghosting object over a set amount of time, referencing a sprite renderer's current sprite
/// combined, this forms a trailing effect behind the player 
/// </summary>
public class Container : MonoBehaviour
{
	private List<AutoSprite> _inactiveAutoSpritesPool; //a list of sprites stored in memory
	
	public float _spawnRate;
	public int _trailLength=1; //effect duration
	public float _desiredAlpha = 0.8f;
	public float _effectDuration = 1f;

	public Color _desiredColor;

	private float _nextSpawnTime;
	private int _sortingLayer;
	private Queue<AutoSprite> _AutoSpritesQueue; 
	private bool _hasStarted;
	private SpriteRenderer _refSpriteRenderer; //sprite renderer to reference
	private GameObject _ghostSpritesParent;
	private bool useTint;
	
	public List<AutoSprite> InactiveGhostSpritePool
	{
		get
		{
			if (_inactiveAutoSpritesPool == null)
			{
				_inactiveAutoSpritesPool = new List<AutoSprite>(5);
			}
			return _inactiveAutoSpritesPool;
		}
		set { _inactiveAutoSpritesPool = value; }
	}
	
	public Queue<AutoSprite> AutoSpritesQueue
	{
		get{
			if (_AutoSpritesQueue == null)
			{
				_AutoSpritesQueue = new Queue<AutoSprite>(_trailLength);
			}
			return _AutoSpritesQueue;
		}
		set { _AutoSpritesQueue = value; }
		
	}
	
	public GameObject GhostSpritesParent
	{
		get
		{
			if (_ghostSpritesParent == null)
			{
				_ghostSpritesParent = new GameObject();
				_ghostSpritesParent.transform.position = Vector3.zero;
				_ghostSpritesParent.name = "AutoParent";
			}
			return _ghostSpritesParent;
		}
		set { _ghostSpritesParent = value; }
	}

	void Start()
	{
		//Init(_trailLength, _spawnRate, GetComponent<SpriteRenderer>(), _effectDuration,_desiredAlpha);
	}

	/// <summary>
	/// Initializes and starts the ghosting effect with the given params but with an option to tint. Please note that the effect duration no longer has an effect on the object in question.  
	/// </summary>
	/// <param name="trailLength"></param>
	/// <param name="spawnRate"></param>
	/// <param name="refSpriteRenderer"></param>
	/// <param name="effectDuration"></param>
	/// <param name="desiredColor"></param>
	public void Init(  int trailLength, float spawnRate, SpriteRenderer refSpriteRenderer, float effectDuration, Color desiredColor)
	{
		_desiredColor = desiredColor;
		_trailLength = trailLength;
		_spawnRate=spawnRate;
		_effectDuration = effectDuration;
		_refSpriteRenderer = refSpriteRenderer;
		
		_nextSpawnTime = Time.time;
		_sortingLayer = _refSpriteRenderer.sortingLayerID; 
		_hasStarted = true;
		useTint = true;
	}
	/// <summary>
	/// Initializes and starts the ghosting effect with the given params. Please note that the effect duration no longer has an effect on the object in question.
	/// </summary>
	/// <param name="trailLength"></param>
	/// <param name="spawnRate"></param>
	/// <param name="refSpriteRenderer"></param>
	/// <param name="effectDuration"></param>
	public void Init(int trailLength, float spawnRate, SpriteRenderer refSpriteRenderer, float effectDuration, float desiredAlpha)
	{ 
		_trailLength = trailLength;
		_spawnRate = spawnRate; 
		_effectDuration = effectDuration;
		_refSpriteRenderer = refSpriteRenderer;
		_sortingLayer = _refSpriteRenderer.sortingLayerID;
		_desiredAlpha = desiredAlpha;
		_nextSpawnTime = Time.time;
		useTint = false;
		_hasStarted = true;
	}
	/// <summary>
	/// Stop the ghosting effect
	/// </summary>
	public void StopEffect()
	{
		_hasStarted = false;
	}
	
	void Update()
	{
		if (_hasStarted)
		{ 
			//check for spawn rate
			//check if we're ok to spawn a new ghost
			if (Time.time >=_nextSpawnTime)
			{  
				//is the queue count number equal than the trail length? 
				if (AutoSpritesQueue.Count == _trailLength )
				{ 
					AutoSprite peekedAutoSprite = AutoSpritesQueue.Peek();
					//is it ok to use? 
					bool canBeReused = peekedAutoSprite.CanBeReused();
					if (canBeReused)
					{
						
						//pop the queue
						AutoSpritesQueue.Dequeue();
						AutoSpritesQueue.Enqueue(peekedAutoSprite);
						
						//initialize the ghosting sprite
						if(!useTint)
						{
							peekedAutoSprite.Init(_effectDuration, _desiredAlpha, _refSpriteRenderer.sprite, _sortingLayer,_refSpriteRenderer.sortingOrder-1, _refSpriteRenderer.transform, Vector3.zero);
						}
						else
						{
							peekedAutoSprite.Init(_effectDuration, _desiredAlpha, _refSpriteRenderer.sprite, _sortingLayer,_refSpriteRenderer.sortingOrder-1, _refSpriteRenderer.transform, Vector3.zero,_desiredColor);
						}
						_nextSpawnTime += _spawnRate; 
					}
					else //not ok, wait until next frame to try again
					{ 
					
						return;
					}
				}
				//check if the count is less than the trail length, we need to create a new ghosting sprite
				if (AutoSpritesQueue.Count < _trailLength)
				{ 
					AutoSprite newAutoSprite = Get();
					AutoSpritesQueue.Enqueue(newAutoSprite); //queue it up!					
					if(!useTint)
					{
						newAutoSprite.Init(_effectDuration, _desiredAlpha, _refSpriteRenderer.sprite, _sortingLayer,_refSpriteRenderer.sortingOrder-1, _refSpriteRenderer.transform, Vector3.zero);
					}
					else
					{
						newAutoSprite.Init(_effectDuration, _desiredAlpha, _refSpriteRenderer.sprite, _sortingLayer,_refSpriteRenderer.sortingOrder-1, _refSpriteRenderer.transform, Vector3.zero,_desiredColor);
					}
					_nextSpawnTime += _spawnRate; 
					
				}
				//check if the queue count is greater than the trail length. Dequeue these items off the queue, as they are no longer needed
				if (AutoSpritesQueue.Count > _trailLength)
				{ 
					int difference = AutoSpritesQueue.Count - _trailLength;
					for (int i = 1; i < difference; i++)
					{
						AutoSprite gs = AutoSpritesQueue.Dequeue();
						InactiveGhostSpritePool.Add(gs);
					}
					return;
				}
			}
			
		}
		
		
		
	}
	
	
	
	
	
	/// <summary>
	/// Returns a ghosting sprite 
	/// </summary>
	/// <returns></returns>
	private AutoSprite Get()
	{
		
		for (int i = 0; i < InactiveGhostSpritePool.Count; i++)
		{
			if (InactiveGhostSpritePool[i].CanBeReused())
			{
				return InactiveGhostSpritePool[i];
			}
			
		}
		return BuildNewAutoSprite();
		
		
	}
	
	private AutoSprite BuildNewAutoSprite()
	{
		//create a gameobject and set the current transform as a parent
		GameObject go = new GameObject();
		go.transform.position = transform.position;
		go.transform.parent = GhostSpritesParent.transform;
		
		AutoSprite gs = go.AddComponent<AutoSprite>();
		
		return gs;
	}
	
}