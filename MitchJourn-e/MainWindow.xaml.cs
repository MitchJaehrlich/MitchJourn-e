﻿using MitchJourn_e.Properties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using Application = System.Windows.Application;
using TextBox = System.Windows.Controls.TextBox;
using Control = System.Windows.Controls.Control;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;
using System.Windows.Forms;
using System.Text;
using System.Collections.Specialized;
using System.Drawing.Imaging;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using MetadataExtractor;
using Directory = System.IO.Directory;
using System.Globalization;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;
using Color = System.Windows.Media.Color;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MitchJourn_e.Windows;
using Clipboard = System.Windows.Clipboard;
using MitchJourn_e.Classes;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Wpf.Ui.Common;
using OpenAI_API.Completions;
using System.Threading;

namespace MitchJourn_e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        System.Timers.Timer timer;
        string lastInvokeImage = "";
        string lastAutomaticImage = "";
        string lastAutomaticImage2 = "";
        public int currentInvokeProcessID = 0;
        public int currentAutomaticProcessID = 0;
        public int currentInvokeWebProcessID = 0;
        public Process rendererProcess;
        public Process invokeAIWebProcess;
        public Process automaticWebProcess;
        bool isFirstRun = true;
        Random rand = new();
        string globalPrompt = "";
        string globalNegativePrompt = "";
        bool windowClosing = false;
        List<string> tempClipboardImagePaths = new();
        BitmapSource clipboardImage;
        RandomWord randomWord = new();
        string[]? promptFolderFiles;
        int promptFolderIndex = -1;

        public System.Windows.Window imageViewer = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSettings();
            InitializePromptHelper();
            InitializePromptBubbles();
            StartRendering();
            UpdateCreativity();
            ScanModels();

            if (Debugger.IsAttached)
                Settings.Default.Reset();
            isFirstRun = false;
        }

        /// <summary>
        /// Go button
        /// </summary>
        private void btn_Go_Click(object sender, RoutedEventArgs e)
        {
            if ((string)lbl_Status.Content == "Stopped continuously prompting.")
            {
                chk_ContinuouslyPrompt.IsChecked = true;
                StartRendering();
            }
            else
            {
                StartRendering();
            }
        }

        /// <summary>
        /// Request to start rendering
        /// </summary>
        /// <param name="promptText">If promptText is "", render using the user provided prompt</param>
        /// <param name="incrementSeed">Use the next seed?</param>
        public void RequestRendering(string promptText = "", bool incrementSeed = true) 
        {
            StartRendering(promptText, incrementSeed);
        }

        /// <summary>
        /// Start the Python script and create the specified image
        /// </summary>
        /// <param name="promptText">Leave blank to use the prompt text box</param>
        /// <param name="incrementSeed">Use the next seed number for this image, chk_IncrementSeed must also be checked</param>
        private async void StartRendering(string promptText = "", bool incrementSeed = true)
        {
            // start windows commandline process, or use the existing one
            Process process = new Process();
            bool useOldCMD = rendererProcess != null && !rendererProcess.HasExited;
            if (useOldCMD)
            {
                process = rendererProcess;
            }


            Application.Current.Dispatcher.Invoke((Action)async delegate
            {
                string prompt = CleanPrompt(ConvertPromptBubblesToString()); //CleanPrompt(txt_Prompt.Text);
                string promptHelper = CleanPrompt(txt_PromptHelper.Text);
                string negativePrompt = CleanPrompt(txt_NegativePrompt.Text);
                string promptSettings = "";
                string imagePrompt = "";
                string seamlessTile = "";
                string showProgress = "";
                string seed = txt_Seed.Text;
                string uprez = "";
                string highRezFix = "";
                double imageWidth = SafeInt(txt_Width.Text);
                double imageHeight = SafeInt(txt_Height.Text);

                globalPrompt = CleanPrompt(globalPrompt);
                globalNegativePrompt = CleanPrompt(globalNegativePrompt);

                lbl_Status.Content = "Loading...";

                if (promptFolderFiles != null)
                {
                    lbl_Status.Content = $"Loading image Sequence {promptFolderIndex}/{promptFolderFiles.Length}";
                }

                if (promptText != "")
                {
                    prompt = promptText;
                }

                // Seed
                if (seed == "random")
                {
                    seed = "" + DateTime.Now.Hour +
                        DateTime.Now.Minute +
                        DateTime.Now.Second +
                        DateTime.Now.Millisecond;
                }
                else if ((bool)chk_IncrementSeed.IsChecked && incrementSeed)
                {
                    int.TryParse(seed, out int seedAsInt);

                    seed = (seedAsInt + 1).ToString();
                    txt_Seed.Text = seed;
                }

                // Upscale
                if ((bool)chk_HighRes.IsChecked)
                {
                    //uprez = $"-U {Settings.Default["gfpganUprezScale"]} -G {Settings.Default["gfpganScale"]}"; //--save_orig
                    highRezFix = "--hires_fix";
                    imageWidth *= SafeDouble(txt_UpscaleFactor.Text);
                    imageHeight *= SafeDouble(txt_UpscaleFactor.Text);
                }

                // Image Prompt
                if (txt_ImagePrompt.Text != "")
                {
                    if (float.TryParse(txt_ImagePromptWeight.Text, out float imageWeight))
                    {
                        imagePrompt += $"-I \"{txt_ImagePrompt.Text.Replace(@"'", @"").Replace("\"", @"")}\"";
                        imagePrompt += $" --strength {1 - imageWeight}";
                    }
                }

                // SeamlessTile
                if ((bool)chk_seamlessTile.IsChecked)
                {
                    seamlessTile = "--seamless";
                }

                // Show progress
                if (Settings.Default["SaveProgress"].ToString() == "1")
                {
                    showProgress = "--save_intermediates 1";
                }

                // highrez fix
                if ((bool)chk_HighrezFix.IsChecked)
                {
                    highRezFix = "--hires_fix";
                }

                promptSettings = "" +
                        $"-W {imageWidth} " +
                        $"-H {imageHeight} " +
                        $"-C {Settings.Default["Scale"]} " +
                        $"-S {seed} " +
                        $"-s {Settings.Default["Steps"]} " +
                        $"-n {Settings.Default["Iter"]} " +
                        $"{imagePrompt} " +
                        $"{uprez} " +
                        $"--sampler {Settings.Default["SamplerType"]} " +
                        $"--threshold {txt_Limiter.Text} --perlin {txt_Noise.Text} " +
                        $"{seamlessTile} " +
                        $"{showProgress} " +
                        $"{highRezFix}";

                // create the process properties
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                // check if there is already a cmd window running from this program
                if (!useOldCMD)
                {
                    // start a new cmd prompt
                    process = Process.Start(processStartInfo);

                    //currentProcessName = process.MainWindowTitle;
                    currentInvokeProcessID = process.Id;
                    rendererProcess = process;
                    //textWriter = process.StandardInput;
                    string sampler = Settings.Default["SamplerType"].ToString();
                    string safetyChecker = "";
                    string useFullPrecision = "";

                    // move the cmd directory to the main stable diffusion path and open the python environment called invokeAI (environment used at python install)
                    string prerequisites = $"cd /d {Settings.Default["MainPath"]} & call .venv\\Scripts\\activate.bat &";

                    // Full Precision
                    if (Settings.Default["UseFullPrecision"].ToString() == "1")
                    {
                        useFullPrecision = "--full_precision";
                    }

                    // safely checker
                    if (Settings.Default["SafetyChecker"].ToString() == "1")
                    {
                        safetyChecker = "--nsfw_checker";
                    }

                    // send the command to the CMD window to start the python script, enable the upsampler
                    process.StandardInput.WriteLine($"{prerequisites} python .venv\\scripts\\invoke.py --png_compression 3 --sampler {sampler} {safetyChecker}");

                    // dropped support for custom gfpgan settings
                    // --gfpgan_bg_tile {Settings.Default["gfpganBgTileSize"]} --gfpgan_upscale {Settings.Default["gfpganUprezScale"]} --gfpgan_bg_upsampler realesrgan {useFullPrecision}" +
                    //$" --gfpgan --gfpgan_dir GFPGAN --gfpgan_model_path 'experiments\\pretrained_models\\GFPGANv1.4.pth'

                    if (isFirstRun)
                    {
                        if (Settings.Default["EnableWelcomePrompt"].ToString() == "1")
                        {
                            process.StandardInput.WriteLine($"Welcome -S 13{seed}37 -s 25");
                        }
                        else
                        {
                            lbl_Status.Content = "Enter a prompt and press go!";
                        }
                        //isFirstRun = false;
                    }
                    else
                    {

                        process.StandardInput.WriteLine($"{globalPrompt} {GatherPromptBubbles()} ({promptHelper}){txt_promptHelperPower.Text} [{globalNegativePrompt}] [({negativePrompt}){txt_negativePromptHelperPower.Text}] {promptSettings}");

                    }
                }
                else // if the CMD window is already opened, send the prompt
                {
                    if ((bool)chk_OutPainting.IsChecked)
                    {
                        process.StandardInput.WriteLine($"!fix {txt_OutPaintImage.Text} --outcrop {txt_OutPaintDirection.Text}");
                    }
                    else
                    {
                        process.StandardInput.WriteLine($"{globalPrompt} {GatherPromptBubbles()} ({promptHelper}){txt_promptHelperPower.Text} [{globalNegativePrompt}] [({negativePrompt}):{txt_negativePromptHelperPower.Text}] {promptSettings}");
                    }
                }

                // start or restart the timer to check for a new image in the output directory
                if (timer != null)
                    timer.Dispose();
                timer = new(interval: 1000);
                timer.Elapsed += (sender, e) => DisplayImage();
                timer.Start();

                //lastPrompt = txt_Prompt.Text;
            });

        }

        RenderedImage GetLastRenderedImage()
        {
            RenderedImage renderedImage = (RenderedImage)((Image)stack_Images.Items[stack_Images.Items.Count - 1]).Tag;

            if (renderedImage == null)
            {
                renderedImage = new RenderedImage();
            }

            return renderedImage;
        }

        /// <summary>
        /// Checks the output directory and displays the last rendered image
        /// </summary>
        void DisplayImage(string promptRAW = "", bool hasImagePrompt = false)
        {
            if (!windowClosing)
            {
                // check InvokeAI image output path, then check Automatic111 image output path
                string[] imageDirectories = { $"{Settings.Default["OutputPath"]}", $"{Settings.Default["AutomaticMainPath"]}\\outputs\\txt2img-images", $"{Settings.Default["AutomaticMainPath"]}\\outputs\\img2img-images" };

                for (int i = 0; i < imageDirectories.Length; i++)
                {
                    string imageDirectory = imageDirectories[i];

                    //string imageDirectory = $"{Settings.Default["OutputPath"]}";

                    if (Directory.Exists(imageDirectory))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(imageDirectory);

                        FileInfo[] Files = directoryInfo.GetFiles("*.png"); // Get all png files

                        if (Files.Length > 0)
                        {
                            Application.Current.Dispatcher.Invoke((Action)delegate
                            {
                                try
                                {
                                    // Check if there is a new image in the folder
                                    string filePath = Files.Last().FullName;
                                    if ((filePath != lastInvokeImage && i == 0) || (filePath != lastAutomaticImage && i == 1) || (filePath != lastAutomaticImage2 && i == 2))
                                    {
                                        // Don't display an image from last boot
                                        if ((lastInvokeImage == "" && i == 0))
                                        {
                                            lastInvokeImage = filePath;
                                            return;
                                        }
                                        else if (lastAutomaticImage == "" && i == 1)
                                        {
                                            lastAutomaticImage = filePath;
                                            return;
                                        }
                                        else if (lastAutomaticImage2 == "" && i == 2)
                                        {
                                            lastAutomaticImage2 = filePath;
                                            return;
                                        }

                                        // create a bitmap from the image file
                                        BitmapImage myBitmapImage = new BitmapImage();
                                        myBitmapImage.BeginInit();
                                        myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                        myBitmapImage.UriSource = new Uri(filePath);
                                        myBitmapImage.EndInit();

                                        // Create the image using the bitmap
                                        Image output = new Image
                                        {
                                            Margin = new Thickness(8),
                                            Stretch = System.Windows.Media.Stretch.Uniform,
                                            Source = myBitmapImage,
                                            HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                                        };
                                        scroll_Images.MaxHeight = myBitmapImage.Height + 50;

                                        bool upscaledImage = false;

                                        // Create the renderedImage object and store useful metadata. This could be embedded in the png?
                                        RenderedImage renderedImage = new RenderedImage().CreateRenderedImage(output, filePath, txt_Prompt.Text + txt_NegativePrompt.Text, txt_Scale.Text, txt_Seed.Text,
                                            txt_Steps.Text, myBitmapImage.Width.ToString(), myBitmapImage.Height.ToString(), txt_ImagePrompt.Text, txt_ImagePromptWeight.Text, upscaledImage);

                                        output.Tag = renderedImage;

                                        if (imageViewer != null)
                                        {
                                            ((ImageViewer)imageViewer).DisplayImage(myBitmapImage, renderedImage);
                                        }

                                        // Add the right click menu to the image
                                        ContextMenu rightClickMenu = new ContextMenu();

                                        output.ContextMenu = rightClickMenu;

                                        // Create Variation
                                        MenuItem menuItemCreateVariation = new MenuItem();
                                        menuItemCreateVariation.Header = "Create Variations";
                                        menuItemCreateVariation.Tag = renderedImage;
                                        menuItemCreateVariation.Click += MenuItemCreateVariation_Click;
                                        rightClickMenu.Items.Add(menuItemCreateVariation);

                                        // Recreate prompt
                                        MenuItem menuItemRecreatePrompt = new MenuItem();
                                        menuItemRecreatePrompt.Header = "Recreate Prompt";
                                        menuItemRecreatePrompt.Tag = renderedImage;
                                        menuItemRecreatePrompt.Click += MenuItemRecreatePrompt_Click;
                                        rightClickMenu.Items.Add(menuItemRecreatePrompt);

                                        // Use as Image-To-Image
                                        MenuItem menuItemImageToImage = new MenuItem();
                                        menuItemImageToImage.Header = "Use as Image-To-Image";
                                        menuItemImageToImage.Tag = renderedImage;
                                        menuItemImageToImage.Click += menuItemImageToImage_Click;
                                        rightClickMenu.Items.Add(menuItemImageToImage);

                                        // ----
                                        rightClickMenu.Items.Add(new Separator());

                                        // Save As
                                        MenuItem menuItemSaveAs = new MenuItem();
                                        menuItemSaveAs.Header = "Save As";
                                        menuItemSaveAs.Tag = renderedImage.filePath;
                                        menuItemSaveAs.Click += MenuItemSaveAs_Click;
                                        rightClickMenu.Items.Add(menuItemSaveAs);

                                        // Save All
                                        MenuItem menuItemSaveAll = new MenuItem();
                                        menuItemSaveAll.Header = "Save All Images";
                                        menuItemSaveAll.Tag = renderedImage.filePath;
                                        menuItemSaveAll.Click += MenuItemSaveAll_Click; ;
                                        rightClickMenu.Items.Add(menuItemSaveAll);

                                        // Get File Path
                                        MenuItem menuItemGetFilePath = new MenuItem();
                                        menuItemGetFilePath.Header = "Get File Path";
                                        menuItemGetFilePath.Tag = renderedImage.filePath;
                                        menuItemGetFilePath.Click += MenuItemGetFilePath_Click;
                                        rightClickMenu.Items.Add(menuItemGetFilePath);

                                        // Open Containing Folder
                                        MenuItem menuItemOpenContainingFolder = new MenuItem();
                                        menuItemOpenContainingFolder.Header = "Open Containing Folder";
                                        menuItemOpenContainingFolder.Tag = renderedImage.filePath;
                                        menuItemOpenContainingFolder.Click += MenuItemOpenContainingFolder_Click;
                                        rightClickMenu.Items.Add(menuItemOpenContainingFolder);

                                        // ----
                                        rightClickMenu.Items.Add(new Separator());

                                        // Delete
                                        MenuItem menuItemDelete = new MenuItem();
                                        menuItemDelete.Header = "Delete";
                                        menuItemDelete.Tag = renderedImage;
                                        menuItemDelete.Click += MenuItemDelete_Click;
                                        rightClickMenu.Items.Add(menuItemDelete);

                                        // Delete All
                                        MenuItem menuItemDeleteAll = new MenuItem();
                                        menuItemDeleteAll.Header = "Delete All Images";
                                        menuItemDeleteAll.Tag = renderedImage;
                                        menuItemDeleteAll.Click += MenuItemDeleteAll_Click; ;
                                        rightClickMenu.Items.Add(menuItemDeleteAll);

                                        // Add the image to the top of the image stack
                                        stack_Images.Items.Insert(0, output);

                                        if (i == 0)
                                        {
                                            lastInvokeImage = filePath;
                                        }
                                        else if (i == 1)
                                        {
                                            lastAutomaticImage = filePath;
                                        }
                                        else if (i == 2)
                                        {
                                            lastAutomaticImage2 = filePath;
                                        }

                                        // If trying to create variations of upscalled images (too large to recreate image size)
                                        if (lbl_Status.Content.ToString() != "Can't set upscaled image as image source, creating downrezed version instead...")
                                        {
                                            lbl_Status.Content = $"Created image from seed {txt_Seed.Text}";
                                        }
                                        else
                                        {
                                            lbl_Status.Content = "Created downrezed version.";
                                        }

                                        // sequential prompting
                                        if ((bool)chk_SequentialPrompting.IsChecked)
                                        {
                                            txt_ImagePrompt.Text = renderedImage.filePath;
                                        }

                                        // Folder Prompt feature
                                        if (promptFolderFiles != null && promptFolderIndex > promptFolderFiles.Length - 1)
                                        {
                                            lbl_Status.Content = $"Done creating image Sequence {promptFolderIndex}/{promptFolderFiles.Length}";
                                            promptFolderIndex = -1;
                                            promptFolderFiles = null;
                                            txt_ImagePrompt.Text = "";
                                            chk_ContinuouslyPrompt.IsChecked = false;
                                        }
                                        if (promptFolderFiles != null && promptFolderFiles.Length > 0 && promptFolderIndex <= promptFolderFiles.Length - 1)
                                        {
                                            txt_ImagePrompt.Text = promptFolderFiles[promptFolderIndex];
                                            promptFolderIndex++;
                                            chk_ContinuouslyPrompt.IsChecked = true;
                                        }

                                        // continuous prompting
                                        if ((bool)chk_ContinuouslyPrompt.IsChecked)
                                        {
                                            StartRendering();
                                        }
                                    }

                                    // Clipboard feature
                                    if ((bool)chk_ClipboardPrompt.IsChecked)
                                    {
                                        BitmapSource currentClipboardImage = null;

                                        if (Clipboard.ContainsImage())
                                        {
                                            currentClipboardImage = Clipboard.GetImage();

                                            if (currentClipboardImage.PixelWidth > 64 && currentClipboardImage.PixelHeight > 64)
                                            {
                                                clipboardImage = currentClipboardImage;

                                                if (clipboardImage == null)
                                                {
                                                    clipboardImage = Clipboard.GetImage();
                                                }

                                                if (clipboardImage != null)
                                                {
                                                    // Create a temporary file to store the clipboard image data
                                                    // then put the path into the image prompt field.
                                                    // Store a reference to the temporary paths so they can be cleaned up on close.
                                                    string tempClipboardImagePath = Path.GetTempFileName();
                                                    using (var fileStream = new FileStream(tempClipboardImagePath, FileMode.Create))
                                                    {
                                                        BitmapEncoder encoder = new PngBitmapEncoder();
                                                        encoder.Frames.Add((BitmapFrame.Create(clipboardImage as BitmapSource)));
                                                        encoder.Save(fileStream);
                                                    }

                                                    tempClipboardImagePaths.Add(tempClipboardImagePath);

                                                    txt_ImagePrompt.Text = tempClipboardImagePath;
                                                }

                                            }
                                        }
                                    }
                                }
                                catch { return; }
                            });
                        }

                    }
                }

            }
        }

        private void MenuItemSaveAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FolderBrowserDialog SaveFolderDialog = new FolderBrowserDialog()
                {
                    RootFolder = Environment.SpecialFolder.MyPictures,
                };

                SaveFolderDialog.ShowDialog();

                if (SaveFolderDialog.SelectedPath != "")
                {
                    foreach (Image image in stack_Images.Items)
                    {
                        string imageFilePath = ((RenderedImage)image.Tag).filePath;
                        string[] fileParts = imageFilePath.Split('\\');
                        File.Copy(imageFilePath, $"{SaveFolderDialog.SelectedPath}\\{fileParts.Last()}");
                    }
                }
            }
            catch
            {
                lbl_Status.Content = "Failed to save png";
            }
        }

        private void MenuItemDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you would like to delete all these generated images?", "Confirm Deletion",
                           (MessageBoxButton)MessageBoxButtons.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (Image image in stack_Images.Items)
                    {
                        if (image.Tag is RenderedImage)
                        {
                            File.Delete(((RenderedImage)image.Tag).filePath);
                        }
                    }
                    foreach (string tempClipboardImagePaths in tempClipboardImagePaths)
                    {
                        File.Delete(tempClipboardImagePaths);
                    }
                    lastInvokeImage = "";
                    stack_Images.Items.Clear();
                }
            }
            catch
            {
                lbl_Status.Content = "Failed to delete all images.";
            }
        }

        private void InitializePromptHelper()
        {
            string[] presets = ((StringCollection)Settings.Default["PromptHelperPresets"]).Cast<string>().ToArray<string>();

            menuItem_PromptHelper.Items.Clear();
            stack_PromptHelperPresets.Children.Clear();

            foreach (string preset in presets)
            {
                try
                {
                    // add the presets to the prompt helper button
                    string[] directoriesAndValue = preset.Split('=');
                    string value = directoriesAndValue[1];
                    string allDirectories = directoriesAndValue[0];
                    string[] directories = allDirectories.Split('/');
                    string topDirectory = directories[0];
                    string menuHeader = directories[1];

                    MenuItem helperMenuItem = new MenuItem();
                    helperMenuItem.StaysOpenOnClick = true;

                    // check if it's a unique directory
                    bool isUniqueDirectory = true;
                    foreach (MenuItem menuItem in menuItem_PromptHelper.Items)
                    {
                        if (menuItem.Header.ToString() == topDirectory)
                        {
                            isUniqueDirectory = false;
                            helperMenuItem = menuItem;
                            break;
                        }
                    }

                    // add directory
                    if (isUniqueDirectory)
                    {
                        helperMenuItem.Header = topDirectory;
                        menuItem_PromptHelper.Items.Add(helperMenuItem);
                    }

                    MenuItem MenuItemPrompt = new MenuItem();
                    MenuItemPrompt.Header = menuHeader;
                    MenuItemPrompt.Tag = $"{topDirectory}/{value}";
                    MenuItemPrompt.Click += PromptHelperMenuItem_Click2;
                    MenuItemPrompt.StaysOpenOnClick = true;
                    helperMenuItem.Items.Add(MenuItemPrompt);

                    // add the prompt helper editor text boxes and buttons
                    StackPanel stackPanel = new StackPanel
                    {
                        Orientation = System.Windows.Controls.Orientation.Horizontal,
                        Tag = helperMenuItem
                    };
                    TextBox textBox = new TextBox
                    {
                        Text = preset,
                        FontSize = 16,
                        Padding = new Thickness(2),
                        Margin = new Thickness(4),
                        Width = 420,
                        Tag = menuHeader
                    };
                    textBox.TextChanged += PromptPresetTextChanged;
                    Button btn_DeletePromptHelperItem = new Button
                    {
                        Content = "Delete",
                        Padding = new Thickness(2),
                        Margin = new Thickness(4),
                        Tag = stackPanel
                    };
                    btn_DeletePromptHelperItem.Click += Btn_DeletePromptHelperItem_Click;
                    stackPanel.Children.Add(textBox);
                    stackPanel.Children.Add(btn_DeletePromptHelperItem);
                    stack_PromptHelperPresets.Children.Add(stackPanel);
                }
                catch { }
            }
        }

        /// <summary>
        /// Delete prompt helper preset
        /// </summary>
        private void Btn_DeletePromptHelperItem_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stack_PromptToDelete = (StackPanel)((Button)sender).Tag;
            MenuItem menuItem_TopDirectory = (MenuItem)stack_PromptToDelete.Tag;
            string presetValue = "";

            try
            {
                foreach (StackPanel stack in stack_PromptHelperPresets.Children)
                {
                    if (stack == stack_PromptToDelete && ((TextBox)stack.Children[0]).Tag != null)
                    {
                        stack.Visibility = Visibility.Collapsed;
                        presetValue = ((TextBox)stack.Children[0]).Tag.ToString();
                    }
                }
                foreach (MenuItem menuItemTopDirectories in menuItem_PromptHelper.Items)
                {
                    if (menuItemTopDirectories == menuItem_TopDirectory)
                    {
                        foreach (MenuItem menuItem in menuItemTopDirectories.Items)
                        {
                            if (presetValue == menuItem.Header.ToString())
                            {
                                menuItem.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
            catch { }
            InitializePromptHelper();
        }

        /// <summary>
        /// Removes characters from the prompt that would disallow the generation to run
        /// </summary>
        public string CleanPrompt(string promptText, bool processWildCards = true)
        {
            string output = "";

            if (promptText != null)
            {
                output = promptText;

                StringBuilder stringBuilder = new StringBuilder();
                //foreach (char c in promptText)
                for (int i = 0; i < promptText.Length; i++)
                {
                    char c = promptText[i];

                    // remove leading and trailing spaces
                    if ((i == 0 || i == promptText.Length - 1 ) && c == ' ')
                    {
                        continue;
                    }

                    // rebuild the string, adding back only the following allowed characters
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == '.' || c == '_' || c == ',' 
                        || c == '/' || c == '?' || c == '!' || c == '&' || c == '+' || c == '$' || c == '%' || c == '^' || c == '#' || c == '@'
                        || c == '(' || c == ')' || c == ':' || c == '-' || c == '\\' || c == '[' || c == ']' || c == '*')
                    {
                        stringBuilder.Append(c);
                    }
                }
                output = stringBuilder.ToString();

                if (processWildCards)
                {
                    while (output.Contains("*random*"))
                    {
                        var regex = new Regex(Regex.Escape("*random*"));
                        output = regex.Replace(output, randomWord.GetWord(), 1);
                    }
                }

                if ((bool)chk_AlternateToken.IsChecked)
                {
                    output = CreateAlternatePromptTokens(output);
                }
            }

            return output;
        }

        /// <summary>
        /// Changes the spaces to underscores to slightly change the tokens used to generate the image
        /// </summary>
        /// <param name="promptText"></param>
        /// <returns></returns>
        private string CreateAlternatePromptTokens(string promptText)
        {
            string output = "";

            if (promptText != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (char c in promptText)
                {
                    if (c == ' ')
                    {
                        stringBuilder.Append('_');
                    }
                    else if (c == '_')
                    {
                        stringBuilder.Append(' ');
                    }
                    else
                    {
                        stringBuilder.Append(c);
                    }
                }

                output = stringBuilder.ToString();
            }

            return output;
        }

        /// <summary>
        /// Add selected menuItem's name to the prompt box
        /// </summary>
        /// <param name="sender">MenuItem</param>
        private async void PromptHelperMenuItem_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox[] textBoxes = { txt_PromptHelper, txt_NegativePrompt };
                TextBox targetTextBox = textBoxes[0];
                string[] promptInfo = ((MenuItem)sender).Tag.ToString().Split('/');
                string promptCategory = promptInfo[0];
                string promptValue = "";
                bool isNegativePrompt = ((MenuItem)sender).Header.ToString().ToLower().Contains("negative");

                //((MenuItem)sender).Foreground = System.Windows.Media.Brushes.LightGray;

                for (int i = 1; i < promptInfo.Length; i++)
                {
                    if (i > 1)
                    {
                        promptValue += "/";
                    }
                    promptValue += promptInfo[i];
                }

                if (promptCategory.Contains("Negative"))
                {
                    //targetTextBox = textBoxes[1];
                    wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble(promptValue, 0, true));
                }
                else if (promptCategory.Contains("GPT3"))
                {
                    Application.Current.Dispatcher.Invoke((Action)async delegate
                    {
                        GPT3 gpt3 = new GPT3();
                        string userInput = txt_Prompt.Text;
                        RandomWord tempRandomWord = null;
                        if (promptValue.Contains("random"))
                        {
                            userInput = "(art, paintings, images, photography, video, cute, beauty, inspiration, wonder)1";
                            tempRandomWord = randomWord;
                        }
                        gpt3.Chat(promptValue, CleanPrompt(userInput), isNegativePrompt, tempRandomWord);
                    });
                }
                else
                {
                    wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble(promptValue));
                }

                //expander_settings.IsExpanded = true;
            }
            catch { return; }
        }

        /// <summary>
        /// Add selected menuItem's name to the prompt box
        /// </summary>
        /// <param name="sender">MenuItem</param>
        private void PromptHelperMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                char[] prompt = txt_Prompt.Text.ToCharArray();
                if (prompt.Last() == ' ')
                {
                    wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble($"{((MenuItem)sender).Header}"));
                    //txt_PromptHelper.Text += ((MenuItem)sender).Header;
                }
                else
                {
                    wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble($" {((MenuItem)sender).Header}"));
                    //txt_PromptHelper.Text += $" {((MenuItem)sender).Header}";
                }
                //expander_settings.IsExpanded = true;
            }
            catch { return; }
        }

        /// <summary>
        /// Right click menu item for images: Recreate Prompt
        /// </summary>
        public void MenuItemRecreatePrompt_Click(object sender, RoutedEventArgs e)
        {
            RenderedImage image = (RenderedImage)((MenuItem)sender).Tag;

            chk_ClipboardPrompt.IsChecked = false;
            txt_Prompt.Text = image.prompt;
            txt_Scale.Text = image.promptWeight;
            txt_Seed.Text = image.seed;
            txt_Steps.Text = image.diffusionSteps;
            txt_Width.Text = image.width;
            txt_Height.Text = image.height;
            txt_ImagePrompt.Text = image.imagePrompt;
            txt_ImagePromptWeight.Text = image.imagePromptWeight;
            ConvertStringToPromptBubbles(image.prompt);
            chk_ContinuouslyPrompt.IsChecked = false;
            chk_IncrementSeed.IsChecked = false;

            //StartRendering(image.prompt, false);
        }

        /// <summary>
        /// Right click menu item for Use as Image-To-Image
        /// </summary>
        public void menuItemImageToImage_Click(object sender, RoutedEventArgs e)
        {
            RenderedImage image = (RenderedImage)((MenuItem)sender).Tag;
            chk_ClipboardPrompt.IsChecked = false;
            txt_ImagePrompt.Text = image.filePath;
        }

        /// <summary>
        /// Right click menu item for images: Open Containing Folder
        /// </summary>
        public void MenuItemOpenContainingFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string folderPath = ((MenuItem)sender).Tag.ToString();
                Process.Start("explorer.exe", $"/select,\"{folderPath}\"");
            }
            catch { return; }
        }

        /// <summary>
        /// Right click menu item for images: Save As
        /// </summary>
        public void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string file = ((MenuItem)sender).Tag.ToString();
                string[] fileParts = file.Split('\\');
                
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    DefaultExt = "png",
                    FileName = $"{fileParts.Last()}",
                    Filter = "png|*.png"
                };
                if (saveFileDialog.ShowDialog() == true)
                    File.Copy(file, saveFileDialog.FileName);
            }
            catch
            {
                lbl_Status.Content = "Failed to save png";
            }
        }

        /// <summary>
        /// Right click menu item for images: Delete Image
        /// </summary>
        public void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RenderedImage renderedImage = (RenderedImage)((MenuItem)sender).Tag;

                foreach (Image image in stack_Images.Items)
                {
                    if (((RenderedImage)image.Tag).filePath.Equals(renderedImage.filePath))
                    {
                        image.Visibility = Visibility.Collapsed;
                        image.Tag = ((RenderedImage)image.Tag).filePath;
                        image.Source = null;

                        break;
                    }
                }
                lastInvokeImage = "";
                GC.Collect();
                File.Delete(renderedImage.filePath);
            }
            catch { }
        }

        /// <summary>
        /// Right click menu item for images: Create Variations
        /// </summary>
        public void MenuItemCreateVariation_Click(object sender, RoutedEventArgs e)
        {
            RenderedImage renderedImage = (RenderedImage)((MenuItem)sender).Tag;

            chk_ClipboardPrompt.IsChecked = false;
            txt_Prompt.Text = renderedImage.prompt;
            txt_Seed.Text = renderedImage.seed;
            txt_ImagePromptWeight.Text = renderedImage.imagePromptWeight;
            txt_ImagePrompt.Text = renderedImage.filePath;
            txt_Width.Text = renderedImage.width;
            txt_Height.Text = renderedImage.height;
            ConvertStringToPromptBubbles(renderedImage.prompt);
            

            if (lbl_Status.Content.ToString() == "Created downrezed version.")
            {
                chk_IncrementSeed.IsChecked = true;
                chk_HighRes.IsChecked = true;
            }

            if (!(bool)chk_ContinuouslyPrompt.IsChecked)
            {
                chk_ContinuouslyPrompt.IsChecked = true;
                StartRendering();
            }
            
        }

        public void MenuItemGetFilePath_Click(object sender, RoutedEventArgs e)
        {
            string renderedImage = ((MenuItem)sender).Tag.ToString();
            Clipboard.SetDataObject(renderedImage);
        }

        /// <summary>
        /// Close all the opened CMD windows
        /// </summary>
        /// <param name="processID"></param>
        private void StopRendering()
        {
            promptFolderFiles = null;
            int[] processIDs = { currentInvokeProcessID, currentAutomaticProcessID, currentInvokeWebProcessID };

            foreach (int processID in processIDs) {

                if (processID != 0)
                {
                    try
                    {
                        Process process = Process.GetProcessById(processID);
                        process.Kill();
                    }
                    catch
                    {
                        // Failed to kill the specified process
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Stop button click event
        /// </summary>
        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            promptFolderFiles = null;

            if (chk_ContinuouslyPrompt.IsChecked != true)
            {
                StopRendering();
            }
            else
            {
                chk_ContinuouslyPrompt.IsChecked = false;
                lbl_Status.Content = "Stopped continuously prompting.";
            }
        }

        /// <summary>
        /// Saves the setting if the sender is a TextBox with the setting property name as it's tag
        /// </summary>
        private void SettingsTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (((System.Windows.Controls.TextBox)sender).Tag != null)
                {
                    string settingValue = ((TextBox)sender).Text;
                    string setting = ((TextBox)sender).Tag.ToString();

                    // Save the setting from the value of the text box with the matching tag
                    foreach (SettingsProperty property in Settings.Default.Properties)
                    {
                        if (property.Name == setting)
                        {
                            Settings.Default[property.Name] = settingValue;
                            Settings.Default.Save();
                        }
                    }
                }
            }
            catch { return; }
        }

        private void SaveSettings()
        {
            try 
            { 
                
            }
            catch { return; }
        }

        /// <summary>
        /// Saves the setting if the sender is a TextBox with the property name as it's tag
        /// </summary>
        /// TODO: make it not hurt anymore
        private void PromptPresetTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<string> presets = new List<string>();
                foreach (StackPanel stack in stack_PromptHelperPresets.Children)
                {
                    foreach (Control control in stack.Children)
                    {
                        if (control is TextBox)
                        {
                            string text = ((TextBox)control).Text;

                            if (text.Split('/').Length > 1 && text.Split('=').Length == 2)
                            {
                                presets.Add(text);
                            }
                        }
                    }
                }
                StringCollection stringCollection = new StringCollection();
                stringCollection.AddRange(presets.ToArray());
                Settings.Default["PromptHelperPresets"] = stringCollection;
                Settings.Default.Save();
            }
            catch 
            {
                return; 
            }
        }

        /// <summary>
        /// Save all settings text box values to the program's setting registry
        /// </summary>
        private void SaveAllSettings()
        {
            foreach (SettingsProperty property in Settings.Default.Properties)
            {
                foreach (Control control in stack_settings.Children)
                {
                    if (control is TextBox)
                    {
                        if (((TextBox)control).Tag == property.Name)
                        {
                            Settings.Default[property.Name] = ((TextBox)control).Text;
                        }
                    }
                }
            }
            Settings.Default.Save();
        }

        /// <summary>
        /// Loads the settings text boxes with saved propery values
        /// </summary>
        private void InitializeSettings()
        {
            if (Settings.Default.FirstTimeRunningThisVersion)
            {
                Settings.Default.Upgrade();
                Settings.Default.FirstTimeRunningThisVersion = false;
                Settings.Default.Save();
            }

            List<TextBox> textBoxes = new List<TextBox>();
            TextBox[] otherTextBoxes = new TextBox[] { txt_Scale, txt_Steps, txt_ImagePromptWeight };
            Slider[] otherTextBoxesSliders = new Slider[] { slider_PromptWeight, slider_Steps, slider_imagePromptWeight };

            // Get all the settings text boxes from advanced settings
            foreach (Control control in stack_settings.Children)
            {
                if (control is TextBox)
                {
                    textBoxes.Add((TextBox)control);
                }
            }
            // Get all the settings text boxes from the installation tab
            foreach (Control control1 in stack_InstallPaths.Children)
            {
                if (control1 is TextBox)
                {
                    textBoxes.Add((TextBox)control1);
                }
            }

            // Check all the settings text boxes and fill in the applicable value from saved properties
            foreach (SettingsProperty property in Settings.Default.Properties)
            {
                foreach (TextBox textBox in textBoxes)
                {
                    if (textBox.Tag != null)
                    {
                        if (property.Name == textBox.Tag.ToString())
                        {
                            textBox.Text = Settings.Default[property.Name].ToString();
                        }
                    }
                }
            }

            for (int i = 0; i < otherTextBoxes.Length; i++)
            {
                string value = Settings.Default[otherTextBoxes[i].Tag.ToString()].ToString();
                otherTextBoxes[i].Text = value;
                otherTextBoxesSliders[i].Value = double.Parse(value);
            }

            txt_SortItems.Text = Settings.Default["SortList"].ToString();

            if ((bool)Settings.Default["EnableSortList"])
            {
                chk_SortOutputImagesByPrompt.IsChecked = true;
            }
        }

        private string GetImageMetaData(string imagePath)
        {
            string output = "";
            try
            {
                List<MetadataExtractor.Directory> directories = (List<MetadataExtractor.Directory>)ImageMetadataReader.ReadMetadata(CleanPrompt(imagePath));
                foreach (MetadataExtractor.Directory directory in directories)
                {
                    foreach (Tag tag in directory.Tags)
                    {
                        output += tag.ToString() + Environment.NewLine;
                    }
                }
            }
            catch
            {
                output = "Failed.";
            }
            return output;
        }

        /// <summary>
        /// Sets the width and height settings and saves to the settings registry
        /// </summary>
        private void setAspectRatio(string width, string height, object? sender = null)
        {
            txt_Width.Text = width;
            txt_Height.Text = height;
            SaveAllSettings();

            if (sender != null)
            {
                foreach (Control control in stack_aspectRatio.Children)
                {
                    if (control is Button)
                    {
                        ((Button)control).Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                    }
                }
            ((Button)sender).Background = new SolidColorBrush(Colors.AliceBlue);
            }
        }

        /// <summary>
        /// 1:1
        /// </summary>
        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio11"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        /// <summary>
        /// 3:2
        /// </summary>
        private void btn_32_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio32"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        /// <summary>
        /// 16:9
        /// </summary>
        private void btn_169_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio169"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        /// <summary>
        /// 2:3
        /// </summary>
        private void btn23_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio23"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        /// <summary>
        /// 9:20
        /// </summary>
        private void btn920_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] widthheight = Settings.Default["AspectRatio920"].ToString().Split(':');
                setAspectRatio(widthheight[0], widthheight[1], sender);
            }
            catch { }
        }

        private void slider_imagePromptWeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_ImagePromptWeight.Text = Math.Round(slider_imagePromptWeight.Value, 3).ToString();
        }

        private void btn_ClearImagePrompt_Click(object sender, RoutedEventArgs e)
        {
            txt_ImagePrompt.Text = "";
            chk_SequentialPrompting.IsChecked = false;
        }

        private void btn_ClearPromptHelper_Click(object sender, RoutedEventArgs e)
        {
            txt_PromptHelper.Text = "";
        }

        /// <summary>
        /// Detects the enter key and starts rendering when pressed, works while any control is focused
        /// </summary>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                StartRendering();
            }
        }

        private void btn_BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var FolderSelector = new FolderBrowserDialog())
            {
                DialogResult result = FolderSelector.ShowDialog();

                if (!string.IsNullOrWhiteSpace(FolderSelector.SelectedPath))
                {
                    txt_PromptFolder.Text = FolderSelector.SelectedPath;
                }
            }
        }

        private void btn_StartPromptFolder_Click(object sender, RoutedEventArgs e)
        {
            StartPromptFolderExperiment(txt_PromptFolder.Text);
        }

        void StartPromptFolderExperiment(string folderPath)
        {
            //if (File.Exists(folderPath))
            {
                try
                {
                    char[] invalidFilePathCharacters = System.IO.Path.GetInvalidPathChars();
                    string cleanFolderPath = String.Join("_", folderPath.Split(invalidFilePathCharacters, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');

                    //if (!folderPath.StartsWith("\"") || !folderPath.EndsWith("\"")) 
                    //{
                    //    folderPath = CleanPrompt(folderPath);
                    //}

                    chk_ContinuouslyPrompt.IsChecked = true;
                    promptFolderFiles = Directory.GetFiles(cleanFolderPath);
                    promptFolderIndex = 1;
                    txt_ImagePrompt.Text = promptFolderFiles[0];
                    StartRendering();
                }
                catch
                {
                    lbl_Status.Content = "Failed to find png files.";
                }
            }
        }
        private void btn_AddPromptPreset_Click(object sender, RoutedEventArgs e)
        {
            // add the prompt helper editor text boxes and buttons
            StackPanel stackPanel = new StackPanel
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                //Tag = helperMenuItem
            };
            TextBox textBox = new TextBox
            {
                Text = "",
                FontSize = 16,
                Padding = new Thickness(2),
                Margin = new Thickness(4),
                Width = 420,
                //Tag = menuHeader
            };
            textBox.TextChanged += PromptPresetTextChanged;
            Button btn_Delete = new Button
            {
                Content = "Delete",
                Padding = new Thickness(2),
                Margin = new Thickness(4),
                Tag = stackPanel
            };
            btn_Delete.Click += Btn_DeletePromptHelperItem_Click;
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(btn_Delete);
            stack_PromptHelperPresets.Children.Add(stackPanel);
        }

        private void btn_OpenInstallGuideClick(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo openWebsite = new ProcessStartInfo
            {
                FileName = "https://invoke-ai.github.io/InvokeAI/installation/INSTALL_SOURCE/",
                UseShellExecute = true
            };
            Process.Start(openWebsite);
        }

        private void btn_MitchGitHub_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo openWebsite = new ProcessStartInfo
            {
                FileName = "https://github.com/MitchJaehrlich/MitchJourn-e",
                UseShellExecute = true
            };
            Process.Start(openWebsite);
        }

        private void btn_StableDiffusionGitHub_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo openWebsite = new ProcessStartInfo
            {
                FileName = "https://github.com/CompVis/stable-diffusion",
                UseShellExecute = true
            };
            Process.Start(openWebsite);
        }

        private void btn_InvokeAIGitHub_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo openWebsite = new ProcessStartInfo
            {
                FileName = "https://github.com/invoke-ai/InvokeAI",
                UseShellExecute = true
            };
            Process.Start(openWebsite);
        }

        private void btn_ClearNegativePrompt_Click(object sender, RoutedEventArgs e)
        {
            txt_NegativePrompt.Text = "";
        }

        private void chk_HighRes_Checked(object sender, RoutedEventArgs e)
        {
            //firstUpscaleRequest = true;
            chk_HighrezFix.IsChecked = true;
        }

        private void btn_MetadataExtractor_Click(object sender, RoutedEventArgs e)
        {
            txt_extractedMetadata.Text = GetImageMetaData(txt_ImagePathMetaData.Text);
        }

        private void chk_SequentialPrompting_Unchecked(object sender, RoutedEventArgs e)
        {
            txt_ImagePrompt.Text = "";
        }

        private void chk_SequentialPrompting_Checked(object sender, RoutedEventArgs e)
        {
            string lastImageFilePath = "";

            chk_ClipboardPrompt.IsChecked = false;

            try
            {
                lastImageFilePath = ((RenderedImage)((Image)stack_Images.Items[0]).Tag).filePath;
            }
            catch { }
            txt_ImagePrompt.Text = lastImageFilePath;
        }

        private void myCanvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Render();
        }

        private void btn_StartInPainting_Click(object sender, RoutedEventArgs e)
        {
            //Render();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_Scale.Text = Math.Round(slider_PromptWeight.Value, 3).ToString();
        }

        private void slider_Steps_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_Steps.Text = Math.Round(slider_Steps.Value, 3).ToString();
        }

        private void slider_Creativity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt_Creativity.Text = Math.Round(slider_Creativity.Value, 3).ToString();
        }

        private void UpdateCreativity()
        {
            globalPrompt = "(incredibly detailed high quality)0.5";
            globalNegativePrompt = "(framed ugly tiling poorly drawn out of frame disfigured deformed blurry blurred watermark grainy signature cut off draft compressed)0.5";

            try
            {
                if (txt_Creativity != null && txt_Scale != null & txt_Limiter != null)
                {
                    double creativity = double.Parse(txt_Creativity.Text);
                    //double scale = double.Parse(txt_Scale.Text);
                    //double limiter = double.Parse(txt_Limiter.Text);
                    double scale = 0;
                    double limiter = 0;
                    double noise = 0;

                    if (creativity < 1) // 0-1
                    {
                        globalPrompt += "(f/1.4 50mm 200iso 4k photograph)+++";
                        globalNegativePrompt += "(cartoon anime art painting)+++";
                        limiter = 2;
                        scale = 15;
                    }
                    else if (creativity < 2) // 1-2
                    {
                        globalPrompt += "(f/1.4 50mm 200iso 4k photograph)++";
                        globalNegativePrompt += "(cartoon anime art painting)++";
                        scale = 12;
                    }
                    else if (creativity < 3) // 2-3
                    {
                        // high cfg
                        globalPrompt += "(f/1.4 50mm a photo of)+";
                        globalNegativePrompt += "(cartoon anime art painting)+";
                        scale = 10;
                    }
                    else if (creativity < 4) // 3-4
                    {
                        globalPrompt += "(a photo of)+";
                        globalNegativePrompt += "(cartoon anime painting)";
                        scale = 9;
                    }
                    else if (creativity < 5) // 4-5
                    {
                        globalPrompt += "(a photo of)";
                        globalNegativePrompt += "(cartoon anime)";
                        scale = 8;
                    }
                    else if (creativity < 6) // 5-6
                    {
                        globalPrompt += "";
                        globalNegativePrompt += "";
                        scale = 7.5;
                    }
                    else if (creativity < 7) // 6-7
                    {
                        globalPrompt += "(hyperdetailed masterpiece art *random*)";
                        globalNegativePrompt += "photo";
                        scale = 7;
                    }
                    else if (creativity < 8) // 7-8
                    {
                        globalPrompt += "(masterpiece amazing hyperdetailed award-winning art artistic creative *random*)+";
                        globalNegativePrompt += "photo";
                        scale = 6.5;
                    }
                    else if (creativity < 9) // 8-9
                    {
                        globalPrompt += "(masterpiece intricate amazing awesome splash-art award-winning hyperdetailed trending work-of-art incredible perfect creative *random*)++";
                        globalNegativePrompt += "photo";
                        scale = 6;
                    }
                    else if (creativity < 10.1) // 9-10
                    {
                        globalPrompt += "(magical wonderous masterpiece intricate amazing awesome splash-art award-winning hyperdetailed trending work-of-art incredible perfect creative *random*)+++";
                        globalNegativePrompt += "(photograph photo ugly out of focus blurry grainy noisy text writing watermark logo oversaturation over saturation over shadow *random*)+";
                        scale = 5.5;
                    }


                    //if (creativity >= 7)
                    //{
                    //    newScale = creativity * 0.15;
                    //    newNoise = Math.Max(6 - (10 - creativity * 0.5), 0);
                    //    globalPrompt = "(masterpiece intricate amazing awesome splash-art award-winning hyperdetailed trending work-of-art incredible perfect artistic creative)";


                    //    if (creativity >= 10)
                    //    {
                    //        globalPrompt += "+++";
                    //        globalNegativePrompt += "(photograph ugly out of focus blurry grainy noisy text writing watermark logo oversaturation over saturation over shadow)+";
                    //        //newScale -= 6;
                    //    }
                    //    else if (creativity >= 9)
                    //    {
                    //        globalPrompt += "++";
                    //        //newScale -= 5;
                    //    }
                    //    else if (creativity >= 8)
                    //    {
                    //        globalPrompt += "+";
                    //        //newScale -= 4;
                    //    }
                    //}
                    //else if (creativity >= 5)
                    //{
                    //    newScale = creativity * 0.8;
                    //    globalPrompt = "incredibly detailed";
                    //    globalNegativePrompt = "(framed, ugly, tiling, blurry, bad anatomy, blurred, watermark, grainy, signature, cut off, draft)0.5";
                    //}
                    //else if (creativity >= 6)
                    //{
                    //    newScale = creativity * 0.5;
                    //    globalPrompt = "(incredibly detailed)1.1";
                    //    globalNegativePrompt = "(framed, ugly, tiling, poorly drawn hands, poorly drawn feet, poorly drawn face, out of frame, extra limbs, disfigured, deformed, body out of frame, blurry, bad anatomy, blurred, watermark, grainy, signature, cut off, draft)0.5";
                    //}
                    //else if (creativity >= 3)
                    //{
                    //    newScale = Math.Max(creativity * 1.5, 5);
                    //}
                    //else
                    //{
                    //    newScale = 3 * (3.1 - creativity) * 10;
                    //    newLimiter = creativity * 0.75;
                    //    newNoise = Math.Max((1 - creativity / 3) * 3, 0);
                    //    globalPrompt = "(incredible photo of a)";
                    //    globalNegativePrompt = "(cartoon anime art painting)+";
                    //    if (creativity <= 2.5)
                    //    {
                    //        globalPrompt += "++";
                    //        globalNegativePrompt += "++";
                    //    }
                    //}

                    txt_Scale.Text = scale.ToString();
                    txt_Limiter.Text = limiter.ToString();
                    txt_Noise.Text = noise.ToString();
                }
            }
            catch { }
        }

        /// <summary>
        /// Attempts to change settings to make the generation more creative or realistic (similar to midjourney)
        /// </summary>
        private void txt_Creativity_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCreativity();
        }

        /// <summary>
        /// When exiting the app. Used to prompt for image sorting
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopRendering();
            windowClosing = true;

            foreach (string tempClipboardImagePaths in tempClipboardImagePaths)
            {
                File.Delete(tempClipboardImagePaths);
            }

            if ((bool)chk_SortOutputImagesByPrompt.IsChecked)
            {
                //if (MessageBox.Show("Start image output sorting?", "Confrim Sorting",
                //       (MessageBoxButton)MessageBoxButtons.YesNo) == MessageBoxResult.Yes)
                {
                    ImageSorter newWindow = new ImageSorter();
                }
            }

            Application.Current.Shutdown();
        }

        private void chk_SortOutputImagesByPrompt_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["EnableSortList"] = (bool)chk_SortOutputImagesByPrompt.IsChecked;
            Settings.Default.Save();
        }
        private void InitializePromptBubbles()
        {
            wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble("Enter your prompt here!"));
        }

        private string GatherPromptBubbles()
        {
            //List<PromptBubble> promptBubbles = new List<PromptBubble>();
            string prompt = "";

            foreach (Object control in wrp_PromptBubbles.Children)
            {
                if (control is StackPanel)
                {
                    StackPanel promptBubbleStack = (StackPanel)control;
                    
                    if (promptBubbleStack.Tag is PromptBubble)
                    {
                        PromptBubble promptBubble = (PromptBubble)promptBubbleStack.Tag;
                        char braceStart = '(';
                        char braceEnd = ')';

                        if (promptBubble.isNegativePrompt)
                        {
                            braceStart = '[';
                            braceEnd = ']';
                        }

                        prompt += $"{braceStart}{promptBubble.prompt}{braceEnd}{1+0.1*promptBubble.power} ";
                    }
                }
            }

            string cleanedPrompt = CleanPrompt(prompt);
            txt_Prompt.Text = cleanedPrompt;

            return cleanedPrompt;
        }

        public string ConvertPromptBubblesToString()
        {
            string output = "";

            List<PromptBubble> promptBubbleStacks = new List<PromptBubble>();
            foreach (Object control in wrp_PromptBubbles.Children)
            {
                try
                {
                    if (control is StackPanel)
                    {
                        StackPanel promptBubbleStack = (StackPanel)control;

                        promptBubbleStacks.Add((PromptBubble)promptBubbleStack.Tag);
                    }
                }
                catch { }
            }
            foreach (PromptBubble bubble in promptBubbleStacks)
            {
                try
                {
                    char braceStart = '(';
                    char braceEnd = ')';

                    if (bubble.isNegativePrompt)
                    {
                        braceStart = '[';
                        braceEnd = ']';
                    }

                    if (bubble.power > 0)
                    {
                        output += $"{braceStart}{bubble.prompt}{braceEnd}{Math.Round(1 + bubble.power * .1, 2)} ";
                    }
                    else if (bubble.power < 0)
                    {
                        output += $"{braceStart}{bubble.prompt}{braceEnd}{Math.Round((bubble.power * .1) + 1, 2)} ";
                    }
                    else
                    {
                        output += $"{braceStart}{bubble.prompt}{braceEnd}1 ";
                    }
                }
                catch { }
            }

            return output;
        }

        private void ConvertStringToPromptBubbles(string input)
        {
            string datalog = "";
            // Clear the existing prompt bubbles
            List<StackPanel> promptBubbleStacks = new List<StackPanel>();
            foreach (Object control in wrp_PromptBubbles.Children)
            {
                if (control is StackPanel)
                {
                    StackPanel promptBubbleStack = (StackPanel)control;

                    if (promptBubbleStack.Tag is PromptBubble)
                    {
                        promptBubbleStacks.Add(promptBubbleStack);
                    }
                }
            }
            foreach (StackPanel stack in promptBubbleStacks)
            {
                wrp_PromptBubbles.Children.Remove(stack);
            }

            // create a container for all the new prompt bubbles
            int promptBubbleIndex = 0;
            List<StackPanel> promptBubbles = new List<StackPanel>();

            datalog += "Processing starts" + Environment.NewLine;
            string[] prompts = input.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> negativePrompts = new List<string>();
            List<string> positivePrompts = new List<string>();

            foreach (string prompt in prompts)
            {
                if (prompt.Contains('[') && prompt.Contains(']'))
                {
                    string[] startBrace = prompt.Split(new char[] { '[' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string part in startBrace)
                    {
                        string[] endBrace = part.Split(new char[] { ']' }, StringSplitOptions.RemoveEmptyEntries);

                        if (endBrace.Length > 1)
                        {
                            negativePrompts.Add(endBrace[0].ToString());
                            negativePrompts.Add(endBrace[1].ToString());
                        }
                        else
                        {
                            positivePrompts.Add(endBrace[0].ToString());
                        }
                    }
                }
                else
                {
                    positivePrompts.Add(prompt);
                }
            }

            
            datalog += "String segments: " + prompts.Length + Environment.NewLine;

            foreach (string prompt in positivePrompts)
            //for (int i = 0; i < prompts.Length; i++)
            {
                //datalog += "Segment: " + i + Environment.NewLine;
                string reformedPrompt = "";
                PromptBubble promptBubble = new PromptBubble();

                //if (!allSplitPrompts[prompt])
                //{
                //    promptBubble.isNegativePrompt = true;
                //}

                // if the bubble has + notation, add power to the previous prompt
                // note: doesn't support - notation, as common prompts include this, without meaning decrease power
                if (prompt.Contains("+"))
                {
                    char[] chars = prompt.ToCharArray();
                    foreach (char c in chars)
                    {
                        if (c == '+')
                        {
                            PromptBubble lastPromptBubble = (PromptBubble)((StackPanel)promptBubbles.Last()).Tag;
                            lastPromptBubble.power++;
                            lastPromptBubble.Tag = lastPromptBubble;
                            promptBubbles.RemoveAt(promptBubbles.Count - 1);
                            promptBubbles.Add(lastPromptBubble.CreatePromptBubble(lastPromptBubble.prompt, lastPromptBubble.power, lastPromptBubble.isNegativePrompt));
                        }
                        else
                        {
                            reformedPrompt += c;
                        }
                    }
                }

                // if the bubble has numeric notation, add power to the previous prompt
                if (reformedPrompt == "")
                {
                    reformedPrompt = prompt;
                }
                //if (reformedPrompt.Contains("1."))
                if (promptBubbles.Count > 0)
                {
                    datalog += "Not first prompt bubble" + Environment.NewLine;
                    string[] promptParts = reformedPrompt.Split(" ");
                    reformedPrompt = "";
                    double finalPower = 0;

                    PromptBubble lastPromptBubble = (PromptBubble)((StackPanel)promptBubbles.Last()).Tag;

                    int j = 0;
                    foreach (string promptPart in promptParts)
                    {
                        bool isDouble = double.TryParse(promptPart, out double power);
                        if (!isDouble && promptPart != "" && promptPart != " ")
                        {
                            datalog += $"Adding Prompt part: {promptPart} " + Environment.NewLine;
                            reformedPrompt += promptPart + " ";
                        }
                        else if (isDouble && j == 0)
                        {
                            if (power > 1)
                            {
                                finalPower = Math.Round((power - 1) * 10, 1);
                            }
                            else if (power < 1)
                            {
                                finalPower = Math.Round((1-power) * -10, 1);
                            }
                            else if (power == 1)
                            {
                                finalPower = 0;
                            }
                            lastPromptBubble.power = (int)finalPower;
                            lastPromptBubble.Tag = lastPromptBubble;
                            promptBubbles.RemoveAt(promptBubbles.Count - 1);
                            promptBubbles.Add(lastPromptBubble.CreatePromptBubble(lastPromptBubble.prompt,lastPromptBubble.power, lastPromptBubble.isNegativePrompt));
                            datalog += $"Adding power: {finalPower}" + Environment.NewLine;
                            j++;
                            break;
                        }
                        j++;
                    }                    
                }
                else
                {
                    datalog += "First prompt bubble! " + reformedPrompt + Environment.NewLine;
                }

                StackPanel promptBubbleStack = promptBubble.CreatePromptBubble(reformedPrompt);
                if (promptBubbleStack.Tag is PromptBubble &&
                    reformedPrompt != "")
                {
                    // create the new UI object for the bubble
                    promptBubbles.Add(promptBubbleStack);
                    promptBubbleIndex++;
                }

                datalog += "------end-------" + Environment.NewLine;
            }

            foreach (string prompt in negativePrompts)
            //for (int i = 0; i < prompts.Length; i++)
            {
                //datalog += "Segment: " + i + Environment.NewLine;
                string reformedPrompt = "";
                PromptBubble promptBubble = new PromptBubble();
                promptBubble.isNegativePrompt = true;
                //if (!allSplitPrompts[prompt])
                //{
                //    promptBubble.isNegativePrompt = true;
                //}

                // if the bubble has + notation, add power to the previous prompt
                // note: doesn't support - notation, as common prompts include this, without meaning decrease power
                if (prompt.Contains("+"))
                {
                    char[] chars = prompt.ToCharArray();
                    foreach (char c in chars)
                    {
                        if (c == '+')
                        {
                            PromptBubble lastPromptBubble = (PromptBubble)((StackPanel)promptBubbles.Last()).Tag;
                            lastPromptBubble.power++;
                            lastPromptBubble.Tag = lastPromptBubble;
                            promptBubbles.RemoveAt(promptBubbles.Count - 1);
                            promptBubbles.Add(lastPromptBubble.CreatePromptBubble(lastPromptBubble.prompt, lastPromptBubble.power, lastPromptBubble.isNegativePrompt));
                        }
                        else
                        {
                            reformedPrompt += c;
                        }
                    }
                }

                // if the bubble has numeric notation, add power to the previous prompt
                if (reformedPrompt == "")
                {
                    reformedPrompt = prompt;
                }
                //if (reformedPrompt.Contains("1."))
                if (promptBubbles.Count > 0)
                {
                    datalog += "Not first prompt bubble" + Environment.NewLine;
                    string[] promptParts = reformedPrompt.Split(" ");
                    reformedPrompt = "";
                    double finalPower = 0;

                    PromptBubble lastPromptBubble = (PromptBubble)((StackPanel)promptBubbles.Last()).Tag;

                    int j = 0;
                    foreach (string promptPart in promptParts)
                    {
                        bool isDouble = double.TryParse(promptPart, out double power);
                        if (!isDouble && promptPart != "" && promptPart != " ")
                        {
                            datalog += $"Adding Prompt part: {promptPart} " + Environment.NewLine;
                            reformedPrompt += promptPart + " ";
                        }
                        else if (isDouble && j == 0)
                        {
                            if (power > 1)
                            {
                                finalPower = Math.Round((power - 1) * 10, 1);
                            }
                            else if (power < 1)
                            {
                                finalPower = Math.Round((1 - power) * -10, 1);
                            }
                            else if (power == 1)
                            {
                                finalPower = 0;
                            }
                            lastPromptBubble.power = (int)finalPower;
                            lastPromptBubble.Tag = lastPromptBubble;
                            promptBubbles.RemoveAt(promptBubbles.Count - 1);
                            promptBubbles.Add(lastPromptBubble.CreatePromptBubble(lastPromptBubble.prompt, lastPromptBubble.power, lastPromptBubble.isNegativePrompt));
                            datalog += $"Adding power: {finalPower}" + Environment.NewLine;
                            j++;
                            break;
                        }
                        j++;
                    }
                }
                else
                {
                    datalog += "First prompt bubble! " + reformedPrompt + Environment.NewLine;
                }

                StackPanel promptBubbleStack = promptBubble.CreatePromptBubble(reformedPrompt,0,true);
                if (promptBubbleStack.Tag is PromptBubble &&
                    reformedPrompt != "")
                {
                    // create the new UI object for the bubble
                    promptBubbles.Add(promptBubbleStack);
                    promptBubbleIndex++;
                }

                datalog += "------end-------" + Environment.NewLine;
            }

            // Add the new bubbles to the UI
            foreach (StackPanel promptBubble in promptBubbles)
            {
                wrp_PromptBubbles.Children.Add(promptBubble);
            }

            //MessageBox.Show(datalog);
        }

        private void btn_AddPromptBubble_Click(object sender, RoutedEventArgs e)
        {
            wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble());
        }

        private void txt_Prompt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ConvertStringToPromptBubbles(txt_Prompt.Text);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializePromptHelper();
        }

        private void btn_ResetPrompt_Click(object sender, RoutedEventArgs e)
        {
            ResetPrompt();
        }

        private void ResetPrompt()
        {
            List<StackPanel> promptBubblesToDelete = new List<StackPanel>();
            TextBox[] textBoxesToReset = { txt_PromptHelper, txt_NegativePrompt, txt_ImagePrompt };

            // delete the prompt bubbles
            foreach (UIElement element in wrp_PromptBubbles.Children)
            {
                if (element is StackPanel)
                {
                    promptBubblesToDelete.Add((StackPanel)element);
                }
            }

            foreach (StackPanel promptBubble in promptBubblesToDelete)
            {
                wrp_PromptBubbles.Children.Remove(promptBubble);
            }

            foreach (TextBox textBoxToReset in textBoxesToReset)
            {
                textBoxToReset.Text = "";
            }

            slider_Creativity.Value = 5;
            txt_promptHelperPower.Text = "1";
            txt_negativePromptHelperPower.Text = "1";
            txt_ImagePromptWeight.Text = "0.25";
        }
        private void ScanModels()
        {
            string modelPath = $"{Settings.Default["MainPath"]}\\models\\ldm\\stable-diffusion-v1\\";

            if (Directory.Exists(modelPath))
            {
                string[] models = Directory.GetFiles(modelPath);
                cmb_Model.Items.Clear();

                foreach (string model in models)
                {
                    string modelFileName = Path.GetFileName(model);
                    ComboBoxItem cmbModel = new ComboBoxItem
                    {
                        Content = modelFileName,
                        Tag = model
                    };
                    cmb_Model.Items.Add(cmbModel);

                    string currentModelFileName = Path.GetFileName($"{Settings.Default["MainPath"]}\\{Settings.Default["ModelPath"]}");

                    if (modelFileName == currentModelFileName)
                    {
                        cmb_Model.SelectedItem = cmbModel;
                    }
                }
            }
        }

        private void cmb_Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isFirstRun)
            {
                if (cmb_Model.SelectedItem != null)
                {
                    string modelPath = $"{Settings.Default["MainPath"]}\\models\\ldm\\{((ComboBoxItem)(cmb_Model.SelectedItem)).Content}";
                    txt_ModelPath.Text = modelPath;
                    Settings.Default["ModelPath"] = modelPath;
                    Settings.Default.Save();

                    string configPath = $"{Settings.Default["MainPath"]}\\configs\\models.yaml";
                    string configText = "stable-diffusion-1.5:\r\n" +
                        "  description: Generated by MitchJourn-E\r\n" +
                        $"  weights: models/ldm/stable-diffusion-v1/{Path.GetFileName(Settings.Default["ModelPath"].ToString())}\r\n" +
                        "  config: configs/stable-diffusion/v1-inference.yaml\r\n" +
                        "  width: 512\r\n" +
                        "  height: 512\r\n" +
                        "  vae: ./models/ldm/stable-diffusion-v1/vae-ft-mse-840000-ema-pruned.ckpt\r\n" +
                        "  default: true";

                    File.WriteAllText(configPath, configText);
                    StopRendering();
                }
            }
        }

        private void btn_ImageViewer_Click(object sender, RoutedEventArgs e)
        {
            ImageViewer imageViewer = new ImageViewer();
            imageViewer.Show();
        }

        /// <summary>
        /// Return a 0 if null
        /// </summary>
        /// <returns>An Int</returns>
        private static int SafeInt(object maybeInt)
        {
            int output = 0;
            if (maybeInt != null)
            {
                int.TryParse(maybeInt.ToString(), out output);
            }
            return output;
        }

        /// <summary>
        /// Return a 0 if null
        /// </summary>
        /// <returns>A double</returns>
        private static double SafeDouble(object maybeDouble)
        {
            double output = 0;
            if (maybeDouble != null)
            {
                double.TryParse(maybeDouble.ToString(), out output);
            }
            return output;
        }

        private void chk_HighRes_Unchecked(object sender, RoutedEventArgs e)
        {
            chk_HighrezFix.IsChecked = false;
        }

        private void btn_AddNegativePromptBubble_Click(object sender, RoutedEventArgs e)
        {
            wrp_PromptBubbles.Children.Add(new PromptBubble().CreatePromptBubble("",0,true));
        }

        private void RunInvokeAIWebUI()
        {
            
            bool webUIAlreadyStarted = invokeAIWebProcess != null && !invokeAIWebProcess.HasExited;
            if (!webUIAlreadyStarted)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                Process process = Process.Start(processStartInfo);

                // move the cmd directory to the main stable diffusion path and open the python environment called invokeAI (environment used at python install)
                string prerequisites = $"cd /d {Settings.Default["MainPath"]} & call .venv\\Scripts\\activate.bat &";

                // send the command to the CMD window to start the python script, enable the upsampler
                process.StandardInput.WriteLine($"{prerequisites} python .venv\\scripts\\invoke.py --web");
                invokeAIWebProcess = process;
                currentInvokeWebProcessID = process.Id;
            }
        }

        private void btn_InvokeAI_Click(object sender, RoutedEventArgs e) 
        {
            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;

            process = Process.Start(processStartInfo);

            // move the cmd directory to the main stable diffusion path and open the python environment called invokeAI (environment used at python install)
            string prerequisites = $"cd /d {Settings.Default["MainPath"]} & call .venv\\Scripts\\activate.bat &";

            // send the command to the CMD window to start the python script, enable the upsampler
            process.StandardInput.WriteLine($"{prerequisites} python .venv\\scripts\\invoke.py --web");

            ProcessStartInfo openWebsite = new ProcessStartInfo {
                FileName = "http://127.0.0.1:9090",
                UseShellExecute = true
            };
            Process.Start(openWebsite);
        }

        private void tab_InvokeAI_GotFocus(object sender, RoutedEventArgs e)
        {
            RunInvokeAIWebUI();
        }

        private void RunAutomaticWebUI()
        {
            bool webUIAlreadyStarted = automaticWebProcess != null && !automaticWebProcess.HasExited;
            if (!webUIAlreadyStarted)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe");
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                Process process = Process.Start(processStartInfo);

                process.StandardInput.WriteLine($"cd /d {Settings.Default["AutomaticMainPath"]} && webui.bat --theme dark");
                automaticWebProcess = process;
                currentAutomaticProcessID = process.Id;
            }
        }

        private void tab_Automatic_GotFocus(object sender, RoutedEventArgs e)
        {
            RunAutomaticWebUI();
        }

        private void cmb_Model_DropDownOpened(object sender, EventArgs e)
        {
            ScanModels();
        }
    }
}
