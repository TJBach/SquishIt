﻿using System;
using NUnit.Framework;
using SquishIt.CoffeeScript.Coffee;
using SquishIt.Framework;
using SquishIt.Tests.Helpers;

namespace SquishIt.Tests
{
    [TestFixture]
    public class CoffeeScriptTests
    {
        //TODO: should probably have more tests here
        JavaScriptBundleFactory javaScriptBundleFactory;

        [SetUp]
        public void Setup()
        {
            javaScriptBundleFactory = new JavaScriptBundleFactory();
        }

        [TestCase(typeof(MsIeCoffeeScript.CoffeeScriptPreprocessor)), Platform(Exclude = "Unix, Linux, Mono")]
        [TestCase(typeof(CoffeeScript.CoffeeScriptPreprocessor)), Platform(Exclude = "Unix, Linux, Mono")]
        public void CanBundleJavascriptWithArbitraryCoffeeScript(Type preprocessorType)
        {
            var preprocessor = Activator.CreateInstance(preprocessorType) as IPreprocessor;
            Assert.NotNull(preprocessor);

            var coffee = "alert 'test' ";

            var tag = javaScriptBundleFactory
                .WithDebuggingEnabled(false)
                .Create()
                .WithPreprocessor(preprocessor)
                .AddString(coffee, ".coffee")
                .Render("~/brewed.js");

            var compiled = javaScriptBundleFactory.FileWriterFactory.Files[TestUtilities.PrepareRelativePath(@"brewed.js")];

            Assert.AreEqual(@"(function(){alert(""test"")}).call(this);", compiled);
            Assert.AreEqual(@"<script type=""text/javascript"" src=""brewed.js?r=hash""></script>", tag);
        }

        [TestCase(typeof(MsIeCoffeeScript.CoffeeScriptPreprocessor)), Platform(Exclude = "Unix, Linux, Mono")]
        [TestCase(typeof(CoffeeScript.CoffeeScriptPreprocessor)), Platform(Exclude = "Unix, Linux, Mono")]
        public void CanBundleJavascriptInDebugWithArbitraryCoffeeScript(Type preprocessorType)
        {
            var preprocessor = Activator.CreateInstance(preprocessorType) as IPreprocessor;
            Assert.NotNull(preprocessor);

            var coffee = "alert 'test' ";

            var tag = javaScriptBundleFactory
                .WithDebuggingEnabled(true)
                .Create()
                .WithPreprocessor(preprocessor)
                .AddString(coffee, ".coffee")
                .Render("~/brewed.js");

            Assert.AreEqual("<script type=\"text/javascript\">(function() {\n\n  alert('test');\n\n}).call(this);\n</script>\n", TestUtilities.NormalizeLineEndings(tag));
        }

        [TestCase(typeof(CoffeeScriptCompiler)), Platform(Include = "Unix, Linux, Mono")]
        [TestCase(typeof(MsIeCoffeeScript.Coffee.CoffeeScriptCompiler)), Platform(Include = "Unix, Linux, Mono")]
        public void CompileFailsGracefullyOnMono(Type compilerType)
        {
            var compiler = Activator.CreateInstance(compilerType);
            var method = compilerType.GetMethod("Compile");
            var exception = Assert.Throws<Exception>(() => method.Invoke(compiler, new[] { "" }));
            Assert.AreEqual("CoffeeScript not yet supported for mono.", exception.Message);
        }
    }
}
