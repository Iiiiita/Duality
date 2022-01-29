//Jyri Tero 2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 5;
    public float maxStamina = 10;
    public float staminaDrainedPerSec = 1;
    public float staminaGainedPerSec = 1;
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
    private bool isDrainCoRunning = false;
    private float currentStamina;
    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        StartCoroutine(DeadOut());
        isInLiving = true;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        if (Input.GetKey(KeyCode.Q))
        {
            if (isInLiving)
            {
                StartCoroutine(DeadIn());
                StartCoroutine(LivingOut());

            }
            if (!isInLiving)
            {
                StartCoroutine(LivingIn());
                StartCoroutine(DeadOut());

            }
        }

        if (isInLiving)
        {

            DrainStaminaCo = StartCoroutine(DrainStamina());
        }

        if (Input.GetKey(KeyCode.K))
        {
            StartCoroutine(DeadIn());
            StartCoroutine(LivingOut());
        }
        if (Input.GetKey(KeyCode.L))
        {
            StartCoroutine(LivingIn());
            StartCoroutine(DeadOut());
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
        if (isDrainCoRunning)
        {
            print("Stopping drain");
            StopCoroutine(DrainStaminaCo);
        }
        
        StartCoroutine(GainStamina());
    }

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

    IEnumerator GainStamina()
    {
        print("Starting gain");
        if (currentStamina < maxStamina)
        {
            currentStamina = currentStamina + (staminaGainedPerSec * Time.deltaTime);
            //Debug.Log("Stamina+: " + currentStamina);
            yield return new WaitForSeconds(1);
        }
        else
            yield return null;
    }

    IEnumerator DrainStamina()
    {
        isDrainCoRunning = true;
        if (currentStamina <= 0)
        {
            print("Ran out of Stm");
            isDrainCoRunning = false;
            StartCoroutine(DeadIn());
            StartCoroutine(LivingOut());
            yield return null;
        }
        else
        {
            while (currentStamina > 0)
            {
                currentStamina = currentStamina - (staminaDrainedPerSec * Time.deltaTime);
                //Debug.Log("Stamina-: " + currentStamina);
                yield return new WaitForSeconds(1);

            }
        }
    }
}
