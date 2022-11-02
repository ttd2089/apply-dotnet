using System;
using System.Threading.Tasks;

namespace Ttd2089.Apply;

/// <summary>
/// Contains the apply function as an extension method.
/// </summary>
public static class ApplyExtensions
{
    /// <summary>
    /// Applies <paramref name="fn"/> to <paramref name="target"/> and returns the result.
    /// </summary>
    public static U ApplyFn<T, U>(this T target, Func<T, U> fn) => fn(target);

    /// <summary>
    /// Applies <paramref name="fn"/> to <paramref name="target"/> and returns the result.
    /// </summary>
    public static async Task<U> ApplyFn<T, U>(this T target, Func<T, Task<U>> fn) => await fn(target);

    /// <summary>
    /// Applies <paramref name="fn"/> to <paramref name="target"/> and returns the result.
    /// </summary>
    public static async Task<U> ApplyFn<T, U>(this Task<T> target, Func<T, U> fn) => fn(await target);

    /// <summary>
    /// Applies <paramref name="fn"/> to <paramref name="target"/> and returns the result.
    /// </summary>
    public static async Task<U> ApplyFn<T, U>(this Task<T> target, Func<T, Task<U>> fn) => await fn(await target);
}
