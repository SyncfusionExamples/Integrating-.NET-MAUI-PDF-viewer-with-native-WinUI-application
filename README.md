# Integrating .NET MAUI PDFViewer with native WinUI embedding application
To create .NET MAUI PDFViewer in a native embedded WinUI application, you need to follow a series of steps. This article will guide you throught the process.

**Step 1:**

Before creating a WinUI project, create a .NET MAUI class library project and delete the Platforms folder and the Class1.cs file from it. Then add the classes named **EmbeddedExtensions**, **EmbeddedPlatformApplication**, **EmbeddedWindowHandler**, and **EmbeddedWindowProvider** to the created .NET MAUI class library project. The required code for these classes are available [here](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/native-embedding?view=net-maui-8.0&pivots=devices-windows#create-extension-methods). 

**Step 2:**

Next in the same solution, create a .NET MAUI single project. This project is used to register the handlers required to render the PDF viewer control. After including the project in the solution, follow the steps mentioned [here](https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/native-embedding?view=net-maui-8.0&pivots=devices-windows#create-a-net-maui-single-project).

**Step 3:**

Install the [Syncfusion.Maui.PdfViewer](https://www.nuget.org/packages/Syncfusion.Maui.PdfViewer) nuget package from nuget.org into the created .NET MAUI single project. 

**Step 4:**

Register the Syncfusion&reg; core handler in the **MauiProgram.cs** file of the .NET MAUI single project by calling the **ConfigureSyncfusionCore** method. 

 
 ```csharp
public static MauiApp CreateMauiApp<TApp>(Action<MauiAppBuilder>? additional = null) where TApp : App
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<TApp>()
        .ConfigureSyncfusionCore()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

    #if DEBUG
    builder.Logging.AddDebug();
    #endif

    additional?.Invoke(builder);

    return builder.Build();
} 
 ```

**Step 5:**

Create the native WinUI application project in which you want to add the PDF viewer and install the [Syncfusion.Maui.PdfViewer](https://www.nuget.org/packages/Syncfusion.Maui.PdfViewer) nuget package from nuget.org.

**Step 6:**

In the .csproj file of the native WinUI project, add `<UseMaui>`, `<MauiEnablePlatformUsings>` and `<EnableDefaultXamlItems>` tags and set the respective values as given below. 

 
 ```xml
<PropertyGroup> 
    <Nullable>enable</Nullable> 
    <ImplicitUsings>enable</ImplicitUsings> 
    <UseMaui>true</UseMaui>
    <MauiEnablePlatformUsings>true</MauiEnablePlatformUsings>    
    <EnableDefaultXamlItems>false</EnableDefaultXamlItems>
</PropertyGroup> 
 ```

**Step 7:**

Initialize .NET MAUI in the native WinUI project by creating a **MauiApp** object and using the **UseMauiEmbedding** method in the **MainWindow.xaml.cs** file as follows. 

 
 ```csharp
public static readonly Lazy<MauiApp> MauiApp = new(() =>
{
    var mauiApp = MauiProgram.CreateMauiApp(builder =>
    {
        builder.UseMauiEmbedding();
    });
    return mauiApp;
});

public static bool UseWindowContext = true;

public MainWindow()
{
    this.InitializeComponent();

    var mauiApp = MainWindow.MauiApp.Value;

    var mauiContext = UseWindowContext
        ? mauiApp.CreateEmbeddedWindowContext(this)
        : new MauiContext(mauiApp.Services); 
} 
 ```

**Step 8:**

Create a new folder with the name “Assets” in the WinUI app project and include the PDF document to be loaded, inside the folder. Set the **Build Action** of the PDF file to **Embedded Resource**. In this example, a PDF named “PDF_Succinctly.pdf” is used. 

**Step 9:**

Create the .NET MAUI PDF viewer control, convert it to a native view and add it as the **Content** of the **MainWindow**.

 
 ```csharp
SfPdfViewer pdfViewer;
public MainWindow()
 {
     ….
     ….
    // Create .NET MAUI PDF viewer
    pdfViewer = new SfPdfViewer();
    
    //Set document source for the PDF viewer.
    pdfViewer.DocumentSource = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("PDFViewerNativeEmbeddingWinUI.WinUI.Assets.PDF_Succinctly.pdf");
    
    // Convert the .NET MAUI PDF viewer to a native control
    var nativeView = pdfViewer.ToPlatformEmbedded(mauiContext);
    
    this.Content = nativeView; 

} 
 ```

**Step 10:**

In the solution, set the native WinUI project as the start-up project. Build, deploy and run the project. 

[View the sample on GitHub](https://github.com/SyncfusionExamples/Integrating-.NET-MAUI-PDF-viewer-with-native-WinUI-application)

**Conclusion**

We hope you enjoyed learning how to integrate .NET MAUI PDF Viewer in a native WinUI application.

Refer to our [.NET MAUI PDF Viewer’s feature tour](https://www.syncfusion.com/maui-controls/maui-pdf-viewer) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI PDF Viewer Documentation](https://help.syncfusion.com/maui/pdf-viewer/getting-started) to understand how to present and manipulate data.

For current customers, check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI PDF Viewer and other .NET MAUI components.

Please let us know in the following comments if you have any queries or require clarifications. You can also contact us through our [support forums](https://www.syncfusion.com/downloads/maui), [support ticket](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui). We are always happy to assist you!