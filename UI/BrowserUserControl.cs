﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WebBrowser.Logic;

namespace UI
{
    public partial class BrowserUserControl : UserControl
    {
        Stack<string> backlinks = new Stack<string>();
        Stack<string> forwardlinks = new Stack<string>();


        public BrowserUserControl()
        {
            InitializeComponent();
           
        }

        private void AddressBox_Click(object sender, EventArgs e)
        {
            addressBox.Text = "";
        }

        private void ToolStripButtonSearch_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(addressBox.Text);


        }

        private void ToolStripReloadButton_Click(object sender, EventArgs e)
        {
            // Skip refresh if about:blank is loaded to avoid removing
            // content specified by the DocumentText property.
            if (!webBrowser.Url.Equals("about:blank"))
            {
                webBrowser.Refresh();
            }
        }

        private void ToolStripBackButton_Click(object sender, EventArgs e)
        {

            webBrowser.GoBack();

            //Push the current link to the top of 'Forward links' stack
            // forwardlinks.Push(addressBox.Text);

            //Pop the new link from the 'Back links' stack, and navigate to it in the browser
            //if (backlinks.Count != 0) {
            //    var popUrl = backlinks.Pop();
            //    webBrowser.Navigate(popUrl);
            //}




        }

        private void ToolStripHomeButton_Click(object sender, EventArgs e)
        {
            webBrowser.GoHome();

        }

        private void ToolStripForwardButton_Click(object sender, EventArgs e)
        {
            webBrowser.GoForward();

        }

        private void AddressBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                webBrowser.Navigate(addressBox.Text);
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void toolStripBookmarkButton_Click(object sender, EventArgs e)
        {

            var item = new BookmarkItem();
            item.URL = webBrowser.Document.Url.ToString();
            item.Title = webBrowser.Document.Title;
            BookmarkManager.AddItem(item);
            
        }

        private void webBrowser_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string title = webBrowser.Document.Title;
            var item = new HistoryItem();
            item.Title = title;
            item.URL = webBrowser.Document.Url.ToString();
            item.Date = DateTime.Now;

            HistoryManager.AddItem(item);
        }

        private void webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {


            progressBar.Maximum = (int)e.MaximumProgress;
            if (e.CurrentProgress >= 0 && e.CurrentProgress <= e.MaximumProgress)
            {
                progressBar.Value = (int)e.CurrentProgress;

            }
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading";
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Done";
        }
    }
}
