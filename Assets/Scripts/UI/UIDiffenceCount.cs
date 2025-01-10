using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIDiffenceCount : MonoBehaviour
{
    [SerializeField] private GameObject diffenceMechanism;
    public DiffenceManagement[] diffenceManagement;

    [System.Serializable]
    public class DiffenceManagement
    {
        public GameObject diffenceMachine;
        public int diffenceIndex;
        public TextMeshProUGUI diffenceNum;
        public int maxCount;
        public int currentCount;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(var diffence in diffenceManagement)
        {
            diffenceManagement[diffence.diffenceIndex].diffenceNum = diffenceManagement[diffence.diffenceIndex].diffenceMachine.GetComponentInChildren<TextMeshProUGUI>();
            diffenceManagement[diffence.diffenceIndex].maxCount = diffenceMechanism.GetComponent<ObjectSpawner>().placementLimits[diffenceManagement[diffence.diffenceIndex].diffenceIndex].maxCount;
            diffenceManagement[diffence.diffenceIndex].diffenceNum.text = diffenceManagement[diffence.diffenceIndex].maxCount.ToString();
            diffenceManagement[diffence.diffenceIndex].diffenceMachine.transform.GetChild(3).gameObject.SetActive(false);
        }
        
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    public void CheckDiffenceNum(int index)
    {
        foreach(var diffence in diffenceManagement)
        {
            if (index == diffenceManagement[diffence.diffenceIndex].diffenceIndex)
            {
                diffenceManagement[diffence.diffenceIndex].currentCount = diffenceMechanism.GetComponent<ObjectSpawner>().placementLimits[diffenceManagement[diffence.diffenceIndex].diffenceIndex].currentCount;
                int num = diffenceManagement[diffence.diffenceIndex].maxCount - diffenceManagement[diffence.diffenceIndex].currentCount;
                diffenceManagement[diffence.diffenceIndex].diffenceNum.text = num.ToString();
                if (num == 0)
                {
                    diffenceManagement[diffence.diffenceIndex].diffenceMachine.transform.GetChild(3).gameObject.SetActive(true);
                }
            }

            
        }
        
        
    }
}
