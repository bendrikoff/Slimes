using System;
using System.Collections.Generic;
using System.Linq;
using Code_Base.Obstacles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Acceleration))]
public class Enemy : Character
{
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Scores;
    [SerializeField] private Image Flag;
    
    [SerializeField] private Acceleration _acceleration;
    [SerializeField] private TrailRenderer _trail;
    
    private CharacterController _controller;

    private List<Food> _food;
    private List<Character> _enemies;

    private List<IEatable> _eatables;

    private Vector3 _runAwayPos;

    private bool _isRunningAway;

    private Vector3 _startPos;

    private Vector3 _startSize;
    private float _startForce;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnDeath += ResetCharacteristics;
        _acceleration.AccelerationStarted += StartAcceleration;
        _acceleration.AccelerationCanceled += CancelAcceleration;
    }

    public override void Eat(IEatable food)
    {
        base.Eat(food);
        Scores.text = Force.ToString();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnDeath -= ResetCharacteristics; 
        _acceleration.AccelerationStarted -= StartAcceleration;
        _acceleration.AccelerationCanceled -= CancelAcceleration;
    }
    public void ResetCharacteristics()
    {
        transform.localScale = _startSize;
        Force = _startForce;
    }

    protected void Start()
    {
        base.Start();
        _startSize = transform.localScale;
        _startForce = Force;
        
        
        _controller = GetComponent<CharacterController>();
        
        _enemies = FindObjectsOfType<Character>().ToList();
        _enemies.Remove(this);

        _food = FoodSpawner.Instance.Foods;
        _eatables = _food.Select(x => (IEatable)x).ToList();
        _eatables.AddRange(_enemies.Select(x => (IEatable)x).ToList());
        _eatables.AddRange(FindObjectsOfType<Player>().ToList());
        _eatables.Remove(this);

        SetNameFlag();
    }

    private void Update()
    {
        if(GamePause.Instance.IsPause) return;
        var closestEatable = _eatables
            .Where(x=>x.GameObject.activeInHierarchy)
            .OrderBy(x => Vector3.Distance(x.GameObject.transform.position, transform.position))
            .FirstOrDefault();
        
        if(closestEatable == null) return;
        
        if (closestEatable is ICharacterInteractable enemy && closestEatable.Force > Force)
        {
            RunAway(enemy);
        }
        else
        {
            Move(closestEatable.GameObject.transform.position);
        }
        
        if (_acceleration.IsAcceleration == false)
        {
            _acceleration.Use(true);
        }
        
    }
    
    public override void Move(Vector3 direction)
    {
       var vector = (direction - transform.position).normalized;
       vector = new Vector3(vector.x, 0, vector.z);
        _controller.Move(vector * MoveSpeed * Time.fixedDeltaTime);
    }

    public void RunAway(ICharacterInteractable character)
    {
        if (_isRunningAway)
        {
            if (Vector3.Distance(transform.position, _startPos) > 15)
            {
                _isRunningAway = false;
                _startPos = Vector3.positiveInfinity;
                _runAwayPos = Vector3.positiveInfinity;
            }
            else
            {
                Move(_runAwayPos);
            }
        }
        else
        {
            Vector3 fleeDirection = transform.position - character.GameObject.transform.position;
            Vector3 fleePosition = transform.position + fleeDirection;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, fleePosition, out hit, 10))
            {
                var normal = hit.normal;

                fleePosition = Vector3.Reflect(fleePosition, normal);

                _runAwayPos = fleePosition;
                _isRunningAway = true;
                _startPos = transform.position;
            }
            Debug.DrawLine(transform.position, fleePosition, Color.red); 
            Move(fleePosition);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent<ICharacterInteractable>(out var interactable))
        {
            interactable.Interact(this);
        }
    }

    private void StartAcceleration()
    {
        _trail.enabled = true;
    }

    private void CancelAcceleration()
    {
        _trail.enabled = false;
    }

    private void SetNameFlag()
    {
        var nameFlag = NameGenerator.Instance.GetName();
        Name.text = nameFlag.Name;
        var flagSprite = NameGenerator.Instance.CountriesFlags.FirstOrDefault(x=>x.Country == nameFlag.Country)?.Flag;
        Flag.sprite = flagSprite;
    }
    
}
