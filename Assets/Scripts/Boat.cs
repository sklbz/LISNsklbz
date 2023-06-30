using System;
using UnityEngine;

public class Boat : MonoBehaviour {

    [SerializeField]
    int teamNumber;
    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    State _state = State.STATE_MOVING, _previousState;
    float nominalSpeed = 3.0f, firingRate = 2, time, _speed;
    Vector3 StartingPosition;
    [SerializeField]
    PlayerManager playerManager = null;
    GameManager gm;

    void Start () {
        StartingPosition = transform.position;
        playerManager = FindObjectOfType<PlayerManager>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update() {
        FindPlayerManager();

        if (playerManager == null || playerManager.team != teamNumber)
            return;

        switch (_state)
        {
            // handle moving state
            case State.STATE_MOVING:
                _speed = nominalSpeed / gm.TeamPlayers[teamNumber];
                
                Vector3 _velocity = Input.acceleration;

                /*if (_velocity.sqrMagnitude > 1)
                {
                    _velocity.Normalize();
                }*/

                float highestVelocity = _velocity.x;

                if (_velocity.y > highestVelocity)
                {
                    highestVelocity = _velocity.y;
                }
                    
                if (_velocity.z > highestVelocity)
                {
                    highestVelocity = _velocity.z;
                }

                _velocity = Vector3.zero;

                _velocity.y = highestVelocity;

                transform.Translate(_speed * Time.deltaTime * _velocity);

                break;

            // handle attacking state
            case State.STATE_SHOOTING:
                int axis = (int)Input.GetAxis("Horizontal");
                time += Time.deltaTime;
                if (Math.Abs(axis) == 1 && time >= 1 / firingRate)
                {
                    Instantiate(Bullet, new Vector3(transform.position.x + axis, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, axis == 1 ? 180 : 0));
                    // (axis == 1 ? 180 : 0) <=> (axis * 90 + 90) because -1 * 90 + 90 = 0 and 1 * 90 + 90 = 180;

                    time = 0;
                }

                break;

            case State.STATE_RESPAWNING:
                time += Time.deltaTime;
                if (time >= .5f)
                {
                    time = 0;
                    transform.position = StartingPosition;
                    _state = _previousState;
                }

                break;
        }

        Vector3 input = Input.acceleration;

        //Debug.Log($"accel:{input}\nspeed:{Time.deltaTime * input}\npos:{Time.deltaTime * Time.deltaTime * input}");
    }

    void OnCollisionEnter2D(Collision2D collision) {
        string tag = collision.gameObject.tag;
        switch (tag) {
            case ("Finish"):
                Debug.Log($"Team {teamNumber} won!");

                break;

            case ("Bullet"):
                Respawn();

                break;
        }
        PhotonNetwork.LoadLevel($"Team{teamNumber}");
    }

    public void SetStateMove() {
        _state = State.STATE_MOVING;
    }

    public void SetStateAttack() {
        _state = State.STATE_SHOOTING;
    }

    void Respawn() {
        _previousState = _state;
        _state = State.STATE_RESPAWNING;
    }

    void FindPlayerManager() {
        if (playerManager == null || playerManager.team != teamNumber)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
    }
}