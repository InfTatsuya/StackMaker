using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem[] winParticles;

    private Player player;

    private void Start()
    {
        player = Player.Instance;

        if (player != null)
        {
            player.onPlayerWin += Player_onPlayerWin;
        }
        //else
        //{
        //    Debug.Log("KO tim thay player");
        //    StartCoroutine(GetPlayerInstance());
        //}
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.onPlayerWin -= Player_onPlayerWin;
        }
    }

    //private IEnumerator GetPlayerInstance()
    //{
    //    while(player == null)
    //    {
    //        yield return new WaitForSeconds(0.2f);

    //        player = Player.Instance;
    //    }

    //    player.onPlayerWin += Player_onPlayerWin;
    //}

    //private void OnEnable()
    //{
    //    if(player == null)
    //    {
    //        player = Player.Instance;
    //    }
    //    else
    //    {
    //        player.onPlayerWin += Player_onPlayerWin;

    //    }
    //}

    //private void OnDisable()
    //{
    //    if (player == null)
    //    {
    //        player = Player.Instance;
    //    }
    //    else
    //    {
    //        player.onPlayerWin -= Player_onPlayerWin;
    //    }
    //}

    private void Player_onPlayerWin(object sender, System.EventArgs e)
    {
        Debug.Log("Play particles");

        foreach(var p in winParticles)
        {
            p.Play();
        }
    }
}
