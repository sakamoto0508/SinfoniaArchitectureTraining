using System;

namespace Domain
{
    public readonly struct CriticalRateValueObject
    {
        /// <summary>
        ///     クリティカル率を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value">クリティカル発生率（0〜1）。</param>
        public CriticalRateValueObject(float value)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value), "CriticalRate must be non-negative.");
            Value = value;
        }

        /// <summary> クリティカル発生率（0〜1）。 </summary>
        public readonly float Value { get; }
    }
}
