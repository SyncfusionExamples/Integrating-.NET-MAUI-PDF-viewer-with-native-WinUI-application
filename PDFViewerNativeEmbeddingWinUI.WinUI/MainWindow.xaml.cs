using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.Maui.PdfViewer;
using System.Reflection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PDFViewerNativeEmbeddingWinUI.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Microsoft.UI.Xaml.Window
    {
        public static readonly Lazy<MauiApp> MauiApp = new(() =>
        {
            var mauiApp = MauiProgram.CreateMauiApp(builder =>
            {
                builder.UseMauiEmbedding();
            });
            return mauiApp;
        });

        public static bool UseWindowContext = true;

        SfPdfViewer pdfViewer;

        public MainWindow()
        {
            this.InitializeComponent();

            // Ensure .NET MAUI app is built before creating the PDF viewer.
            var mauiApp = MainWindow.MauiApp.Value;

            // Create .NET MAUI context
            var mauiContext = UseWindowContext
                ? mauiApp.CreateEmbeddedWindowContext(this) // Create window context
                : new MauiContext(mauiApp.Services);        // Create app context

            // Create .NET MAUI PDF viewer
            pdfViewer = new SfPdfViewer();

            //Set document source for the PDF viewer.
            pdfViewer.DocumentSource = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("PDFViewerNativeEmbeddingWinUI.WinUI.Assets.PDF_Succinctly.pdf");

            // Convert the .NET MAUI PDF viewer to a native control
            var nativeView = pdfViewer.ToPlatformEmbedded(mauiContext);

            this.Content = nativeView;
        }
    }
}
