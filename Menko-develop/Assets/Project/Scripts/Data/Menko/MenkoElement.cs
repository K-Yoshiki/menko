using System.Collections;

namespace MenkoiMonster
{
	/// <summary>
	/// メンコの属性値
	/// </summary>
	public enum MenkoElement : int
	{
		None,
		Flame,
		Water,
		Leaf,
		Light,
		Dark,

		LastIndexer // 必ず最後においてください
	}

	public static class MenkoElementUtils
	{
		// 格子状データとして弱点か否かを格納(列参照が攻、行参照が受)
		// 読みだす時は属性Enumの番号値で取ってくる
		// 実際の値はこれに0.01を乗算した値になる
		static byte[] magnif = new byte[(int)MenkoElement.LastIndexer * (int)MenkoElement.LastIndexer] {
		//	None	Flame	Water	Leaf	Light	Dark
			100,	100,	100,	100,	100,	100,	// None
			100,	100,	125,	125,	100,	100,	// Flame
			100,	075,	100,	075,	100,	100,	// Water
			100,	125,	075,	100,	100,	100,	// Leaf
			100,	100,	100,	100,	100,	125,	// Light
			100,	100,	100,	100,	125,	100		// Dark
		};

		/// <summary>
		/// 攻撃側属性と防御側属性による属性倍率の取得をします
		/// </summary>
		/// <param name="attacker"></param>
		/// <param name="difenser"></param>
		/// <returns></returns>
		public static float GetMagnification(MenkoElement attacker, MenkoElement difenser)
		{
			int atkIndex = (int)attacker;
			int difIndex = (int)difenser * (int)MenkoElement.LastIndexer;
			return magnif[atkIndex + difIndex] * 0.01f;
		}
	}
}