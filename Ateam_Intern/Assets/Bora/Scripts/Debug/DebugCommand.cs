using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugCommand : MonoBehaviour {

	[SerializeField]
	FeverGauge feverGauge = null;

	void LateUpdate () {

		//if (!Application.isEditor)
		//	return;

		// プレイヤー死亡
		if (Input.GetKeyDown (KeyCode.Q)) {
			GameMainUpperManager.instance.player.Damaged(100000);
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			GameMainUpperManager.instance.player.Damaged(1000);
		}

		// Uno手前までゲージ増加
		if (Input.GetKeyDown (KeyCode.E)) {
			feverGauge.SetPoint (feverGauge.feverPointMax - 2);
		}

		// Unoまでゲージ増加
		if (Input.GetKeyDown (KeyCode.R)) {
			feverGauge.SetPoint (feverGauge.feverPointMax);
		}

		// 詰んだらシャッフル
		if (Input.GetKeyDown (KeyCode.T)) {
			for (int i = 0; i < 16; i++) {
				UnoCreateManager.Instance.NonSelect ();
			}
		}

		// 強攻撃
		if (Input.GetKeyDown (KeyCode.Y)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_RED;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			for (int i = 0; i < 5; i++) {
				card.m_Number = (UnoStruct.eNumber)Random.Range(0,10);
				list [10 + i].DebugCard (card);
			}
        }

        // 敵死亡
        if (Input.GetKeyDown(KeyCode.U))
        {
            List<Enemy> list = GameMainUpperManager.instance.enemyList;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Damaged(10000000, Charactor.Attribute.Solar);
            }
        }

        // ボス特殊攻撃
        if (Input.GetKeyDown(KeyCode.I))
        {
            Enemy_Debug.BossSpecialAttack();
        }

		// スキップ
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_RED;
			card.m_Number = UnoStruct.eNumber.NUMBER_SKIP;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [0].DebugCard (card);
		}

		// リバース
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_RED;
			card.m_Number = UnoStruct.eNumber.NUMBER_REVERSE;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [1].DebugCard (card);
		}

		// ドロー２
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_RED;
			card.m_Number = UnoStruct.eNumber.NUMBER_DROWTWO;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [2].DebugCard (card);
		}

		// ワイルド
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_WILD;
			card.m_Number = UnoStruct.eNumber.NUMBER_WILD;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [3].DebugCard (card);
		}

		// ドロー４
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_WILD;
			card.m_Number = UnoStruct.eNumber.NUMBER_DROWFOUR;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [4].DebugCard (card);
		}

		// ハレ
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_RED;
			card.m_Number = UnoStruct.eNumber.NUMBER_FOUR;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [5].DebugCard (card);
		}

		// アメ
		if (Input.GetKeyDown (KeyCode.Alpha7)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_BLUE;
			card.m_Number = UnoStruct.eNumber.NUMBER_TWO;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [6].DebugCard (card);
		}

		// カゼ
		if (Input.GetKeyDown (KeyCode.Alpha8)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_GREEN;
			card.m_Number = UnoStruct.eNumber.NUMBER_SEVEN;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [7].DebugCard (card);
		}

		// カミナリ
		if (Input.GetKeyDown (KeyCode.Alpha9)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_YELLOW;
			card.m_Number = UnoStruct.eNumber.NUMBER_SIX;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [8].DebugCard (card);
		}

		// フレンド
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			UnoStruct.tCard card;
			card.m_Color = UnoStruct.eColor.COLOR_RED;
			card.m_Number = UnoStruct.eNumber.NUMBER_EIGHT;
			List<UnoData> list = UnoCreateManager.Instance.GetCardList ();
			list [9].DebugCard (card);
		}

		// ポーズ、アンポーズ
		if (Input.GetKeyDown (KeyCode.A)) {
			GamePause.Pause ();
		} else if (Input.GetKeyDown (KeyCode.S)) {
			GamePause.UnPause ();
		}
	}
}
