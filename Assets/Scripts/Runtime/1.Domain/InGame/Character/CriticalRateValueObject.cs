using System;

namespace Domain
{
    public readonly struct CriticalRateValueObject
    {
        /// <summary>
        ///     クリティカル率を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value"></param>
        public CriticalRateValueObject(float value)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value), "CriticalRate must be non-negative.");
            Value = value;
        }

        public readonly float Value { get; }
    }
}
