using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace PaladinPharmacyCOMv1.Example.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebService.PharmacyCOM m_service;
        public MainWindow()
        {
            InitializeComponent();

            //Setup service logging handlers
            WebService.PharmacyCOMServiceLogger.RequestHandler = ((msg) =>
            {
                if (Dispatcher.CheckAccess())
                {
                    txtRequest.Text = msg;
                    tabServiceDetails.SelectedIndex = 0;
                    return;
                }
                else
                {
                    Dispatcher.Invoke(WebService.PharmacyCOMServiceLogger.RequestHandler, msg);
                    return;
                }
            });

            WebService.PharmacyCOMServiceLogger.ResponseHandler = ((msg) =>
            {
                if (Dispatcher.CheckAccess())
                {
                    txtResponse.Text = msg;
                    tabServiceDetails.SelectedIndex = 1;
                    return;
                }
                else
                {
                    Dispatcher.Invoke(WebService.PharmacyCOMServiceLogger.ResponseHandler, msg);
                    return;
                }
            });

            ObservableCollection<string> list = new ObservableCollection<string>();
            list.Add("http://localhost:61793/PharmacyCOM.asmx");
            cbServerUrl.ItemsSource = list;

            tabServiceCommands.IsEnabled = false;
            tabServiceDetails.IsEnabled = false;
        }

        private void btnSendGetRxItem_Click(object sender, RoutedEventArgs e)
        {
            txtRequest.Text = String.Empty;
            txtResponse.Text = String.Empty;
            m_service.GetRxItemAsync(txtPartNumber.Text.Trim());
        }

        void service_GetRxItemCompleted(object sender, WebService.GetRxItemCompletedEventArgs e)
        {
            //tabServiceDetails.SelectedIndex = 1;
        }

        private void cbServerUrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_service != null)
            {
                m_service.GetRxItemCompleted -= service_GetRxItemCompleted;
                m_service.Dispose();
                m_service = null;
            }

            if (!String.IsNullOrWhiteSpace(cbServerUrl.Text.Trim()))
            {
                m_service = new WebService.PharmacyCOM(cbServerUrl.Text.Trim());
                m_service.GetRxItemCompleted -= service_GetRxItemCompleted;
                tabServiceCommands.IsEnabled = true;
                tabServiceDetails.IsEnabled = true;
            }
            else
            {
                tabServiceCommands.IsEnabled = false;
                tabServiceDetails.IsEnabled = false;
            }
        }
    }
}
