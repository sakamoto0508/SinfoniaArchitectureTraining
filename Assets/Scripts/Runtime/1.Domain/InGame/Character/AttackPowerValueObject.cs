using System;

namespace Domain
{
    public readonly struct AttackPowerValueObject
    {
        /// <summary>
        ///     攻撃力を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value">攻撃力の値（0以上）。</param>
        public AttackPowerValueObject(float value)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value), "AttackPower must be non-negative.");
            Value = value;
        }

        /// <summary> 攻撃力の値。 </summary>
        public readonly float Value { get; }
    }
}
