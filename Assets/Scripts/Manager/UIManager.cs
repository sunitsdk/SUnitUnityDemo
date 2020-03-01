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
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameManager _gameManager;
    public Image uiHP;
    public Image avatarPlane;
    public GameObject uiEndGame;
    public GameObject uiWin;
    public GameObject uiLose;
    public GameObject uiPause;
    public Text txtLife;
    public Text txtBoom;
    public Text txtScore;

    public Text txtScoreWin;
    public Text txtCountKill;
    public GameObject[] star;

    public GameObject black_slow;
    public GameObject rocket;

    private bool isShoot;
    // Use this for initialization
    void Start () {
        _gameManager = GetComponent<GameManager>();
        updateData();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void slowGame(bool isSlow)
    {
        black_slow.SetActive(isSlow);
    }

    public void updateHP()
    {
        uiHP.fillAmount = (float)_gameManager._planeController.preHP / _gameManager._planeController.HPmax;
    }

    public void updateAvatar(Sprite avatar)
    {
        avatarPlane.sprite = avatar;
    }

    public void updateScore(int score)
    {
        txtScore.text = "SCORE: " + score;
    }

    public void pauseGame()
    {
        if (_gameManager.gameState == GameState.Play)
        {
//			AdsControl.Instance.showAds ();
            uiPause.SetActive(true);
            _gameManager.gameState = GameState.Pause;
            Time.timeScale = 0;
        }
    }

    public void resumeGame()
    {
        uiPause.SetActive(false);
        _gameManager.gameState = GameState.Play;
        Time.timeScale = 1;
    }

    public void gameOver()
    {
//		AdsControl.Instance.showAds ();
        SoundManager.instance.playSoundEndGame();
        uiLose.SetActive(true);
    }

    public void gameWin()
    {
//		AdsControl.Instance.showAds ();
        SoundManager.instance.playSoundEndGame();
        txtScoreWin.text = "SCORE: " + _gameManager.point;
        txtCountKill.text = "KILL ENEMY: " + _gameManager.countEnemyDie;
        uiWin.SetActive(true);
        forStar();
    }

    void forStar()
    {
        if (_gameManager.countEnemyDie < _gameManager.countEnemy / 3)
        {
            showStar(1);
            PlayerPrefs.SetInt(MenuScript.STAR_KEY +GameSetting.level,1);
        }

        if (_gameManager.countEnemyDie > _gameManager.countEnemy / 3 && _gameManager.countEnemyDie < _gameManager.countEnemy * 2/3)
        {
            showStar(2);
            PlayerPrefs.SetInt(MenuScript.STAR_KEY + GameSetting.level, 2);
        }

        if (_gameManager.countEnemyDie > _gameManager.countEnemy * 2/3)
        {
            showStar(3);
            PlayerPrefs.SetInt(MenuScript.STAR_KEY + GameSetting.level, 3);
        }
    }

    void showStar(int index)
    {
        for (int i = 0; i < star.Length; i++)
        {
            if (i < index)
            {
                star[i].SetActive(true);
            }else
                star[i].SetActive(false);
        }
    }

    public void reset()
    {
        Application.LoadLevel(GameSetting.level+"");
        Time.timeScale = 1;
    }

    public void next()
    {
        Application.LoadLevel(MenuScript.MAP_NAME);
        Time.timeScale = 1;
    }

    public void menu()
    {
        Application.LoadLevel(MenuScript.MENU_NAME);
        Time.timeScale = 1;
    }

    public void updateData()
    {
        txtBoom.text = PlayerPrefs.GetInt(MenuScript.BOOM_KEY) + "";
        txtLife.text = GameSetting.instance.life + ""; 
    }

    public void btBoom()
    {
        if (!isShoot && _gameManager.gameState == GameState.Play && getBoom())
        {
            StartCoroutine(activeRocket());
        }
    }

    bool getBoom()
    {
        int count = PlayerPrefs.GetInt(MenuScript.BOOM_KEY);
        Debug.Log(count + "");
        if (count > 0)
        {
            Debug.Log(count + "");
            count--;
            PlayerPrefs.SetInt(MenuScript.BOOM_KEY, count);
            updateData();
            return true;
        }
        else
            return false;
    }

    IEnumerator activeRocket()
    {
        isShoot = true;
        rocket.SetActive(true);
        UbhObjectPool bullet = FindObjectOfType<UbhObjectPool>();
        Destroy(bullet.gameObject);
        yield return new WaitForSeconds(1.5f);
        rocket.SetActive(false);
        isShoot = false;
    }
}
