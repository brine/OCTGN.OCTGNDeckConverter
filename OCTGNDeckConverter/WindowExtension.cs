// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="jlkatz">
// Copyright (c) 2013 Justin L Katz. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO.Packaging;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace OCTGNDeckConverter
{
    public static class WindowExtension
    {
        public static void LoadViewFromUri(this Window window, string baseUri)
        {
            try
            {
                var resourceLocater = new Uri(baseUri, UriKind.RelativeOrAbsolute);
                var exprCa = (PackagePart)typeof(Application).GetMethod("GetResourceOrContentPart", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { resourceLocater });
                var stream = exprCa.GetStream();
                var uri = new Uri((Uri)typeof(BaseUriHelper).GetProperty("PackAppBaseUri", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null), resourceLocater);
                var parserContext = new ParserContext
                {
                    BaseUri = uri
                };
                typeof(XamlReader).GetMethod("LoadBaml", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, new object[] { stream, parserContext, window, true });

            }
            catch (Exception)
            {
                //log
            }
        }
    }
}