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
        public List<UserClass> Users = new List<UserClass>();
        public List<UserClass> Admins = new List<UserClass>();
        public MainWindow()
        {
            
            InitializeComponent();

        }

        private void Button_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserName != null && TextBox_CreateUserEmail != null)
            {
                //remove users from Listbox_users
                ListBox_UserList.Items.Clear();
                
                //Add user to list users
                Users.Add( new UserClass(TextBox_CreateUserName.Text, TextBox_CreateUserEmail.Text));
                //Sort by name
                //var sortedUserList = from u in Users
                    //                 orderby u.UserName
                     //                select u;
                //Add the new users lst to listbox
                for (int i = 0; i < Users.Count; i++)
                {
                    ListBox_UserList.Items.Add(Users.ElementAt(i));
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

        private void ListBox_UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_UserList.SelectedItem != null)
            {
                Icon_UserIcon_Placeholder_.Opacity = 100;

                Textbox_DisplayUserName.Text = ListBox_UserList.SelectedItem.ToString();
            }
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

        private void Button_CreateAdmin_Click(object sender, RoutedEventArgs e)
        {
            int index = ListBox_UserList.SelectedIndex;
            if (index != -1)
            {
                UserClass theUser = Users[index];
                ListBox_AdminList.Items.Add(theUser.UserName);
                Admins.Add(theUser);
                ListBox_UserList.Items.RemoveAt(index);//(theUser.UserName);
                Users.Remove(theUser);
            }
        }

        private void Button_RemoveAdmin_Click(object sender, RoutedEventArgs e)
        {
            int index = ListBox_AdminList.SelectedIndex;
            if (index != -1)
            {
                UserClass theAdmin = Admins[index];
                ListBox_UserList.Items.Add(theAdmin.UserName);
                Users.Add(theAdmin);
                ListBox_AdminList.Items.RemoveAt(index);//(theAdmin.UserName);
                Admins.Remove(theAdmin);
            }
        }

        private void Button_RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            int index = ListBox_UserList.SelectedIndex;
            if (index != -1)
            {
                if (!Admins.Contains(Users[index]))
                {
                    UserClass theUser = Users[index];
                    ListBox_UserList.Items.RemoveAt(index);//(theUser.UserName);
                    Users.Remove(theUser);
                }
            }
        }
    }
}
