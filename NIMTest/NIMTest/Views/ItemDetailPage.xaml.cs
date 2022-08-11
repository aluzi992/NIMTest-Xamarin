using NIMTest.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NIMTest.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}