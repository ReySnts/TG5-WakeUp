using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth objInstance = null;
    GameObject bloodObject = null;
    Image bloodImage = null;
    Animator bloodAnimator = null;
    readonly string bloodObjectName = "Blood";
    public float currentHealth = 0f;
    public float minimumHealth = 0f;
    float maximumHealth = 100f;
    float regenAmount = 5f;
    public bool isDie = false;
    bool isRegen = false;
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        bloodObject = GameObject.Find(bloodObjectName);
        bloodImage = bloodObject.GetComponent<Image>();
        bloodAnimator = bloodObject.GetComponent<Animator>();
        currentHealth = maximumHealth;
    }
    IEnumerator HoldRegen()
    {
        yield return new WaitForSeconds(1f);
        currentHealth += regenAmount;
        if 
        (
            Player.objInstance.isCollidingEnemy
            ||
            Player.objInstance.isTriggeringEnemyGhost
        ) 
        currentHealth -= regenAmount;
        isRegen = false;
    }
    IEnumerator HoldRestart()
    {
        yield return new WaitForSeconds(2f);
        Scene.objInstance.Restart();
    }
    void Update()
    {
        bloodAnimator.SetFloat
        (
            "currentHealth",
            currentHealth = Mathf.Clamp
            (
                currentHealth,
                minimumHealth,
                maximumHealth
            )
        );
        if
        (
            !isDie
            &&
            currentHealth == minimumHealth
        )
        {
            isDie = true;
            PlayerHide.objInstance.success?.Invoke();
            #region Play Game Over Sound
            SoundManager.objInstance.Crystal_get.enabled = false;
            SoundManager.objInstance.walk.enabled = false;
            SoundManager.objInstance.run.enabled = false;
            SoundManager.objInstance.Damaged.enabled = false;
            SoundManager.objInstance.Explode_sound.enabled = false;
            SoundManager.objInstance.Game_over.Play();
            #endregion
            StartCoroutine
            (
                HoldRestart()
            );
        }
        bloodAnimator.SetBool
        (
            "isDie",
            isDie
        );
        if (!isRegen) 
        {
            isRegen = true;
            StartCoroutine
            (
                HoldRegen()
            );
        }
        bloodAnimator.SetBool
        (
            "isWaitingToHit",
            Enemy.isWaitingToHit
        );
    }
}