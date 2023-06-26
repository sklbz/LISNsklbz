using System;
using UnityEngine;

public class Boat : MonoBehaviour {

    [SerializeField]
    int teamNumber;
    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    State _state = State.STATE_MOVING, _previousState;
    float speed = 3.0f, firingRate = 2, time;
    Vector3 StartingPosition;
    [SerializeField]
    PlayerManager playerManager;

    void Start () {
        StartingPosition = transform.position;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update() {
        FindPlayerManager();

        if (playerManager.team != teamNumber)
            return;

        switch (_state)
        {
            // handle moving state
            case State.STATE_MOVING:
                Vector3 _velocity = Vector3.zero;

                _velocity.y = Math.Abs(Input.acceleration.y);

                if (_velocity.sqrMagnitude > 1)
                {
                    _velocity.Normalize();
                }

                transform.Translate(speed * Time.deltaTime * _velocity);

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
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
    }
}