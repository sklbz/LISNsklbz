using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    GameManager gm;
    public int team;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        int min = int.MaxValue;

        for(int i = 0; i < 3; i++)
        {
            if (gm.TeamPlayers[i] < min)
            {
                team = i;
                min = gm.TeamPlayers[i];
            }
        }

        gm.TeamPlayers[team] += 1;

        team++;
    }
}
