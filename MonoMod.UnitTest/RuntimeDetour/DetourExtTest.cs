﻿#pragma warning disable CS1720 // Expression will always cause a System.NullReferenceException because the type's default value is null
#pragma warning disable xUnit1013 // Public method should be marked as test

using Xunit;
using MonoMod.RuntimeDetour;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MonoMod.UnitTest {
    [Collection("RuntimeDetour")]
    public class DetourExtTest {
        [Fact]
        public void TestDetours() {
            // The following use cases are not meant to be usage examples.
            // Please take a look at DetourTest and HookTest instead.

            using (NativeDetour d = new NativeDetour(
                typeof(TestObject).GetMethod("TestStaticMethod").GetNativeStart(),
                typeof(DetourExtTest).GetMethod("TestStaticMethod_A")
            )) {
                int staticResult = d.GenerateTrampoline<Func<int, int, int>>()(2, 3);
                Console.WriteLine($"TestStaticMethod(2, 3): {staticResult}");
                Assert.Equal(6, staticResult);

                staticResult = TestObject.TestStaticMethod(2, 3);
                Console.WriteLine($"TestStaticMethod(2, 3): {staticResult}");
                Assert.Equal(12, staticResult);
            }
        }

        public static int TestStaticMethod_A(int a, int b) {
            return a * b * 2;
        }

    }
}
