﻿#pragma checksum "C:\Users\User\Documents\BME6\Kliensoldali\KlitechHf2\hf3333333\klitechHazi\klitechHazi\View\BookDetailsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2459D47C5A686F5A0B97B5B8A48360CD607DC77BA37B71410C86CB7FA7B487E8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace klitechHazi
{
    partial class BookDetailsPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // View\BookDetailsPage.xaml line 61
                {
                    this.povCharactersListBox = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            case 4: // View\BookDetailsPage.xaml line 64
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.Character_Click;
                }
                break;
            case 5: // View\BookDetailsPage.xaml line 47
                {
                    this.charactersListBox = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            case 7: // View\BookDetailsPage.xaml line 50
                {
                    global::Windows.UI.Xaml.Controls.Button element7 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element7).Click += this.Character_Click;
                }
                break;
            case 8: // View\BookDetailsPage.xaml line 24
                {
                    this.titleLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9: // View\BookDetailsPage.xaml line 25
                {
                    this.authorLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // View\BookDetailsPage.xaml line 26
                {
                    this.isbnLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11: // View\BookDetailsPage.xaml line 27
                {
                    this.pagesLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // View\BookDetailsPage.xaml line 28
                {
                    this.publisherLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13: // View\BookDetailsPage.xaml line 29
                {
                    this.countryLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14: // View\BookDetailsPage.xaml line 30
                {
                    this.mediaTypeLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 15: // View\BookDetailsPage.xaml line 31
                {
                    this.releasedLabel = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 16: // View\BookDetailsPage.xaml line 34
                {
                    global::Windows.UI.Xaml.Controls.Button element16 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element16).Click += this.BackButton_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

