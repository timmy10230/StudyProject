using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    private TurretData selectedTurretData;
    private MapCube selectedMapCube;

    public Text moneyText;

    public Animator moneyAnimator;

    private int money = 1000;

    public GameObject upgradeCanvas;

    private Animator upgradeCanvasAnimator;

    public Button buttonUpgrade;

    void ChangeMoney(int change=0)
    {
        money += change;
        moneyText.text = "$" + money;
    }

    void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        if (money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if (mapCube.turretGo != null)
                    {
                        if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapCube = mapCube;
                    }

                }
            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }

    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade=false)
    {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgrade;
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        //upgradeCanvas.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (money >= selectedMapCube.turretData.costUpgraded)
        {
            ChangeMoney(-selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
        }
        else
        {
            moneyAnimator.SetTrigger("Flicker");
        }

        StartCoroutine(HideUpgradeUI());
    }
    public void OnDestroyButtonDown()
    {
        selectedMapCube.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
    }
     
}
