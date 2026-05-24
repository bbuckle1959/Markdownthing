using System.Reflection;
using System.Text;

namespace MarkdownThing
{
    public partial class AboutBox : Form
    {
        private const string AuthorName = "Barry Buckle";
        private const string CopyrightYears = "2024-2026";

        public AboutBox()
        {
            InitializeComponent();
            AppIcons.ApplyTo(this);

            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = $"Version {AssemblyVersion}";
            labelCopyright.Text = AssemblyCopyright;
            linkLabelGitHub.Text = AppConstants.RepositoryUrl;
            linkLabelGitHub.Links.Add(0, AppConstants.RepositoryUrl.Length, AppConstants.RepositoryUrl);
            textBoxDescription.Text = GetAboutText();
            LoadAboutLogo();
            Shown += AboutBox_Shown;
            textBoxDescription.Enter += TextBoxDescription_Enter;
        }

        private void AboutBox_Shown(object? sender, EventArgs e)
        {
            ClearTextBoxSelection();
            okButton.Focus();
        }

        private void TextBoxDescription_Enter(object? sender, EventArgs e) =>
            ClearTextBoxSelection();

        private void ClearTextBoxSelection()
        {
            textBoxDescription.SelectionStart = 0;
            textBoxDescription.SelectionLength = 0;
        }

        private void LoadAboutLogo()
        {
            var bitmap = AppIcons.LoadBitmap();
            if (bitmap == null)
                return;

            logoPictureBox.Image?.Dispose();
            logoPictureBox.Image = bitmap;
            logoPictureBox.BackColor = SystemColors.Control;
        }

        private static string AssemblyVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return version?.ToString(3) ?? "1.0.0";
            }
        }

        private static string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "MarkdownThing"
                    : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        private static string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0
                    ? $"Copyright © {CopyrightYears} {AuthorName}"
                    : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        private static string GetAboutText()
        {
            var sb = new StringBuilder();
            sb.AppendLine("MarkdownThing is free and open source software.");
            sb.AppendLine($"Copyright © {CopyrightYears} {AuthorName}");
            sb.AppendLine();
            sb.AppendLine("Licensed under the MIT License.");
            sb.AppendLine($"Project: {AppConstants.RepositoryUrl}");
            sb.AppendLine("See LICENSE in the application folder for the full license text.");
            sb.AppendLine();
            sb.AppendLine(new string('-', 72));
            sb.AppendLine();
            sb.Append(GetThirdPartyNotices());
            return sb.ToString();
        }

        private static string GetThirdPartyNotices()
        {
            try
            {
                var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (exePath != null)
                {
                    var noticesPath = Path.Combine(exePath, "THIRD-PARTY-NOTICES.txt");
                    if (File.Exists(noticesPath))
                        return File.ReadAllText(noticesPath);
                }
            }
            catch
            {
                // Fall through to default text
            }

            return GetDefaultThirdPartyNotices();
        }

        private static string GetDefaultThirdPartyNotices()
        {
            return """
                THIRD-PARTY SOFTWARE NOTICES AND INFORMATION

                MarkdownThing uses the following open source packages:

                1. Markdig (BSD-2-Clause)
                   https://github.com/xoofx/markdig
                   Copyright (c) Alexandre Mutel

                2. Microsoft.Web.WebView2 (BSD-3-Clause)
                   https://www.nuget.org/packages/Microsoft.Web.WebView2
                   Copyright (c) Microsoft Corporation

                3. PuppeteerSharp (MIT)
                   https://github.com/hardkoded/puppeteer-sharp
                   Copyright (c) Darío Kondratiuk

                4. DocumentFormat.OpenXml (MIT)
                   https://github.com/dotnet/Open-XML-SDK
                   Copyright (c) .NET Foundation and Contributors

                See THIRD-PARTY-NOTICES.txt in the application folder for full license texts.
                """;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LinkLabelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = e.Link?.LinkData?.ToString() ?? AppConstants.RepositoryUrl;
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open:\n{url}\n\n{ex.Message}",
                    "Open link", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
