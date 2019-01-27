using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkHouseSpawn : MonoBehaviour
{
    private bool _dontAsk = true;
    [SerializeField] private GameObject _pinkHouse;
    [SerializeField] private float timeToSpawn;
    
    private void Awake(){
        StartCoroutine(SpawnBonus());
    }
    
    private IEnumerator SpawnBonus(){
        while (true) {
            _dontAsk = true;
            
                var x = Random.Range(0f,1f);
                var y = Random.Range(0f,1f);
					
                var _bonus =  Instantiate(_pinkHouse, 
                    Camera.main.ViewportToWorldPoint(new Vector3(x, y, 1))
                    , new Quaternion(0,0,0,0));
                yield return new WaitForSeconds(timeToSpawn);
                if (_bonus != null) {
                    _dontAsk = false;
                    Destroy(_bonus);
                yield return new  WaitForSeconds(timeToSpawn);
            }
        }
    }
}
