using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed partial class NCore
{
    private static Dictionary<Type, ReferencePool> __ReferencePools = new Dictionary<Type, ReferencePool>();
    private static bool __isCheckReferenceType = true;
    private static bool __isLimitReferenceCount = true;
    private static uint __referenceLimitCount = 100;

    /// <summary>
    /// 是否开启引用类型检查
    /// </summary>
    public static bool IsCheckReferenceType
    {
        get => __isCheckReferenceType;
        set => __isCheckReferenceType = value;
    }

    /// <summary>
    /// 是否限制引用个数
    /// </summary>
    public static bool isLimitReferenceCount
    {
        get => __isLimitReferenceCount;
        set => __isLimitReferenceCount = value;
    }

    /// <summary>
    /// 引用类型限制个数
    /// </summary>
    public static uint ReferenceLimitCount
    {
        get => __referenceLimitCount;
        set => __referenceLimitCount = value;
    }

    /// <summary>
    /// 创建或从引用池中获取一个指定的引用类型对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T New<T>() where T : class, IReference, new()
    {
        return null;
    }

    /// <summary>
    /// 回收引用对象
    /// </summary>
    /// <param name="reference"></param>
    public static void Recycle(IReference reference)
    {

    }
}
