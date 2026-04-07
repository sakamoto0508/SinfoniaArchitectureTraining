using System;

namespace Domain
{
    /// <summary>
    ///     乱数生成を抽象化するインターフェース（ドメイン内での確率判定に使用）。
    /// </summary>
    public interface IRandomProvider
    {
        /// <summary> 0.0以上1.0未満の乱数を返す。 </summary>
        double NextDouble();
    }
}
