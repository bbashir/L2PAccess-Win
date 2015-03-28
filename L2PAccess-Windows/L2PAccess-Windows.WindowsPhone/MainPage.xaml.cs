using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
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
using L2PAccess.Authentication;
using L2PAccess.Authentication.Config;
using L2PAccess.Authentication.Verification;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace L2PAccess_Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IWebAuthenticationContinuable
    {
        private L2PClient l2PClient;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var config = RwthConfig.Create("YOUR CLIENT ID");

            l2PClient = new L2PClient(config);
            var viewAllCourseInfo = await l2PClient.ViewAllCourseInfo();
            ResuListView.ItemsSource = viewAllCourseInfo.dataSet.Select(course => course.courseTitle);
        }

        public async void ContinueWebAuthentication(WebAuthenticationBrokerContinuationEventArgs args)
        {
            var windowsPhoneVerifier = OAuthManager.Instance.AuthModule.Verifier as WindowsPhoneVerifier;
            if (windowsPhoneVerifier != null)
            {
                await windowsPhoneVerifier.ContinueVerification(args);
                var viewAllCourseInfo = await l2PClient.ViewAllCourseInfo();
                ResuListView.ItemsSource = viewAllCourseInfo.dataSet.Select(course => course.courseTitle);
            }
        }
    }
}
