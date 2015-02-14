using System;
using System.Collections;
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

            //ObservableCollection<string> list = new ObservableCollection<string>();
            //list.Add("http://localhost:61793/PharmacyCOM.asmx");
            //cbServerUrl.ItemsSource = list;
            txtServerUrl.Text = "http://localhost:61793/PharmacyCOM.asmx";

            tabServiceCommands.IsEnabled = false;
            tabServiceDetails.IsEnabled = false;
        }

        private void btnSendGetRxItem_Click(object sender, RoutedEventArgs e)
        {
            txtRequest.Text = String.Empty;
            txtResponse.Text = String.Empty;
            txtResults.Text = String.Empty;
            m_service.GetRxItemAsync(txtPartNumber.Text.Trim());
        }

        void service_GetRxItemCompleted(object sender, WebService.GetRxItemCompletedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (e.Error != null)
            {
                sb.AppendLine("#####");
                sb.AppendLine(String.Format("{0}: {1}", "Error", e.Error));
                sb.AppendLine("#####");
                sb.AppendLine();
            }

            if (e.Result == null)
            {
                sb.AppendLine(String.Format("{0}: {1}", "Result", "null"));
            }
            else
            {
                WebService.RxItem item = e.Result;
                sb.AppendLine(String.Format("{0}: \"{1}\"", "Result", item.GetType().Name));
                item.GetType().GetProperties().ToList().ForEach(p =>
                {
                    sb.AppendLine(String.Format("{0}: {1}", p.Name, p.GetValue(item)));
                    if (p.PropertyType.IsGenericType)
                    {
                        var list = p.GetValue(item) as IEnumerable;
                        if (list != null)
                        {
                            foreach (object lItem in list)
                            {
                                sb.AppendLine(String.Format("\t{0}: {1}", "Item", lItem.GetType().Name));
                                lItem.GetType().GetProperties().ToList().ForEach(p2 =>
                                {
                                    sb.AppendLine(String.Format("\t\t{0}: {1}", p2.Name, p2.GetValue(lItem)));
                                });
                            }
                        }
                    }
                });
            }

            sb.AppendLine();

            txtResults.Text = sb.ToString();
            tabServiceDetails.SelectedIndex = 2;
        }

        private void SetupService()
        {
            try
            {
                TeardownService();
                if (!String.IsNullOrWhiteSpace(txtServerUrl.Text.Trim()))
                {
                    m_service = new WebService.PharmacyCOM(txtServerUrl.Text.Trim());
                    m_service.GetRxItemCompleted += service_GetRxItemCompleted;
                    tabServiceCommands.IsEnabled = true;
                    tabServiceDetails.IsEnabled = true;
                }
            }
            catch
            {
                tabServiceCommands.IsEnabled = false;
                tabServiceDetails.IsEnabled = false;
            }
        }

        private void TeardownService()
        {
            if (m_service != null)
            {
                m_service.GetRxItemCompleted -= service_GetRxItemCompleted;
                m_service.Dispose();
                m_service = null;
            }

            tabServiceCommands.IsEnabled = false;
            tabServiceDetails.IsEnabled = false;
        }

        private void btnSetupService_Click(object sender, RoutedEventArgs e)
        {
            SetupService();
        }

        private void txtServerUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            TeardownService();
        }
    }
}
