using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAssessment.Models;
using MauiAssessment.Services;

namespace MauiAssessment.ViewModels
{
    [QueryProperty(nameof(ProductDetail), "ProductDetail")]
    public partial class AddProductViewmodel : ObservableObject
    {
        [ObservableProperty]
        private Product _productDetail = new Product();

        private readonly ProductServices _productServices;
        public string DateValue;
        private bool IsPhotoAdded;

        public AddProductViewmodel(ProductServices productServices)
        {
            _productServices = productServices;
            CaptureProductPhotoCommand = new Command(DoCaptureProductPhoto, () => MediaPicker.IsCaptureSupported);

            
        }

        [RelayCommand]
        public async void AddUpdateProduct()
        {
            if (ProductDetail.Name == "" || ProductDetail.Price =="" ||
                ProductDetail.Category == "" || ProductDetail.Detail == "" ||
                ProductDetail.Name is null || ProductDetail.Price is null ||
                ProductDetail.Category is null || ProductDetail.Detail is null)
            {
                await Shell.Current.DisplayAlert("Alert!", "Please fill all the Required Fields", "OK");
                return;
            }
            decimal result = decimal.Parse(ProductDetail.Price);
            if (result < 1) {
                await Shell.Current.DisplayAlert("Alert!", "Price cannot be less than 1", "OK");
                return;
            }
            int response = -1;
            if (ProductDetail.Id > 0)
            {
                ProductDetail.ProductPhoto = CompleteProductPhotoPath;
                response = await _productServices.UpdateProduct(ProductDetail);    
            }
            else
            {
                IsPhotoAdded = CompleteProductPhotoPath is not null;
                response = await _productServices.AddProduct(new Models.Product
                {
                    Name = ProductDetail.Name,
                    Price = ProductDetail.Price,
                    Detail = ProductDetail.Detail,
                    Category = ProductDetail.Category,
                    ProductPhoto = CompleteProductPhotoPath,
                    HasPhoto = IsPhotoAdded,
                    LaunchDate = DateValue
                });
            }



            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Success!", "Product Saved Successfully", "OK");
                await Shell.Current.GoToAsync("..", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }
        }

        public async Task<String> LoadPhotoAsync(FileResult photo)
        {
            var stream = photo.OpenReadAsync().Result;

            byte[] imagedata;

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imagedata = ms.ToArray();
            }

            var folderpath = Path.Combine(FileSystem.AppDataDirectory, "ProductPhoto");
            if (!File.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            var empfilename = Guid.NewGuid() + "_Productphoto.jpg";

            var newfile = Path.Combine(folderpath, empfilename);// Complete Path of the photo

            using (var stream2 = new MemoryStream(imagedata))
            using (var newstream = File.OpenWrite(newfile))
            {
                await stream2.CopyToAsync(newstream);
            }

            return newfile;
        }

        public ICommand CaptureProductPhotoCommand { get; }


        private async void DoCaptureProductPhoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                CompleteProductPhotoPath = await LoadPhotoAsync(photo);
                Console.WriteLine("Product Photo Captured" + CompleteProductPhotoPath);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private string ProductPhotoPath;
        public string CompleteProductPhotoPath
        {
            get => ProductPhotoPath;
            set
            {
                SetProperty(ref ProductPhotoPath, value);
                HasPhoto = !string.IsNullOrEmpty(value);
            }
        }

        private bool _hasPhoto;
        public bool HasPhoto
        {
            get => _hasPhoto;
            set => SetProperty(ref _hasPhoto, value);

        }
    }
}

