using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HackerNewsReader {
  /// <summary>
  /// Interaction logic for ListItem.xaml
  /// </summary>
  public partial class ListItem : UserControl {
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
      "Title",
      typeof(string),
      typeof(ListItem),
      new PropertyMetadata(string.Empty)
    );

    public static readonly DependencyProperty UrlProperty = DependencyProperty.Register(
      "Url",
      typeof(string),
      typeof(ListItem),
      new PropertyMetadata(string.Empty)
    );

    public string Title {
      get { return (string)GetValue(TitleProperty); }
      set { SetValue(TitleProperty, value); }
    }

    public string Url {
      get { return (string)GetValue(UrlProperty); }
      set { SetValue(UrlProperty, value); }
    }

    public ListItem() {
      InitializeComponent();
    }

    private void OnRequestNavigate(object sender, RequestNavigateEventArgs e) {
      Hyperlink hl = (Hyperlink)sender;  
      var navigateUri = hl.NavigateUri.ToString();  
      Process.Start(new ProcessStartInfo(navigateUri));  
      e.Handled = true;  
    }
  }
}
