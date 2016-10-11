using HackerNewsReader.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
  class Model : ObservableObject {
    private NotifyTaskCompletion<List<Story>> _stories;
    
    public NotifyTaskCompletion<List<Story>> Stories {
      get { return _stories; }
      set {
        _stories = value;
        RaisePropertyChangedEvent("Stories");
      }
    }

    public Model() {
      this.fetchData();
    }

    public void fetchData() {
      Stories = new NotifyTaskCompletion<List<Story>>(
        Core.getDataAsync()
      );
    }
  }

  public partial class MainWindow : Window {
    private Model model;

    public MainWindow() {
      InitializeComponent();
      model = new Model();
      this.DataContext = model;
    }

    private void OnRefresh(object sender, RoutedEventArgs e) {
      model.fetchData();
    }
  }
}
