﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = Ar4eR_ValerA.Test.CSharpCodeFixVerifier<
    Ar4eR_ValerA.Ar4eR_ValerA_O5_Analyzer,
    Ar4eR_ValerA.Ar4eR_ValerA_O5_CodeFixProvider>;

namespace Ar4eR_ValerA.Test
{
    [TestClass]
    public class Ar4eR_ValerA_O5_UnitTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class t
        {
            public string TryCalc(int x)
            {
                if (x > 1) 
                {
                    return string.Empty;
                }
                else
                {
                    return String.Empty;
                }
            }   
        }
    }";

            var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class t
        {
            public bool TryCalc(out string stringOut, int x)
            {
                if (x > 1) 
                {
                stringOut = string.Empty;
                return true;
                }
                else
                {
                stringOut = String.Empty;
                return true;
                }
            }   
        }
    }";

            var expected = VerifyCS
                .Diagnostic("Ar4eR_ValerA")
                .WithSpan(13, 13, 23, 14)
                .WithArguments("TryCalc");
            await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }

        [TestMethod]
        public async Task TestMethod3()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class t
        {
            public bool TryCalc(int x)
            {
                if (x > 1) 
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }   
        }
    }";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        [TestMethod]
        public async Task TestMethod4()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class t
        {
            public void TryCalc(int x)
            {
                Console.WriteLine();
            }   
        }
    }";

            var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class t
        {
            public bool TryCalc(int x)
            {
                Console.WriteLine();
            return true;
        }   
        }
    }";

            var expected = VerifyCS
                .Diagnostic("Ar4eR_ValerA")
                .WithSpan(13, 13, 16, 14)
                .WithArguments("TryCalc");
            await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }


        [TestMethod]
        public async Task TestMethod5()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class t
        {
            public int TryCalc(int x)
            {
                return 2;
            }   
        }
    }";

            var fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class t
        {
            public bool TryCalc(out int intOut, int x)
            {
            intOut = 2;
            return true;
            }   
        }
    }";

            var expected = VerifyCS
                .Diagnostic("Ar4eR_ValerA")
                .WithSpan(13, 13, 16, 14)
                .WithArguments("TryCalc");
            await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
        }
    }
}