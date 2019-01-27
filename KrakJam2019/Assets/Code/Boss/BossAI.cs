using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Boss;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float _speed;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _minDistanceToPlayer;
    [SerializeField] private float _maxDistanceToPlayer;
    [SerializeField] private Transform _strifeRightSide;
    [SerializeField] private Transform _strifeLeftSide;
    [SerializeField] private List<GameObject> _bullets;
    
    private Vector3 position;
    private bool maxPosition = true;
    private bool goingRight = true;
    
    private BossInfo _bossInfo;
    


    private void Start(){
        _bossInfo = GetComponent<BossInfo>();
        position = transform.position;
    }

    private void Update()
    {
        if (CalculateDistanceToPlayer() < _maxDistanceToPlayer &&
            CalculateDistanceToPlayer() > _minDistanceToPlayer){
            StrifeNearPlayer();
        }else MoveToPlayer();
        
    }

    private void DamageRecieved(int damageTaken){
        _bossInfo.CurrentHealth -= damageTaken;
    }
    
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Bullet")){
            DamageRecieved(other.gameObject.GetComponent<RocketShooter>().boomBoomValue);
            Destroy(gameObject);
        }
    }


    private void MoveToPlayer(){
        var step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, step);
    }

    private void StrifeNearPlayer(){
        if (CalculateBossPosition()){
            if (goingRight){
                transform.position += transform.TransformDirection (_strifeRightSide.position);
            }else transform.position += transform.TransformDirection (_strifeLeftSide.position);
        }
        ShootThePlayer();
    }

    private bool CalculateBossPosition(){
        if (Vector3.Distance(transform.position,_strifeRightSide.position) > 1.0f){
            return !maxPosition;
        }
        goingRight = !goingRight;
        return maxPosition;
    }

    private void ShootThePlayer(){
        Instantiate(_bullets[Random.Range(0, _bullets.Count)]);
    }

    private float CalculateDistanceToPlayer(){
        var heading = target.position - _playerTransform.position;
        var distance = heading.magnitude;
        return distance;
    }
}
