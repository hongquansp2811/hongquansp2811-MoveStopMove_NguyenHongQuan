using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private Transform posHair;
    [SerializeField] private Transform posShield;
    [SerializeField] private Transform tailPos;
    [SerializeField] private Transform wingPos;
    [SerializeField] private SkinnedMeshRenderer PantsMesh;
    [SerializeField] private SkinnedMeshRenderer fullMesh;

    private HairSkinObject skinHairPrefab;
    private ShieldSkinObject skinShieldPrefab;
    public Material curentFullMesh;

    public void ChangeSkinHair(int ID)
    {
        HairSkinObject newSkinHair = LevelManager.Ins.shopSkinData.GetHairSkinObjectBuyID(ID);
        skinHairPrefab = newSkinHair;
        ChangeItem(skinHairPrefab, posHair);
    }

    public void ChangeSkinShield(int ID)
    {
        ShieldSkinObject newShield = LevelManager.Ins.shopSkinData.GetShieldSkinObjectBuyID(ID);
        skinShieldPrefab = newShield;
        ChangeItem(skinShieldPrefab, posShield);
    }

    public void ChangePant(Material material)
    {
        PantsMesh.material = material;
    }

    public void ChangeFullMesh(Material material)
    {
        fullMesh.material = material;
    }

    public void ChangeSetFull(int ID)
    {
        SetFullObject newSet = LevelManager.Ins.dataSetFull.GetSetFullObjectBuyID(ID);
        ChangeItemOffSetFull(newSet.moduleHair, posHair, ref skinHairPrefab);
        ChangeItemOffSetFull(newSet.moduleShield, posShield, ref skinShieldPrefab);

        ChangeItem(newSet.moduleTail, tailPos);
        ChangeItem(newSet.moduleWing, wingPos);
        if (newSet.paintMesh != null) 
        {
            ChangePant(newSet.paintMesh);
        }
        if (newSet.fullMesh != null) ChangeFullMesh(newSet.fullMesh);
    }

    public void ChangeItemOffSetFull<T>(T newItem, Transform parentTransform, ref T currentItem) where T : MonoBehaviour
    {
        if (newItem != null)
        {
            ChangeItem(newItem, parentTransform);
            currentItem = newItem;
        }
        else
        {
            DelCurrentItems(parentTransform);
            currentItem = null;
        }
    }

    public void ChangeItem<T>(T newItem, Transform parentTransform) where T : MonoBehaviour
    {
        if (parentTransform != null && newItem != null)
        {
            DelCurrentItems(parentTransform);
            T itemInstance = Instantiate(newItem, parentTransform.position, parentTransform.rotation);
            itemInstance.transform.SetParent(parentTransform);
        }
    }

    public void DelCurrentItems(Transform parentTransform)
    {
        if (parentTransform != null)
        {
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
