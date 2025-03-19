using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterActivation : MonoBehaviour
{
    private Animator animator;

    private bool LeftClicked = false;

    // Start is called before the first frame update
    public delegate void LighterEvent(); 
    public static event LighterEvent OnLighterActivated; 
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !LeftClicked)
        {
            animator.SetBool("LeftClicked", true);
            LeftClicked = true; 

            OnLighterActivated?.Invoke();
            
            StartCoroutine(ResetLeftClicked());
        }
    }
    private IEnumerator ResetLeftClicked()
    {
        yield return new WaitForSeconds(0.5f); 

        animator.SetBool("LeftClicked", false); 

        yield return new WaitForSeconds(2f);

        LeftClicked = false;

    }
}
