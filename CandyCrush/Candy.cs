using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour {

    //设置行列索引及偏移量
    public int rowIndex=0;
    public int columnIndex=0;
    public float xOff = -4.5f;
    public float yOff = -3.5f;

    //控制生成糖果的类型
    private int candyTypeNumber = 7;

    //生成糖果材质素材
    public GameObject[] BGs;
    private GameObject bg;

    //指示糖果的颜色
    public int type;

    //生成对GameController的引用
    public GameController gameController;
    private SpriteRenderer sr;
    public bool selected
    {
        //值存在于value中
        set {
            if (transform != null)
                sr.color = value ? Color.gray: Color.white;
                }
    }

    //点击鼠标时，选择点击的糖果
    void OnMouseDown()
    {
       gameController.SelectCandy(this);
    }

    //生成随机颜色的糖果
    private void AddRandomBG()
    {
        //如果糖果已有颜色，就不再生成
        if (bg != null)
            return;
        //随机生成一种颜色的糖果
        type = Random.Range(0, Mathf.Min(candyTypeNumber,BGs.Length));
        bg = (GameObject)Instantiate(BGs[type]);
        sr = bg.GetComponent<SpriteRenderer>();
        bg.transform.parent = this.transform;
    }

    //根据索引值与偏移来更新糖果坐标
    public void UpdatePosition()
    {
        //每次更新坐标，就生成不同颜色的糖果
        AddRandomBG();
        transform.position = new Vector3(columnIndex + xOff, rowIndex + yOff, 0f);
    }

    //实现缓动的效果
    public void ITweenToPosition()
    {
        AddRandomBG();
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "x", columnIndex + xOff,
            "y", rowIndex + yOff,
            "time",0.3f));
    }
    //销毁糖果
    public void DisposeCandy()
    {
        gameController = null;
        Destroy(bg.gameObject);
        Destroy(this.gameObject);
    }
}
