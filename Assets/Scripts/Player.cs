using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    private readonly int MoveString = Animator.StringToHash("Move");
    private readonly int WinString = Animator.StringToHash("Win");
    private readonly int ResetString = Animator.StringToHash("Reset");

    [SerializeField] GameObject playerVisual;
    [SerializeField] float speed = 5f;
    [SerializeField] Transform spawnBrickTransform;
    [SerializeField] GameObject brickPrefab;
    [SerializeField] float offset = 0.2f;
    [SerializeField] Animator anim;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private int amountOfBricks = 0;
    private float winAnimationLegth;
    private List<GameObject> bricksList = new List<GameObject>();
    private int coinAmount;

    private Vector3 startPosition;

    public int CoinAmount
    {
        get => PlayerPrefs.GetInt(UIManager.COIN_KEY, 0);

        set
        {
            coinAmount = value;
            PlayerPrefs.SetInt(UIManager.COIN_KEY, coinAmount);
        }
    }

    public bool IsControllable { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startPosition = transform.position;

        rb = GetComponent<Rigidbody>();

        foreach(AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if(clip.name == "PlayerWin")
            {
                winAnimationLegth = clip.length;
            }
        }

        Debug.Log(winAnimationLegth);
    }

    private void Update()
    {
        rb.velocity = moveDirection * speed;   
    }

    public void Move(Vector2 moveDirection)
    {
        if (!IsControllable) return;

        this.moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.y);
        anim.SetTrigger(MoveString);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            Destroy(other.gameObject);
            amountOfBricks++;
            InstantiateNewBrick();
        }
        else if (other.gameObject.CompareTag("Bridge"))
        {
            if(other.TryGetComponent<Bridge>(out Bridge bridge))
            {
                IsControllable = false;
                other.enabled = false;
                bridge.ShowBrick();
                DestroyBrick();
            }
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            Debug.Log("win");

            IsControllable = false;
        }
        else if (other.gameObject.CompareTag("Treasure"))
        {
            moveDirection = Vector3.zero;
            UpdateFinishVisual();
            anim.SetTrigger(WinString);

            TriggerWinPanel();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bridge"))
        {
            IsControllable = true;
        }
    }

    private void InstantiateNewBrick()
    {
        Vector3 offsetVector = new Vector3(0f, offset * (amountOfBricks - 1), 0f);

        GameObject newBrick = Instantiate(brickPrefab, spawnBrickTransform);
        newBrick.transform.localPosition += offsetVector;
        bricksList.Add(newBrick);

        playerVisual.transform.localPosition = new Vector3(0f, 0.3f + offsetVector.y, 0f);
    }

    private void DestroyBrick()
    {
        amountOfBricks--;

        GameObject brickToDestroy = bricksList[bricksList.Count - 1];
        bricksList.Remove(brickToDestroy);
        Destroy(brickToDestroy);

        playerVisual.transform.localPosition = new Vector3(0f, 0.3f + offset * (amountOfBricks - 1), 0f);

    }

    private void UpdateFinishVisual()
    {
        while(amountOfBricks > 1)
        {
            DestroyBrick();
        }
        playerVisual.transform.localPosition = new Vector3(0f, 0.3f, 0f);
    }

    private void TriggerWinPanel()
    {
        StartCoroutine(DelayOpenWinPanel());
    }

    private IEnumerator DelayOpenWinPanel()
    {
        yield return new WaitForSeconds(winAnimationLegth);

        UIManager.Instance.OpenWinPanel();
    }

    public void NextLevel()
    {
        transform.position = startPosition;
        anim.SetTrigger(ResetString);
    }
}
