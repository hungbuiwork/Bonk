using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ICS167/UnitStats")]
public class UnitStats : ScriptableObject
{
    //Main objects
    [Header("Objects")]
    [SerializeField]
    public Sprite aliveSprite;
	[SerializeField]
	public Sprite deadSprite;
    [SerializeField]
    public GameObject projectilePrefab;
	
	//Stat values
    [Header("Stats")]
    [SerializeField]
    public float projectileRange;
	[SerializeField]
    public float projectileRate;
	[SerializeField]
    public float projectileSpeed;
	[SerializeField]
    public int projectileDamage;
	[SerializeField]
    public float speed;
	[SerializeField]
    public int maxHealth;
}
