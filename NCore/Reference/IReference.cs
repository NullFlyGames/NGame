using System;

public interface IReference : IDisposable
{
    int id { get; set; }
    void Awake();
    void Recycle();
}
