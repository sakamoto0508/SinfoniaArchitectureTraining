namespace Domain
{
    /// <summary>
    ///     防御者のステータスを保持する値型。
    /// </summary>
    public readonly struct DefenderStats
    {
        /// <summary> コンストラクタ。 </summary>
        /// <param name="defense">防御力。</param>
        public DefenderStats(float defense)
        {
            Defense = defense;
        }

        /// <summary> 防御力。 </summary>
        public float Defense { get; }
    }
}
