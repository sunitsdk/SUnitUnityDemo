/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

public class GenEnemy : MonoBehaviour {

    [SerializeField]
    GameObject[] _Waves;
    int _CurrentWave;
    GameManager _gameManager;

    public bool loop;

    IEnumerator Start()
    {
        if (_Waves.Length == 0)
        {
            yield break;
        }

        _gameManager = FindObjectOfType<GameManager>();

        while (true)
        {
            while (_gameManager.gameState != GameState.Play)
            {
                yield return 0;
            }

            if (_CurrentWave < _Waves.Length)
            {
                GameObject wave = (GameObject)Instantiate(_Waves[_CurrentWave], transform.position, Quaternion.identity);

                wave.transform.parent = transform;

                while (0 < wave.transform.childCount)
                {
                    yield return 0;
                }

                Destroy(wave);
            }
            else
                yield return 0;

            if (loop)
                _CurrentWave = (int)Mathf.Repeat(_CurrentWave + 1f, _Waves.Length);
            else {
                _CurrentWave++;

                if (_CurrentWave >= _Waves.Length)
                {
                    GenEnemy[] wave = FindObjectsOfType<GenEnemy>();
                    if (wave.Length <= 1)
                    {
                        _gameManager.gameWin();
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
