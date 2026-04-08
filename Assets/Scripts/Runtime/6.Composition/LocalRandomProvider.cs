using System;
using Domain;

namespace Composition
{
    /// <summary>
    ///     Composition 層内で使用する簡易乱数プロバイダ実装。
    ///     InfraStructure の SystemRandomProvider に依存しないようローカル実装を提供する。
    /// </summary>
    public sealed class LocalRandomProvider : IRandomProvider
    {
        public LocalRandomProvider()
        {
            _random = new Random();
        }

        public double NextDouble()
        {
            lock (_sync)
            {
                return _random.NextDouble();
            }
        }

        private readonly Random _random;
        private readonly object _sync = new object();
    }
}
