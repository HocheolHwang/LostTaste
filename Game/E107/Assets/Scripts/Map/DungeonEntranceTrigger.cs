using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DungeonEntranceTrigger : MonoBehaviour
{
    private HashSet<string> playersInPortal = new HashSet<string>();

    public int totalPlayers;

    public GameObject portal;

    private bool portalActivated = false;

    private void Start()
    {
        //totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        //totalPlayers = 1;
        //portal.SetActive(false);
        //Debug.Log("포탈 비활성화");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("플레이어가 포탈에 진입함");
            //string playerName = other.gameObject.name;
            //playersInPortal.Add(playerName);

            //if (playersInPortal.Count == totalPlayers)
            //{
            //    portal.SetActive(true); 
            //    Debug.Log("Portal activated");
            //    portalActivated = true;
            //}
            portal.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    string playerName = other.gameObject.name;
        //    playersInPortal.Remove(playerName);
        //}
    }
}
