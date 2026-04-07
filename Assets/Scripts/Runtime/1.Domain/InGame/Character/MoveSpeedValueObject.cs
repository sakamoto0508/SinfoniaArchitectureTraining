using System;

namespace Domain
{
    public readonly struct MoveSpeedValueObject
    {
        /// <summary>
        ///     移動速度を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value">移動速度（0以上）。</param>
        public MoveSpeedValueObject(float value)
        {
            Value = value;
        }

        /// <summary> 移動速度。 </summary>
        public readonly float Value;
    }
}
