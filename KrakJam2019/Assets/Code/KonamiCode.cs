using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    private string[] _cheatCode;
    private int _index;

    public int Index
    {
        get { return _index; }
        set{
            _index = value;
            if (_index == _cheatCode.Length){
                _index = 0;
                Cheat();
            }
        }
    }

    private void Start()
    {
        _cheatCode = new[] {"w", "w", "s", "s", "a", "d", "a", "d", "mouse 0", "mouse 1", "space"};
    }

    private void Update() {
        if (Input.anyKeyDown) {
            if (Input.GetKeyDown(_cheatCode[Index])) {
               Index++;
            }else {
                Index = 0;    
            }
        }
    }

    private void Cheat(){
        Debug.Log("cheat enabled");
    }
}