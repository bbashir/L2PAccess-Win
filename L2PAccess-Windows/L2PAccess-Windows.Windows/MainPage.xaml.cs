using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using L2PAccess.API;
using L2PAccess.API.Model;
using L2PAccess.Authentication;
using L2PAccess.Authentication.Config;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace L2PAccess_Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var config = RwthConfig.Create("yJV0yVoAPZ3ykvnhZImWg61TBbMUBv7ZYI3zSo7XCaPlJKsMVOHBqBbbx7Ko5SXi.apps.rwth-aachen.de");

            var l2PClient = new L2PClient(config);
            var viewAllCourseInfo = await l2PClient.ViewAllCourseInfo();
            ResuListView.ItemsSource = viewAllCourseInfo.dataSet.Select(course => course.courseTitle);
        }
    }
}
