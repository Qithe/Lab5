using System;
using System.Collections.Generic;
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

namespace Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserName != null && TextBox_CreateUserEmail != null)
            {
                UserClass user = new UserClass(TextBox_CreateUserName.ToString(),TextBox_CreateUserEmail.ToString());
                ListBox_UserLIst.Items.Add(user);
            }
        }

        private void TextBox_CreateUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            Button_CreateUser.IsEnabled = TextBox_CreateUserName != null && TextBox_CreateUserEmail != null;
            
        }

        private void TextBox_CreateUserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button_CreateUser.IsEnabled = TextBox_CreateUserName != null && TextBox_CreateUserEmail != null;
        }

        private void ListBox_UserLIst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Icon_UserIcon_Placeholder_.Opacity = 100;

            Textbox_DisplayUserName.Text = ListBox_UserLIst.SelectedItem.ToString();
        }
    }
}
