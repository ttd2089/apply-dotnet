using Moq;
using System;
using Xunit;

namespace Ttd2089.Apply.UnitTests;

public class ApplyExtensionsTests
{
    [Fact]
    public void AppliesFunctionToValue()
    {
        var target = "forty-two";
        var expected = 42;
        var fn = new Mock<Func<string, int>>();
        fn.Setup(x => x.Invoke(It.IsAny<string>())).Returns(expected);
        var result = target.ApplyFn(fn.Object);
        Assert.Equal(expected, result);
        fn.Verify(x => x.Invoke(target), Times.Once);
    }
}