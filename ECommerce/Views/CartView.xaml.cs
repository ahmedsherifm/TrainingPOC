using System.Windows.Controls;

namespace ECommerce.Views
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        public CartView()
        {
            InitializeComponent();
        }

        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listview.SelectedItem = null;
        }
    }
}
