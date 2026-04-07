namespace Domain
{
    /// <summary>
    ///     攻撃者のステータスを保持する値型。
    /// </summary>
    public readonly struct AttackerStats
    {
        /// <summary> コンストラクタ。 </summary>
        /// <param name="attack">攻撃力。</param>
        /// <param name="criticalRate">クリティカル発生率（0〜1）。</param>
        /// <param name="criticalMultiplier">クリティカル時の倍率。</param>
        public AttackerStats(float attack, float criticalRate, float criticalMultiplier)
        {
            Attack = attack;
            CriticalRate = criticalRate;
            CriticalMultiplier = criticalMultiplier;
        }

        /// <summary> 攻撃力。 </summary>
        public float Attack { get; }

        /// <summary> クリティカル発生率（0〜1）。 </summary>
        public float CriticalRate { get; }

        /// <summary> クリティカル時の倍率。 </summary>
        public float CriticalMultiplier { get; }
    }
}
