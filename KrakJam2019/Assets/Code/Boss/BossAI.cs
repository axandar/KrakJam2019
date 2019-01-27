using System.Collections;
using System.Collections.Generic;
using Code;
using Code.Boss;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _minDistanceToPlayer;
    [SerializeField] private float _maxDistanceToPlayer;
    [SerializeField] private Transform _strifeRightSide;
    [SerializeField] private Transform _strifeLeftSide;
    [SerializeField] private List<GameObject> _bullets;
    [SerializeField] private float reload;
    
    private Vector3 position;
    private bool maxPosition = true;
    private bool goingRight = true;
    private bool firing = true;
    
    private BossInfo _bossInfo;
    


    private void Start(){
        _bossInfo = GetComponent<BossInfo>();
        position = transform.position;
    }

    private void FixedUpdate()
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
        var step = _speed * Time.deltaTime;
        if (goingRight){
            if (IsNearRightWall()){
                goingRight = false;
            }else{
                Debug.Log("Going Right Direction");
                transform.position = Vector3.MoveTowards(transform.position, _strifeRightSide.position, step);
            }
        }else if (IsNearLeftWall()){
            goingRight = true;
        }else
        {transform.position = Vector3.MoveTowards(transform.position, _strifeLeftSide.position, step);}
        
       
        if (firing){
          StartCoroutine(ShootThePlayer());
            
        }
    }
    private bool IsNearRightWall(){
        if (Vector3.Distance(transform.position,_strifeRightSide.position) < 10.0f){
            return true;
        }
            return false;
    }
    private bool IsNearLeftWall(){
        if (Vector3.Distance(transform.position,_strifeLeftSide.position) < 10.0f){
            return true;
        }
            return false;
    }


   
    private float CalculateDistanceToPlayer(){
        var heading = transform.position - _playerTransform.position;
        var distance = heading.magnitude;
//        Debug.Log(transform.position);
//        Debug.Log(_playerTransform.position);
//        Debug.Log(heading);
//        Debug.Log(distance);
        return distance;
    }



    IEnumerator ShootThePlayer()
    {
        firing = false;
        yield return new WaitForSeconds(reload);
        Instantiate(_bullets[Random.Range(0, _bullets.Count)],transform.position,Quaternion.identity);
        firing = true;
    }
}
