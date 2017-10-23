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

        private void TextBox_CreateUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            if (TextBox_CreateUserName.Text == null)
            {
                TextBox_CreateUserName.Foreground = (Brush)bc.ConvertFrom("#FF838383");
                TextBox_CreateUserName.Text = "User name";
            }
        }

        private void TextBox_CreateUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            if (TextBox_CreateUserName.Text == "User Name")
            {
                TextBox_CreateUserName.Foreground = (Brush)bc.ConvertFrom("#FF000000");
                TextBox_CreateUserName.Clear();
            }
            
            
        }
    }
}
