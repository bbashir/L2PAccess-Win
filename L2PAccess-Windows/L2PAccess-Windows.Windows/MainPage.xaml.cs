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
            var config = RwthConfig.Create("YOUR CLIENT ID");

            var task1 = Task.Factory.StartNew(async () =>
            {
                var l2PClient = new L2PClient(config);
                return await l2PClient.ViewAllCourseInfo();
            }).Result;
            var task2 = Task.Factory.StartNew(async () =>
            {
                var l2PClient = new L2PClient(config);
                return await l2PClient.ViewAllCourseInfo();
            }).Result;

            var l2PResponses = await Task.WhenAll(task1, task2);

            ResuListView.ItemsSource = l2PResponses[0].dataSet.Select(course => course.courseTitle);
        }
    }
}
