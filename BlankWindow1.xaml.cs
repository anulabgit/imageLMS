using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Search;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankWindow1 : Window
    {
        private void ImageGridView_ContainerContentChanging(
    ListViewBase sender,
    ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue)
            {
                var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;
                var image = templateRoot.FindName("ItemImage") as Image;
                image.Source = null;
            }

            if (args.Phase == 0)
            {
                args.RegisterUpdateCallback(ShowImage);
                args.Handled = true;
            }
        }

        private async void ShowImage(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase == 1)
            {
                // It's phase 1, so show this item's image.
                var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;
                var image = templateRoot.FindName("ItemImage") as Image;
                var item = args.Item as ImageFileInfo;
                image.Source = await item.GetImageThumbnailAsync();
            }
        }
        private async Task GetItemsAsync()
        {
            StorageFolder appInstalledFolder = Package.Current.InstalledLocation;
            StorageFolder picturesFolder = await appInstalledFolder.GetFolderAsync("Assets\\Samples");

            var result = picturesFolder.CreateFileQueryWithOptions(new QueryOptions());

            IReadOnlyList<StorageFile> imageFiles = await result.GetFilesAsync();
            foreach (StorageFile file in imageFiles)
            {
                Images.Add(await LoadImageInfoAsync(file));
            }

            ImageGridView.ItemsSource = Images;
        }

        public async static Task<ImageFileInfo> LoadImageInfoAsync(StorageFile file)
        {
            var properties = await file.Properties.GetImagePropertiesAsync();
            ImageFileInfo info = new(properties,
                                     file, file.DisplayName, file.DisplayType);

            return info;
        }
        public ObservableCollection<ImageFileInfo> Images { get; } =
            new ObservableCollection<ImageFileInfo>();
        public BlankWindow1()
        {
            this.InitializeComponent();
            GetItemsAsync();
        }
        private async void MoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string sourcePath = (string)button.Tag; // Get the path from the button's tag
                string destinationPath = "Assets\\Samples"; // Set your desired destination path

                try
                {
                    StorageFile fileToMove = await StorageFile.GetFileFromPathAsync(sourcePath);
                    StorageFolder destinationFolder = await StorageFolder.GetFolderFromPathAsync(destinationPath);
                    await fileToMove.MoveAsync(destinationFolder);
                    // Optionally refresh the view or update UI after moving
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., file not found, access denied)
                    await ShowMessageAsync("오류", ex.Message);
                }
            }
        }
        private async void GoodPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string sourcePath = (string)button.Tag; // Get the path from the button's tag
                string destinationPath = "C:\\Users\\cubedice\\source\\repos\\App1\\App1\\Assets\\좋은 사진"; // Set your desired destination path for good photos

                try
                {
                    StorageFile fileToMove = await StorageFile.GetFileFromPathAsync(sourcePath);
                    StorageFolder destinationFolder = await StorageFolder.GetFolderFromPathAsync(destinationPath);
                    await fileToMove.MoveAsync(destinationFolder);
                    // Optionally refresh the view or update UI after moving
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., file not found, access denied)
                    await ShowMessageAsync("오류", ex.Message);
                }
            }
        }

        private async void BadPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string sourcePath = (string)button.Tag; // Get the path from the button's tag
                string destinationPath = "C:\\Users\\cubedice\\source\\repos\\App1\\App1\\Assets\\안좋은 사진"; // Set your desired destination path for bad photos

                try
                {
                    StorageFile fileToMove = await StorageFile.GetFileFromPathAsync(sourcePath);
                    StorageFolder destinationFolder = await StorageFolder.GetFolderFromPathAsync(destinationPath);
                    await fileToMove.MoveAsync(destinationFolder);
                    // Optionally refresh the view or update UI after moving
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., file not found, access denied)
                    await ShowMessageAsync("오류", ex.Message);
                }
            }
        }

        private async void KeepPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string sourcePath = (string)button.Tag; // Get the path from the button's tag
                string destinationPath = "C:\\Users\\cubedice\\source\\repos\\App1\\App1\\Assets\\남겨두기"; // Set your desired destination path for keeping photos

                try
                {
                    StorageFile fileToMove = await StorageFile.GetFileFromPathAsync(sourcePath);
                    StorageFolder destinationFolder = await StorageFolder.GetFolderFromPathAsync(destinationPath);
                    await fileToMove.MoveAsync(destinationFolder);
                    // Optionally refresh the view or update UI after moving
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., file not found, access denied)
                    await ShowMessageAsync("오류", ex.Message);
                }
            }
        }
        private async Task ShowMessageAsync(string title, string content)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "확인"
            };

            dialog.XamlRoot = this.Content.XamlRoot;
            await dialog.ShowAsync();
        }
    }
}
