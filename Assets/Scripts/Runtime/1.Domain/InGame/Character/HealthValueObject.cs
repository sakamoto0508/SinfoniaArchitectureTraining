using System;

namespace Domain
{
    public readonly struct HealthValueObject
    {
        /// <summary>
        ///     体力を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value"></param>
        public HealthValueObject(float value)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value), "Health must be non-negative.");
            Value = value;
        }

        public readonly float Value { get; }
    }
}
