using System;

namespace Application
{
    /// <summary>
    ///     乱数生成を抽象化するインターフェース。
    /// </summary>
    public interface IRandomProvider
    {
        /// <summary> 0.0以上1.0未満の乱数を返す。 </summary>
        double NextDouble();
    }
}
