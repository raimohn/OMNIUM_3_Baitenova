using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int scoreCost;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;

    public float DefaultSpeed => speed;
    public int ScoreCost => scoreCost;
    public float TimeBetweenAttacks => timeBetweenAttacks;
    public Transform CharacterTransform => characterTransform;

    public CharacterController CharacterController => characterController;
}
