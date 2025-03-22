using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Itens/Weapon", order = 1)]
public class WeaponSO : EquipableItemSO
{
    private const string WEAPON_NAME = "Weapon";

    [SerializeField] AnimatorOverrideController _animatorOverride = null;
    [SerializeField] Weapon _equippedPrefab = null;
    [SerializeField] public float _weaponRange = 2f;
    [SerializeField] public float _weaponDamage = 2f;
    [SerializeField] public float _percentageBonus = 0;
    [SerializeField] private float _maxLifeTime = 10f;
    [SerializeField] private bool _isRightHanded = true;

    public Weapon Spawn(Transform rightHand, Transform leftHand, Animator anim)
    {
        DestroyOldWeapon(rightHand, leftHand);
        Weapon weapon = null;
        if (_equippedPrefab != null)
        {
            weapon = Instantiate(_equippedPrefab, GetTransform(rightHand, leftHand));
            weapon.gameObject.name = WEAPON_NAME;
        }

        AnimatorOverrideController overrideController = anim.runtimeAnimatorController as AnimatorOverrideController;

        if (_animatorOverride != null)
        {
            anim.runtimeAnimatorController = _animatorOverride;
        }
        else if (overrideController != null)
        {
            anim.runtimeAnimatorController = overrideController;
        }

        return weapon;
    }

    private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
    {
        Transform oldWeapon = rightHand.Find(WEAPON_NAME);
        if (oldWeapon == null)
        {
            oldWeapon = leftHand.Find(WEAPON_NAME);
        }
        if (oldWeapon == null) 
        { 
            return; 
        }
        oldWeapon.name = "DESTROYING";
        Destroy(oldWeapon.gameObject);
    }

    private Transform GetTransform(Transform rightHand, Transform _leftHand)
    {
        Transform handTransform;
        if (_isRightHanded == true)
        {
            handTransform = rightHand;
        }
        else 
        { 
            handTransform = _leftHand; 
        }
        return handTransform;
    }
}
