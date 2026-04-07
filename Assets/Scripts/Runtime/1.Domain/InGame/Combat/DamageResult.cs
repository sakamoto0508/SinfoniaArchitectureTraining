namespace Domain
{
    /// <summary>
    ///     ダメージ計算の結果を表す値型。
    /// </summary>
    public readonly struct DamageResult
    {
        /// <summary> コンストラクタ。 </summary>
        /// <param name="damage">計算されたダメージ（整数）。</param>
        /// <param name="isCritical">クリティカル発生フラグ。</param>
        public DamageResult(int damage, bool isCritical)
        {
            Damage = damage;
            IsCritical = isCritical;
        }

        /// <summary> 計算されたダメージ。 </summary>
        public int Damage { get; }

        /// <summary> クリティカルが発生したかどうか。 </summary>
        public bool IsCritical { get; }
    }
}
