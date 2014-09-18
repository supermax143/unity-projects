using UnityEngine;
using System.Collections;
using Spine;

public class BonesBrakerScript : MonoBehaviour {


    SkeletonAnimation skeletonAnimation;
    public string[] bones;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
       
    }
    public void BreakeBones()
    {

        foreach (string boneName in bones)
            BreakeBone(boneName);
    }

    private void BreakeBone(string boneName)
    {
        Slot slot = skeletonAnimation.skeleton.FindSlot(boneName);
        slot.A = 0;
        RegionAttachment imageRegion = slot.Attachment as RegionAttachment;
        AtlasRegion atlasRegion = imageRegion.RendererObject as AtlasRegion;
        Material material = (Material)atlasRegion.page.rendererObject;
        Rect atlasRect = new Rect(imageRegion.uvs[RegionAttachment.X1] * atlasRegion.page.width, imageRegion.uvs[RegionAttachment.Y1] * atlasRegion.page.height, (imageRegion.uvs[RegionAttachment.X3] - imageRegion.uvs[RegionAttachment.X1]) * atlasRegion.page.width, (imageRegion.uvs[RegionAttachment.Y3] - imageRegion.uvs[RegionAttachment.Y1]) * atlasRegion.page.height);
        Sprite sprite = Sprite.Create(material.mainTexture as Texture2D, atlasRect, new Vector2(0, 0), 1/skeletonAnimation.skeletonDataAsset.scale);
        

        GameObject boneGameObject = new GameObject("bone");

        boneGameObject.AddComponent<SpriteRenderer>();
        boneGameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        boneGameObject.AddComponent<Rigidbody2D>();
        boneGameObject.rigidbody2D.gravityScale = 8;


        boneGameObject.AddComponent<BoxCollider2D>();
        boneGameObject.layer = LayerMask.NameToLayer(LayerEnum.Void);
        
        Vector3 axePosition = new Vector3(skeletonAnimation.transform.position.x + slot.bone.worldX, skeletonAnimation.transform.position.y + slot.bone.worldY);
        boneGameObject.transform.position = axePosition;
        boneGameObject.rigidbody2D.AddTorque(-200);
        boneGameObject.rigidbody2D.AddForce(new Vector2(Random.Range(-100, 100), 1000));
    }


}
