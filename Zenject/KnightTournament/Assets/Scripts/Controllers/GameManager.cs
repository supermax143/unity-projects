using Knights;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine;

public class GameManager : MonoBehaviour {

    private const float DISTANCE_TO_MAXIMAZE_CAMERA = 25;
    private const float DISTANCE_TO_SLOW_DOWN = 5;

    private enum GameState {Acceleration, SlowMotion, WaitCloseDistance, SeeResult};

    public GameObject player;
    public GameObject enemy;
    private GameObject playerMoto;
    private GameObject enemyMoto;
    private GUIManager uiManager;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }

    }

    private GameState currentState = GameState.Acceleration;

    void Awake()
    {
            
    }
    
    void Start () 
    {
        InitLayersCollision();
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        playerMoto = GameObject.FindGameObjectWithTag(Tags.PlayerMoto);
        enemy = GameObject.FindGameObjectWithTag(Tags.Enemy);
        enemyMoto = GameObject.FindGameObjectWithTag(Tags.EnemyMoto);
        uiManager = GameObject.Find("Managers").GetComponent<GUIManager>();
        _instance = this;
        
    }

    private void InitLayersCollision()
    {
        SetCollisionIgnore(Layer.Default, Layer.Void, true);
        SetCollisionIgnore(Layer.Motorcicle, Layer.Motorcicle, true);
        SetCollisionIgnore(Layer.Rider, Layer.Rider, true);
        SetCollisionIgnore(Layer.Rider, Layer.Motorcicle, true);
        SetCollisionIgnore(Layer.Spear, Layer.Motorcicle, true);
        SetCollisionIgnore(Layer.Spear, Layer.Void, true);
        SetCollisionIgnore(Layer.Spear, Layer.Spear, true);
        SetCollisionIgnore(Layer.Void, Layer.Void, true);
        SetCollisionIgnore(Layer.Rider, Layer.Void, true);
        SetCollisionIgnore(Layer.Motorcicle, Layer.Void, true);
    }

    public bool IsOponentDead(string tag) 
    {
        if (tag == Tags.Player)
            return enemy.GetComponent<BikerController>().Dead;
        else
            return player.GetComponent<BikerController>().Dead;
    }
    

    public void ResetGame()
    {
        SceneManager.LoadScene("Level_1");
        SetTimeScale(1);
    }

    public void StartGame()
    {
        enemyMoto.GetComponent<MotorcycleController>().moving = true;
        playerMoto.GetComponent<MotorcycleController>().moving = true;
    }

    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
        Time.fixedDeltaTime = value * 0.02f;
    }

    public void SetCollisionIgnore(string layer1, string layer2, bool ignore)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layer1), LayerMask.NameToLayer(layer2), ignore);
    }

	// Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == GameState.Acceleration)
            onStateAcceleration();
        if (currentState == GameState.SlowMotion)
            onStateSlowMotion();
        else if (currentState == GameState.WaitCloseDistance)
            onStateWaitCloseDistance();
    }

    void onStateAcceleration()
    {
        Camera[] cams = GameObject.FindObjectsOfType<Camera>();
        if (Vector2.Distance(cams[0].transform.position, cams[1].transform.position) > CameraWidth + CameraWidth / 2)
            return;
        SetTimeScale(0.1f);
        currentState = GameState.SlowMotion;
    }

    private void onStateSlowMotion()
    {
        Camera[] cams = GameObject.FindObjectsOfType<Camera>();
        if (Vector2.Distance(cams[0].transform.position, cams[1].transform.position) > CameraWidth)
            return;
        cams[0].GetComponent<CameraController>().Maximize();
        cams[1].enabled = false;
        currentState = GameState.WaitCloseDistance;
    }

    private void onStateWaitCloseDistance()
    {
        if (Vector2.Distance(player.transform.position, enemy.transform.position) > DISTANCE_TO_SLOW_DOWN)
            return;
        SetTimeScale(.1f);
        currentState = GameState.SeeResult;
    }

    private float CameraWidth
    {
        get { return ((Camera.main.orthographicSize * 2) * Screen.width / Screen.height) * Camera.main.rect.width; }
    }
}
