
using System;

namespace Domain
{
    public readonly struct CriticalMultiplierValueObject
    {
        /// <summary>
        ///     クリティカル倍率を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value"></param>
        public CriticalMultiplierValueObject(float value)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value), "CriticalMultiplier must be non-negative.");
            Value = value;
        }

        public float Value { get; }
    }
}
