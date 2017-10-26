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
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace Lab5
{
    public partial class MainWindow : Window
    {
        public List<UserClass> users = new List<UserClass>();
        public List<UserClass> admins = new List<UserClass>();
        public MainWindow()
        {
            
            InitializeComponent();

        }

        private void Button_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserName != null && TextBox_CreateUserEmail != null)
            {
                //remove users from Listbox_users
                ListBox_UserLIst.Items.Clear();
                
                //Add user to list users
                users.Add( new UserClass(TextBox_CreateUserName.Text, TextBox_CreateUserEmail.Text));
                //Sort by name
                var sortedUserList = from u in users
                                     orderby u.UserName
                                     select u;
                //Add the new users lst to listbox
                for (int i = 0; i < sortedUserList.Count(); i++)
                {
                    ListBox_UserLIst.Items.Add(sortedUserList.ElementAt(i));
                }
                //Remove contest of textboxes
                TextBox_CreateUserName.Clear();
                TextBox_CreateUserEmail.Clear();
                Lable_CreateUserNameWatermark.Visibility = System.Windows.Visibility.Visible;
                Lable_CreateUserEmailWatermark.Visibility = System.Windows.Visibility.Visible;
                Button_CreateUser.IsEnabled = false;
            }
        }
        
        private void ListBox_UserLIst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Icon_UserIcon_Placeholder_.Opacity = 100;

            Textbox_DisplayUserName.Text = ListBox_UserLIst.SelectedItem.ToString();
        }

        private void TextBox_CreateUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            Lable_CreateUserNameWatermark.Visibility = System.Windows.Visibility.Hidden;
        }

        private void TextBox_CreateUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserName.Text != null || TextBox_CreateUserName.Text != "")
            {
                if (TextBox_CreateUserEmail.Text != null && TextBox_CreateUserEmail.Text != "")
                {
                    Button_CreateUser.IsEnabled = true;
                }
                else
                {
                    Button_CreateUser.IsEnabled = false;
                }
            }
            else
            {
                Lable_CreateUserNameWatermark.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void TextBox_CreateUserEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            Lable_CreateUserEmailWatermark.Visibility = System.Windows.Visibility.Hidden;
        }

        private void TextBox_CreateUserEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserEmail.Text != null && TextBox_CreateUserEmail.Text != "")
            {
                if (TextBox_CreateUserName.Text !=null && TextBox_CreateUserName.Text !="")
                {
                    Button_CreateUser.IsEnabled = true;
                }
                else
                {
                    Button_CreateUser.IsEnabled = false;
                }
            }
            else
            {
                Lable_CreateUserEmailWatermark.Visibility = System.Windows.Visibility.Visible;
            }
            
        }
    }
}
