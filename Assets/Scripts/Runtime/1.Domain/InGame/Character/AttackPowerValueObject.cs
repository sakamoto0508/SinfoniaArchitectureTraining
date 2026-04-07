using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace Domain
{
    public readonly struct AttackPowerValueObject
    {
        /// <summary>
        ///     攻撃力を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value"></param>
        public AttackPowerValueObject(float value)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(nameof(value), "AttackPower must be non-negative.");
            Value = value;
        }

        public float Value { get; }
    }
}
