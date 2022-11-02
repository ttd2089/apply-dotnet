using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ttd2089.Apply.UnitTests;

public class ApplyExtensionsTests
{
    [Fact]
    public void TargetIsNonTaskFnReturnsNonTask_ReturnsFnAppliedToTarget()
    {
        var target = "forty-two";
        var expected = 42;
        var fn = new Mock<Func<string, int>>();
        fn.Setup(x => x.Invoke(It.IsAny<string>())).Returns(expected);
        var result = target.ApplyFn(fn.Object);
        Assert.Equal(expected, result);
        fn.Verify(x => x.Invoke(target), Times.Once);
    }

    [Fact]
    public async Task TargetIsNonTaskFnReturnsTask_ReturnsTaskOfFnAppliedToTarget()
    {
        var target = "forty-two";
        var expected = 42;
        var fn = new Mock<Func<string, Task<int>>>();
        fn.Setup(x => x.Invoke(It.IsAny<string>())).ReturnsAsync(expected);
        var result = await target.ApplyFn(fn.Object);
        Assert.Equal(expected, result);
        fn.Verify(x => x.Invoke(target), Times.Once);
    }

    [Fact]
    public async Task TargetIsTaskFnReturnsNonTask_ReturnsTaskOfFnAppliedToTarget()
    {
        var target = Task.FromResult("forty-two");
        var expected = 42;
        var fn = new Mock<Func<string, int>>();
        fn.Setup(x => x.Invoke(It.IsAny<string>())).Returns(expected);
        var result = await target.ApplyFn(fn.Object);
        Assert.Equal(expected, result);
        fn.Verify(x => x.Invoke(target.Result), Times.Once);
    }


    [Fact]
    public async Task TargetIsTaskFnReturnsTask_ReturnsTaskOfFnAppliedToTarget()
    {
        var target = Task.FromResult("forty-two");
        var expected = 42;
        var fn = new Mock<Func<string, Task<int>>>();
        fn.Setup(x => x.Invoke(It.IsAny<string>())).ReturnsAsync(expected);
        var result = await target.ApplyFn(fn.Object);
        Assert.Equal(expected, result);
        fn.Verify(x => x.Invoke(target.Result), Times.Once);
    }

    [Fact]
    public async Task TaskChainExample()
    {
        var result = await "The answer to the ultimate question of life, the universe, and everything?"
            .ApplyFn(question => new string(question.Reverse().ToArray()))
            .ApplyFn(async question =>
            {
                await Task.Delay(question.Length);
                return question;
            })
            .ApplyFn(question =>
            {
                int n = nameof(question).Length;
                foreach (var c in question)
                {
                    n += c;
                }
                return (int)(Math.Round(Math.Sqrt(n)) / 2);
            }).ApplyFn(async n =>
            {
                await Task.Delay(n);
                return n;
            });

        Assert.Equal(42, result);
    }
}