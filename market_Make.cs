using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeMarket : MonoBehaviour
{
    public Sprite[] theSprites;//br:1~9, ori:10~18
    //---------------break-----------------
    //01 02 03
    //04 05 06
    //07 08 09
    //---------------break-----------------
    //----------------new------------------
    //10 11 12
    //13 14 15
    //16 17 18
    //----------------new------------------
    public float marketWidth;
    public string marketName;
    public float twoStoreDistance;//0.5店家背靠背距離
    public float streatMinWidth;//3.0
    public float streatMaxWidth;//4.0
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject theMarket = makeTheMarket();
        //Instantiate(theMarket);
        theMarket.transform.parent = transform;
        theMarket.transform.localPosition = new Vector3(0, 0, 0);
    }
    GameObject makeTheMarket(){
        GameObject market = new GameObject(marketName);
        float _z = 0.0f;
        int _lastCount = 0;
        int i = 0;
        while (!(_z >= marketWidth && i % 2 == 1)){
            float finalWidth = 0.0f;
            GameObject streat = makeStoreStreat(_z,ref _lastCount, ref finalWidth);//finalwidth至中用
            streat.transform.parent = market.transform;
            streat.transform.localPosition = new Vector3(-finalWidth / 2.0f, 0.0f, _z);
            if (i % 2 == 0){
                _z += Random.Range(twoStoreDistance-0.1f,twoStoreDistance+0.1f);//商店背靠背
            }else{
                _z += Random.Range(streatMinWidth,streatMaxWidth);//街道寬
            }
            i++;
        }
        return market;
    }
    
    GameObject makeStoreStreat(float _z,ref int _lastCount,ref float storeX){//_z計算商店位置對應等級用,_lastCount 讓街道盡量呈現梯形，短邊朝中央水池, storeX最後回傳直可以用在街道至中
    
        GameObject streat = new GameObject($"StoreStreat{_z.ToString()}");
        
        int storeCount = Random.Range(_lastCount, 20);
        _lastCount = storeCount;
        for (int i = 0; i < storeCount; i++)
        {
            storeX += Random.Range(0.0f,1.5f);
            float thisLocationX = storeX;
            GameObject store = makeStore(ref storeX,_z,i);
            store.transform.parent = streat.transform;
            store.transform.localPosition = new Vector3(thisLocationX,0.0f,0.0f);
        }
        
        return streat;
    }
    GameObject makeStore(ref float _x, float _z,int i){//計算商店位置對應等級用(未完成)
        float level = Mathf.Floor(Random.Range(2.0f,8.0f)); //StoreLevel(_x,_z);
        GameObject store = new GameObject($"Store-{i.ToString()}-{level.ToString()}");

        float storeWidth = Mathf.Floor(Random.Range(level,level+3.0f));//small-store to large-store
        float storeHeight = 3.0f;//Random.Range(3.0f, 7.0f);目前的sprites無法拆高
        float oldOrNewRnd = (7.0f - level) / 7.0f;//計算區域新舊機率,小於是old,大於是new，先簡單計算，還須調整
        Color roofColor = new Color(1.0f,1.0f,1.0f);//屋頂顏色(等級+亂數 之後再算)
        Color woodColor = new Color(1.0f,1.0f,1.0f);//攤販架子顏色(等級+亂數 之後再算)
        for (float y = 0.0f; y <storeHeight;y += 1.0f){
            
            for (float x = 0.0f; x < storeWidth; x += 1.0f){
                GameObject vendor = new GameObject($"vendor({x.ToString()},{y.ToString()})");
                vendor.transform.parent = store.transform;
                vendor.AddComponent<SpriteRenderer>();
                int spriteIndex = (Random.Range(0.0f ,1.0f) > oldOrNewRnd) ? 9 : 0;
                SpriteRenderer vendorRenderer = vendor.GetComponent<SpriteRenderer>();
                vendorRenderer.sprite = theSprites[RandomSpriteIndex(oldOrNewRnd,x,y,storeWidth,storeHeight)];
                if (y == 0.0f){
                    vendorRenderer.color = roofColor;
                }else{
                    vendorRenderer.color = woodColor;
                }
                vendor.transform.localPosition = new Vector3(storeWidth-x-1.0f,storeHeight-y-1.0f,0.0f);
                vendor.transform.localRotation = new Quaternion(0,90,0,0);
            }
        }
        _x += storeWidth;
        return store;
    }
    int RandomSpriteIndex(float oldOrNewRnd,float x,float y,float storeWidth,float storeHeight){
        int spriteIndex = (Random.Range(0.0f ,1.0f) > oldOrNewRnd) ? 9 : 0;
        if (y == 0.0f){
            spriteIndex += Random.Range(0 ,3);
        }else{
            if (x == 0){
                spriteIndex += 3;
            }else if(x == storeWidth-1.0){
                spriteIndex += 5;
            }else{
                spriteIndex += 4;
            }
        }
        if(y == storeHeight-1.0){
            spriteIndex += 3;
        }
        return spriteIndex;
    }
    //--------------------------------StoreLevel--------------------------------
    //7 全部高級肉--------------------------------5個警衛，5個店員
    //6 有高級肉70%普通肉30%----------------------3~4個警衛，3個店員
    //5 普通肉80%肉乾19%可能出現高級肉-------------1個警衛，2個店員
    //4 普通肉50%肉乾50%-------------------------2個店員
    //3 全部肉乾---------------------------------1個店員
    //2 肉乾90%，有毒肉乾(20秒頭暈)10%------------1個店員
    //除了上述，還會影響店面大小、顏色，新舊
    //--------------------------------StoreLevel--------------------------------

    float StoreLevel(float x,float z){
        float level = 2.0f;//最小2最大7
        
        level += 1.0f;//之後再計算
        return level;
    }
}
