
using System;

namespace Domain
{
    public readonly struct CriticalMultiplierValueObject
    {
        /// <summary>
        ///     クリティカル倍率を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value">クリティカル倍率（0以上）。</param>
        public CriticalMultiplierValueObject(float value)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value), "クリティカル倍率は0以上でなければなりません。");
            Value = value;
        }

        /// <summary> クリティカル倍率。 </summary>
        public readonly float Value;
    }
}
