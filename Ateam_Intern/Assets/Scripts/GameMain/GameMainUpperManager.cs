using UnityEngine;
using System.Collections.Generic;

public class GameMainUpperManager : MonoBehaviour
{
    [SerializeField]
    private GameObject charactors;

    [SerializeField]
    private GameObject friendPrefab;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject feverGaugePrefab;


    public Player player { get; private set; }
    public FeverGauge feverGauge { get; private set; }
    public Queue<RemovedCard> removedCards { get; private set; }                     //消したカードマスの効果を格納
    public List<PlayerCharactor> charactorList { get; private set; }                 //ゲームメイン画面にいる自キャラクターのリスト
    public List<PlayerCharactor> charactorAndFriend { get; private set;}             //フレンドを含めたキャラリスト（カード情報の取得に使う）
    public PlayerCharactor friend { get; private set;}
    public List<Enemy> enemyList { get; private set; }                               //現在戦っている敵のリスト


    //どこからでもアクセスできるインスタンス
    static public GameMainUpperManager instance = null;

    void Awake()
    {
        //ゲームメイン画面ではシングルトン
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        removedCards = new Queue<RemovedCard>();
        charactorList = new List<PlayerCharactor>();
        enemyList = new List<Enemy>();
        charactorAndFriend = new List<PlayerCharactor>();

        //メニュー画面で選択したキャラクターのリストをどこかから取得
        //charactorDataList = JsonManager.Load<CharactorData>("SelectedCharactor.json") as List<CharactorData>;


        player = playerPrefab.GetComponent<Player>();
        feverGauge = feverGaugePrefab.GetComponent<FeverGauge>();
        friend = null;

        Init();
    }

    void Start()
    {
		CardResource.Instance.SetCharaCard ();
		UnoCreateManager.Instance.Init ();
    }

	//void Update() {
    void LateUpdate() {
        //戦闘中じゃなかったら行動しない
        if (BattleManager.Instance.GetIsInBattle() == false) return;

        //プレイヤーが生きていたら
        if(!player.isDead)
        {
            //カードが場に置かれたら
            int cardAmount = TurnData.Instance.CardAmount;
            if (cardAmount > 0)
            {
                //カードアクション
                player.CardAction();
            }
        }
    }


    //初期化
    private void Init()
    {
        List<CharactorData> charactorDataList = GetComponent<CharactorDataList_Debug>().charactorDataList;
        CharactorData friendCharactorData = GetComponent<CharactorDataList_Debug>().friendCharactorData;


        int playerHpSum = 0;
        int charactorCount = charactorDataList.Count;

        //フレンドを選択していたらフレンドを登録
        if (friendCharactorData.skillText != "")
        {
            PlayerCharactor friendCharactor = friendPrefab.GetComponent<PlayerCharactor>();
            friendCharactor.InitFriend(friendCharactorData);
            friend = friendCharactor;
            charactorAndFriend.Add(friend);
            friendCharactor.gameObject.SetActive(false);
        }

        for (int i = 0; i < charactors.transform.childCount; i++)
        {
            GameObject charactor = charactors.transform.GetChild(i).gameObject;

            //メニュー画面で選択した数だけキャラクターを表示
            if(i < charactorCount)
            {
                CharactorData data = charactorDataList[i];
                PlayerCharactor playerCharactor = charactor.GetComponent<PlayerCharactor>();
                playerCharactor.Init(data);
                charactorList.Add(playerCharactor);
                if(charactorAndFriend.Count > 0)
                {
                    charactorAndFriend.Add(playerCharactor);
                }
                //プレイヤーのHPを計算
                playerHpSum += data.hp;
            }
            else
            {
                charactor.SetActive(false);
            }
        }



        //プレイヤーの初期化
        player.GetComponent<Player>().Init(playerHpSum);

    }


    public UnoStruct.tCard GetLeaderTCard()
    {
        UnoStruct.tCard tCard = new UnoStruct.tCard();

        foreach(PlayerCharactor elem in charactorList)
        {
            if (elem.isLeader) { tCard = elem.GetTCard();}
        }

        return tCard;
    }


    //カードマスを一つ消したことにするデバッグ関数
    public void RemoveCard_Debug(RemovedCard_Debug removedCard)
    {
        RemovedCard removeCard = new RemovedCard(removedCard.num, removedCard.symbol, removedCard.attribute);
        removedCards.Enqueue(removeCard);
        //フィーバー中でなければフィーバーポイントを加算
        if (!feverGauge.isFeverMode) { feverGauge.IncrementPoint(); }
    }
}


public class RemovedCard
{
    public enum Symbol
    {
        None,               //記号なし
        Skip,               //スキップ
        Reverse,            //リバース
        DrawTwo,            //ドロー2
        Wild,               //ワイルド
        WildDrawFour,       //ワイルドドロー4
    }

    public int num { get; private set; }                         //数字
    public Symbol symbol { get; private set; }                   //記号
    public Charactor.Attribute attribute { get; private set; }   //属性

    public RemovedCard(int num, Symbol symbol, Charactor.Attribute attribute)
    {
        this.num = num;
        this.symbol = symbol;
        this.attribute = attribute;
    }
}