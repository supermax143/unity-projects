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
        Sprite sprite = Sprite.Create(material.mainTexture as Texture2D, atlasRect, new Vector2(0, 0), 20);


        GameObject spriteGameObject = new GameObject("bone");

        spriteGameObject.AddComponent<SpriteRenderer>();
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        spriteGameObject.AddComponent<Rigidbody2D>();
        spriteGameObject.rigidbody2D.gravityScale = 8;

        spriteGameObject.AddComponent<BoxCollider2D>();
        spriteGameObject.layer = LayerMask.NameToLayer(LayerEnum.Void);

        Vector3 axePosition = new Vector3(skeletonAnimation.transform.position.x + slot.bone.worldX, skeletonAnimation.transform.position.y + slot.bone.worldY);
        spriteGameObject.transform.position = axePosition;
        spriteGameObject.rigidbody2D.AddTorque(-200);
        spriteGameObject.rigidbody2D.AddForce(new Vector2(Random.Range(-100, 100), 1000));
    }


}
