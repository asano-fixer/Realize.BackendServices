using Realize.BackendServices.Core.Models.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.BackendServices.Core.Models.Entities.Requests.Distribution
{
    public class DistributionInfo
    {
        /// <summary>
        /// 配布先
        /// </summary>
        public IEnumerable<long> NativeUserIds { get; set; }

        /// <summary>
        /// アイテムタイプ
        /// </summary>
        [Required]
        public ItemType ItemType { get; set; }
        /// <summary>
        /// アイテムId
        /// </summary>
        public int? ItemId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int Quantity { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 武器：レベル
        /// </summary>
        public int? WeaponLevel { get; set; }

        /// <summary>
        /// 武器：武器スキル（MActiveSkillId）
        /// </summary>
        public int? WeaponActiveSkillId { get; set; }
        /// <summary>
        /// 武器：武器スキルレベル
        /// </summary>
        public int? WeaponActiveSkillLv { get; set; }
        /// <summary>
        /// 武器：パッシブスキル（MPassiveSkillId）
        /// </summary>
        public int? WeaponPassiveSkillId { get; set; }
        /// <summary>
        /// 武器：パッシブスキルレベル
        /// </summary>
        public int? WeaponPassiveSkillLv { get; set; }
        /// <summary>
        /// 武器：限界突破数
        /// </summary>
        public int? WeaponLimitBreakCount { get; set; }
        /// <summary>
        /// アクセサリー：パッシブスキル1のId(MPassiveSkillId)
        /// </summary>
        public int? AccessoryPassiveSkillId_1 { get; set; }
        /// <summary>
        /// アクセサリー：パッシブスキル2のId(MPassiveSkillId)
        /// </summary>
        public int? AccessoryPassiveSkillId_2 { get; set; }
        /// <summary>
        /// アクセサリー：パッシブスキル3のId(MPassiveSkillId)
        /// </summary>
        public int? AccessoryPassiveSkillId_3 { get; set; }
        /// <summary>
        /// アクセサリー：パッシブスキル１のレベル
        /// </summary>
        public int? AccessoryPassiveSkillLevel_1 { get; set; }
        /// <summary>
        /// アクセサリー：パッシブスキル２のレベル
        /// </summary>
        public int? AccessoryPassiveSkillLevel_2 { get; set; }
        /// <summary>
        /// アクセサリー：パッシブスキル３のレベル
        /// </summary>
        public int? AccessoryPassiveSkillLevel_3 { get; set; }
        /// <summary>
        /// アクセサリー：ラック
        /// </summary>
        public int? AccessoryLuck { get; set; }
        /// <summary>
        /// ヒーロー：限界突破数
        /// </summary>
        public int? HeroLimitBreakCount { get; set; }
    }
}
