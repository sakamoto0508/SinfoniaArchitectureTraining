namespace Domain
{
    public readonly struct AttackRangeValueObject
    {
        /// <summary>
        ///     攻撃範囲を初期化するコンストラクタ。
        /// </summary>
        /// <param name="value">攻撃範囲（0以上）。</param>
        public AttackRangeValueObject(float value)
        {
            Value = value < 0f ? 0f : value;
        }

        /// <summary> 攻撃範囲。 </summary>
        public readonly float Value;
    }
}
