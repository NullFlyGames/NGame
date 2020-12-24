using System;

namespace NGame.NCore
{
    /// <summary>
    /// 实体对象
    /// </summary>
    public interface IEntity : IDisposable
    {
        int id { get; set; }
    }
}