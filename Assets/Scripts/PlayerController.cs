//Jyri Tero 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 5;
    public float maxStamina = 10;
    public float staminaDrainSpeed = 1;
    public float staminaGainSpeed = 1;
    public Interactions interactable;

    [Header("Fade Settings")]
    public float fadeSpeed = 1f;
    public float fadeWaitSec = 1f;
    public GameObject LivingParent;
    public GameObject DeadParent;
    public Material LivingOpaqueMat;
    public Material DeadOpaqueMat;
    public Material LivingTransparentMat;
    public Material DeadTransparentMat;
    public GameObject LivingGround;
    public GameObject DeadGround;

    private bool isInLiving;
    private Coroutine DrainStaminaCo;
    private Coroutine GainStaminaCo;
    private bool isDrainCoRunning = false;
    private bool isGainCoRunning;
    private float currentStamina;

    private bool hasDoorCollision;
    private bool hasPaperCollision;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        LivingGround.SetActive(false);

        StartCoroutine(LivingOut());
        GainStaminaCo = StartCoroutine(GainStamina());
        isInLiving = false;
        interactable = new Interactions();
    }

    // Update is called once per frame
    void Update()
    {

        Moving();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isInLiving)
            {
                StartCoroutine(DeadIn());
                StartCoroutine(LivingOut());

                isDrainCoRunning = false;
                isGainCoRunning = true;

                Debug.Log("About to stop drain");
                StopCoroutine(DrainStaminaCo);
                Debug.Log("About to gain");
                GainStaminaCo = StartCoroutine(GainStamina());

            }
            if (!isInLiving)
            {
                StartCoroutine(LivingIn());
                StartCoroutine(DeadOut());

                isDrainCoRunning = true;
                isGainCoRunning = false;

                StopCoroutine(GainStaminaCo);
                DrainStaminaCo = StartCoroutine(DrainStamina());

            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Tarkista tagi, kutsu sen mukaista funktiota, asuvat interactable
            if (hasDoorCollision)
            {
                interactable.DoorInteraction();
            }

            if (hasPaperCollision)
            {
                interactable.PaperInteraction();
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("OfficeDoor"))
        {
            //Kun kolaroidaan oveen
            Debug.Log("You enter doors sphere of influence...");
            hasDoorCollision = true;
            
            
        }
        if (collision.gameObject.CompareTag("WillPaper"))
        {
            // Kun kolaroidaan testamenttiin.
            Debug.Log("You enter papers sphere of influence...");

            hasPaperCollision = true;

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "OfficeDoor")
        {
            //Kun poistutaan ovelta
            Debug.Log("You leave doors sphere of influence...");
            hasDoorCollision = false;

        }
        if (collision.gameObject.tag == "WillPaper")
        {
            // Kun poistutaan testamentiltä.
            Debug.Log("You leave papers sphere of influence...");
            hasPaperCollision = false;

        }
    }

    //Fade the Dead in
    IEnumerator DeadIn()
    {

        DeadGround.SetActive(true);
        LivingGround.SetActive(false);

        DeadParent.SetActive(true);

        var renderers = DeadParent.GetComponentsInChildren(typeof(Renderer));

        Renderer r = (Renderer)renderers[0];
        Color objColor = r.material.color;

        while (objColor.a < 1)
        {
            foreach (Renderer rend in renderers)
            {
                objColor = rend.material.color;
                float fadeAmmount = objColor.a + (fadeSpeed * Time.deltaTime);

                objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmmount);
                rend.material.color = objColor;
            }
            yield return new WaitForSeconds(fadeWaitSec);
        }


        //change the material to opaque for shadows
        foreach (Renderer rend in renderers)
        {
            MatRendererChange.ChangeRenderMode(rend.material, MatRendererChange.BlendMode.Opaque);
        }

    }
    //Fade the living out
    IEnumerator LivingOut()
    {


        var renderers = LivingParent.GetComponentsInChildren(typeof(Renderer));

        //change the material to opaque for alpha
        foreach (Renderer rend in renderers)
        {
            MatRendererChange.ChangeRenderMode(rend.material, MatRendererChange.BlendMode.Fade);
        }

        Renderer r = (Renderer)renderers[0];
        Color objColor = r.material.color;
        r = (Renderer)renderers[0];
        objColor = r.material.color;

        while (objColor.a > 0)
        {
            foreach (Renderer rend in renderers)
            {
                objColor = rend.material.color;
                float fadeAmmount = objColor.a - (fadeSpeed * Time.deltaTime);

                objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmmount);
                rend.material.color = objColor;
            }
            yield return new WaitForSeconds(fadeWaitSec);
        }
        LivingParent.SetActive(false);
        isInLiving = false;
    }
    /*    if (!isDrainCoRunning)
    //    {
    //        Debug.Log("STOPPP drain");
    //        StopCoroutine(DrainStaminaCo);
    //    }

    //    GainStaminaCo = StartCoroutine(GainStamina());*/

    //Fade the Living in
    IEnumerator LivingIn()
    {

        DeadGround.SetActive(false);
        LivingGround.SetActive(true);

        LivingParent.SetActive(true);

        var renderers = LivingParent.GetComponentsInChildren(typeof(Renderer));

        Renderer r = (Renderer)renderers[0];
        Color objColor = r.material.color;

        while (objColor.a < 1)
        {
            foreach (Renderer rend in renderers)
            {
                objColor = rend.material.color;
                float fadeAmmount = objColor.a + (fadeSpeed * Time.deltaTime);

                objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmmount);
                rend.material.color = objColor;
            }
            yield return new WaitForSeconds(fadeWaitSec);

        }
        //change the material to opaque for shadows
        foreach (Renderer rend in renderers)
        {
            MatRendererChange.ChangeRenderMode(rend.material, MatRendererChange.BlendMode.Opaque);
        }
    }

    //Fade the dead out
    IEnumerator DeadOut()
    {

        var renderers = DeadParent.GetComponentsInChildren(typeof(Renderer));

        //change the material to opaque for alpha
        foreach (Renderer rend in renderers)
        {
            MatRendererChange.ChangeRenderMode(rend.material, MatRendererChange.BlendMode.Fade);
        }

        Renderer r = (Renderer)renderers[0];
        Color objColor = r.material.color;
        r = (Renderer)renderers[0];
        objColor = r.material.color;

        while (objColor.a > 0)
        {
            foreach (Renderer rend in renderers)
            {
                objColor = rend.material.color;
                float fadeAmmount = objColor.a - (fadeSpeed * Time.deltaTime);

                objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmmount);
                rend.material.color = objColor;
            }
            yield return new WaitForSeconds(fadeWaitSec);
        }
        DeadParent.SetActive(false);
        isInLiving = true;
    }
    void Moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }
    }
    IEnumerator DrainStamina()
    {

        while (currentStamina > 0)
        {
            if (isDrainCoRunning)
            {
                currentStamina = currentStamina - (staminaDrainSpeed * Time.deltaTime);

                if (currentStamina <= 0)
                {

                    Debug.Log("RAN OUT of Stm");
                    currentStamina = 0;

                    StartCoroutine(DeadIn());
                    StartCoroutine(LivingOut());

                    isDrainCoRunning = false;
                    isGainCoRunning = true;
                    StopCoroutine(DrainStaminaCo);
                    GainStaminaCo = StartCoroutine(GainStamina());
                    yield return null;

                    //break;
                }
                Debug.Log("-Stamina-: " + currentStamina);
                yield return new WaitForSeconds(1);
            }
        }
        yield return null;

    }

    IEnumerator GainStamina()
    {
        Debug.Log("Starting Gain");
        if (currentStamina >= maxStamina)
        {
            Debug.Log("MaxStm");
            currentStamina = maxStamina;
            //  StopCoroutine(GainStaminaCo);
            // isGainCoRunning = false;
            yield return null;
        }
        else if (currentStamina < maxStamina)
        {
            yield return new WaitForSeconds(5);
            currentStamina = maxStamina;
            //currentStamina = currentStamina + (staminaGainSpeed * Time.deltaTime);
            Debug.Log("+Stamina+: " + currentStamina);
        }
        else
            Debug.Log("hajos lol xd");

    }

    /*   Vanha DrainStamina
     *   isDrainCoRunning = true;
    //    if (currentStamina <= 0)
    //    {
    //        Debug.Log("RAN OUT of Stm");
    //        StopCoroutine(DrainStaminaCo);
    //        isDrainCoRunning = false;
    //        StartCoroutine(DeadIn());
    //        StartCoroutine(LivingOut());
    //        yield return null;
    //    }
    //    else
    //    {
    //        while (currentStamina > 0)
    //        {
    //            currentStamina = currentStamina - (staminaDrainSpeed * Time.deltaTime);
    //            Debug.Log("Stamina-: " + currentStamina);
    //            yield return new WaitForSeconds(1);

    //        }
          }*/

    /* Vanha GainStamina
     * Debug.Log("Starting gain");
    //    if (currentStamina >= maxStamina)
    //    {
    //        currentStamina = maxStamina;
    //        StopCoroutine(GainStaminaCo);
    //        yield return null;
    //    }
    //    else
    //    {
    //        while (currentStamina < maxStamina)
    //        {
    //            currentStamina = currentStamina + (staminaGainSpeed * Time.deltaTime);
    //            Debug.Log("Stamina+: " + currentStamina);
    //            yield return new WaitForSeconds(1);
    //        }
         }*/

}
