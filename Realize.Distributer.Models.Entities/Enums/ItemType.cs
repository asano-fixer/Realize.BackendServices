using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.Distributer.Models.Entities.Enums
{
    public enum ItemType : int
    {
        /// <summary>
        /// なし
        /// </summary>
        None = 0,
        /// <summary>
        /// ヒーロ
        /// </summary>
        Hero,
        /// <summary>
        /// 武器
        /// </summary>
        Weapon,
        /// <summary>
        /// アクセサリ
        /// </summary>
        Accessory,
        /// <summary>
        /// スタックアイテム
        /// </summary>
        StackItem,
        /// <summary>
        /// ゲーム内通貨
        /// </summary>
        InGameMoney,
        /// <summary>
        /// マナジュエル（無料）
        /// </summary>
        /// <remarks>ゲーム内課金通貨。名前は仮置き</remarks>
        ManaJewel,
    }
}
