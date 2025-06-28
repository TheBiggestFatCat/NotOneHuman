using UnityEngine;
using UnityEngine.UI;

public class SkillCD : MonoBehaviour
{
    public int PlayerIndex;
    public Image SkillImage;
    public Actor PlayerActor;
    void Update()
    {
        Actor actor = GameManager.Instance.GetActor(PlayerIndex);
        if (actor != null)
        {
            actor.UpdateAttackCD.AddListener(SetFillAmount);
        }
        else
        {
            return;
        }   
    }
    public void SetFillAmount(float fillAmount)
    {
        if (SkillImage != null)
        {
            SkillImage.fillAmount = 1 - fillAmount;
        }
    }
}