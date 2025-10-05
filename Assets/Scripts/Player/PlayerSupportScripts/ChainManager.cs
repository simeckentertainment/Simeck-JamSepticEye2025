using UnityEngine;

public class ChainManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject chainLinkObj;
    [SerializeField] Player player;
    [SerializeField] GrapplerTargetingMachine GTM;
    [SerializeField] public int HowManyAreNeededThisFrame;
    [SerializeField] public Vector2 launchPoint;
    [SerializeField] GameObject[] ChainLinks;
    void OnEnable()
    {
        //First find out how many of these fuckers we need.
        //HowManyAreNeededThisFrame = (int)Vector2.Distance(player.transform.position, GTM.WorldSpaceGrappleTarget);

    }
    void FixedUpdate()
    {
        if (ChainLinks != null)
        {
            foreach (GameObject link in ChainLinks)
            {
                Destroy(link);
            }
        }
        HowManyAreNeededThisFrame = (int)Vector2.Distance(player.transform.position, player.HookOBJ.transform.position);
        ChainLinks = new GameObject[HowManyAreNeededThisFrame];
        for (int i = 0; i < ChainLinks.Length; i++)
        {
            float ThisLinkLerpValue = (float)i / (float)HowManyAreNeededThisFrame;
            Vector2 ThisLinkPos = Vector2.Lerp(player.transform.position, player.HookOBJ.transform.position, ThisLinkLerpValue);
            GameObject ThisLink = Instantiate(chainLinkObj, gameObject.transform);
            ThisLink.transform.position = ThisLinkPos;
            ThisLink.transform.up = ((Vector2)player.HookOBJ.transform.position - (Vector2)player.transform.position).normalized;
            ChainLinks[i] = ThisLink;
        }



    }
}
