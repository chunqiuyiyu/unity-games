using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int rowNumber = 7;
    public int columnNumber = 10;
    public Candy candy;
    public GameObject candyParent;

    //当前选中的糖果
    private Candy currentCandy;
    private ArrayList candyArrayList;
    //获取匹配数组
    private ArrayList matchesArrayList;

    //音效获取
    public AudioClip mathThreeClip;
    public AudioClip SwapClip;
    public AudioClip explosionClip;
    public AudioClip SwapWrog;

    public Text score;
    public Timer timer;
    public int allScore = 0;

    void Start () {
        //建立一个二维数组，存储糖果的位置
        candyArrayList = new ArrayList();
        //生成网格状的糖果
        for (int rowIndex = 0; rowIndex < rowNumber; rowIndex++)
        {
            ArrayList temp = new ArrayList();
            for (int columnIndex = 0; columnIndex < columnNumber; columnIndex++)
            {
                Candy c = AddCandy(rowIndex, columnIndex);
                //更新坐标
                c.UpdatePosition();
                //将当前生成的糖果加入到临时数组中
                temp.Add(c);
            }
            candyArrayList.Add(temp);
        }
        //如果刚开始有一起的糖果，进行消除
        if (CheckMatches())
        {
            RemoveMatches();
        }
    }
    //生成糖果
    private Candy AddCandy(int rowIndex, int columnIndex)
    {
        //实例化预置件是对象类型
        object o = Instantiate(candy);
        //将这转换为游戏对象类型
        Candy c = o as Candy;
        //设置其父容器为GameController
        c.transform.parent = candyParent.transform;
        c.rowIndex = rowIndex;
        c.columnIndex = columnIndex;
        c.gameController = this;
        return c;
    }
    //根据行列索引得到糖果对象
    private Candy GetCandy(int rowIndex,int columnIndex)
    {
        ArrayList temp = candyArrayList[rowIndex]as ArrayList;
        Candy c = temp[columnIndex] as Candy;
        return c;
    }

    //根据行列索引设置糖果的位置
    private void SetCandyPosition(int rowIndex,int columnIndex,Candy c)
    {
        ArrayList temp = candyArrayList[rowIndex] as ArrayList;
        temp[columnIndex] = c;
    }
    //选中糖果
    public void SelectCandy(Candy c)
    {
        //RemoveCandy(c);return;
        if (currentCandy == null)
        {
            //第一次点击，将其存入当前糖果中
            currentCandy = c;
            currentCandy.selected = true;
        }
        else
        {
            //如果点击的是相邻的糖果，则进行交换
            if (Mathf.Abs(c.rowIndex-currentCandy.rowIndex)
                +Mathf.Abs(c.columnIndex-currentCandy.columnIndex)==1)
            {
                StartCoroutine(ExchangeCandy2(currentCandy,c));
            }
            //置空第一次选中的对象
            currentCandy.selected = false;
            currentCandy = null;
        }
    }

    //协程处理延时逻辑
    IEnumerator ExchangeCandy2(Candy c1,Candy c2)
    {
        //进行交换，完成一轮游戏
        ExchangeCandy(c1, c2);
        //等待一段时间
        yield return new WaitForSeconds(0.4f);
        //如果有匹配，则发生交换
        if (CheckMatches() )
        {
            RemoveMatches();
        }
        //否则返回原来位置
        else {
            //播放交换错误音效
            GetComponent<AudioSource>().PlayOneShot(SwapWrog);
            Debug.Log("wrong!");
            ExchangeCandy(c1, c2);
        }
     }

    public void ExchangeCandy(Candy c1,Candy c2)
    {
        //播放交换音效
        GetComponent<AudioSource>().PlayOneShot(SwapClip);
        SetCandyPosition(c1.rowIndex, c1.columnIndex, c2);
        SetCandyPosition(c2.rowIndex, c2.columnIndex, c1);
        //交换两个糖果的位置
        int rowIndex = c1.rowIndex;
        c1.rowIndex = c2.rowIndex;
        c2.rowIndex = rowIndex;

        int columnIndex = c1.columnIndex;
        c1.columnIndex = c2.columnIndex;
        c2.columnIndex = columnIndex;

        //c1.UpdatePosition();
        //c2.UpdatePosition();
        //实现动画效果
        c1.ITweenToPosition();
        c2.ITweenToPosition();
    }
    //添加爆炸效果
    private void AddEffect(Vector3 position)
    {
        Instantiate(Resources.Load("Prefabs/Explosion2"), position, Quaternion.identity);
        //添加摄像头摇动范围
        CameraShake.shakeFor(0.1f, 0.1f);
    }
    //调用销毁方法删除糖果
    private void RemoveCandy(Candy c)
    {
        AddEffect(c.transform.position);
        //播放爆炸音效
        GetComponent<AudioSource>().PlayOneShot(explosionClip);
        //移除自己
        c.DisposeCandy();
        //得到被移除糖果上面的糖果
        int columnIndex = c.columnIndex;
        for (int rowIndex = c.rowIndex + 1; rowIndex < rowNumber; rowIndex++)
        {
            Candy c1 = GetCandy(rowIndex, columnIndex);
            //往下移一位
            c1.rowIndex--;
            //c1.UpdatePosition();
            c1.ITweenToPosition();
            //保存其位置
            SetCandyPosition(rowIndex-1, columnIndex, c1);
        }
        //在最上端生成一个新的糖果
        Candy newCandy = AddCandy(rowNumber - 1, columnIndex);
        newCandy.rowIndex = rowNumber;
        newCandy.UpdatePosition();
        newCandy.rowIndex--;
        newCandy.ITweenToPosition();
        SetCandyPosition(rowNumber - 1, columnIndex, newCandy);
    }

    //返回布尔值，显示是否能够交换
    private bool CheckMatches()
    {
        return CheckHotizontalMatches() || ChecVerticalMatches();
    }

    //检查水平方向是否有可以消除的
    private bool CheckHotizontalMatches()
    {
       
        bool result = false;
        for (int rowIndex = 0; rowIndex < rowNumber; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columnNumber - 2; columnIndex++)
            {
                if ((GetCandy(rowIndex, columnIndex).type == GetCandy(rowIndex, columnIndex + 1).type) &&
                    (GetCandy(rowIndex, columnIndex + 1).type == GetCandy(rowIndex, columnIndex + 2).type))
                {
                    //播放匹配音效
                    GetComponent<AudioSource>().PlayOneShot(mathThreeClip);
                    result = true;
                    Debug.Log(columnIndex + "" + columnIndex + 1 + "" + columnIndex + 2);
                    AddMatches(GetCandy(rowIndex, columnIndex));
                    AddMatches(GetCandy(rowIndex, columnIndex + 1));
                    AddMatches(GetCandy(rowIndex, columnIndex + 2));
                }
            }
        }

        return result;
    }

    //检查垂直是否有可以消除的
    private bool ChecVerticalMatches()
    {
        bool result = false;
        for (int columnIndex = 0; columnIndex < columnNumber; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < rowNumber - 2; rowIndex++)
            {
                if ((GetCandy(rowIndex, columnIndex).type == GetCandy(rowIndex+1, columnIndex).type) &&
                    (GetCandy(rowIndex+1, columnIndex).type == GetCandy(rowIndex+2, columnIndex).type))
                {

                    //播放匹配音效
                    GetComponent<AudioSource>().PlayOneShot(mathThreeClip);
                    result = true;
                    AddMatches(GetCandy(rowIndex, columnIndex));
                    AddMatches(GetCandy(rowIndex+1, columnIndex));
                    AddMatches(GetCandy(rowIndex+2, columnIndex));
                }
            }
        }

        return result;
    }

    //生成匹配数组
    private void AddMatches(Candy c)
    {
        if (matchesArrayList == null)
        {
            //第一次进行创建
            matchesArrayList = new ArrayList();
        }
        int index = matchesArrayList.IndexOf(c);
        //如果当前对象不在匹配数组中，就将之添加进去
        if (index == -1)
        {
            matchesArrayList.Add(c);
        }
    }

    //移除匹配数组中的元素
    private void RemoveMatches()
    {        
        Candy temp;
        for (int index = 0; index < matchesArrayList.Count; index++)
        {
            temp = matchesArrayList[index] as Candy;
            RemoveCandy(temp);
        }

        //得到分数
        GetScore(matchesArrayList);

        matchesArrayList = new ArrayList();
        StartCoroutine(WaitAndCheck());


    }

    //等待一会儿后进行检查
    IEnumerator WaitAndCheck()
    {
        yield return new WaitForSeconds(0.5f);
        //如果消除完成后还有一起的糖果，消除
        if (CheckMatches())
        {
            RemoveMatches();
        }
    }

    //得到分数
    private void GetScore(ArrayList al){

        timer.Reset();
        allScore += 100 * al.Count;
        score.text = "SCORE : " + allScore;
    }
}
