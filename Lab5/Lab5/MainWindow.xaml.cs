using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public List<UserClass> Users = new List<UserClass>();
        public List<UserClass> Admins = new List<UserClass>();
        public int Index { get; set; }
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
                //if (IsEmail(TextBox_CreateUserEmail.Text))
                Index = ListBox_UserList.SelectedIndex;
                if (Index != -1)
                {
                    Users[Index] = new UserClass(TextBox_CreateUserName.Text, TextBox_CreateUserEmail.Text);
                }
                else
                {
                    Users.Add(new UserClass(TextBox_CreateUserName.Text, TextBox_CreateUserEmail.Text));
                }
                
                //Sort by name
                //var sortedUserList = from u in Users
                     //                orderby u.UserName
                     //                select u;
                //Add the new users lst to listbox
                for (int i = 0; i < Users.Count; i++)
                {
                    ListBox_UserList.Items.Add(Users.ElementAt(i));
                }
                //Remove contest of textboxes
                TextBox_CreateUserName.Clear();
                TextBox_CreateUserEmail.Clear();
                Lable_CreateUserNameWatermark.Visibility = Visibility.Visible;
                Lable_CreateUserEmailWatermark.Visibility = Visibility.Visible;
                Button_CreateUser.IsEnabled = false;
            }
        }
        
        private void ListBox_UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_AdminList.SelectedIndex != -1)
            {
                ListBox_AdminList.SelectedIndex = -1;
            }
            Icon_UserIcon_Placeholder_.Opacity = 100;
            Index = ListBox_UserList.SelectedIndex;
            if (Index != -1)
            {
                DisplayInfo(Users[Index]);
            }
            else
            {
                DisplayInfo_Label.Content = "Name:\nEmail:";
            }
        }

        private void TextBox_CreateUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            Lable_CreateUserNameWatermark.Visibility = Visibility.Hidden;
        }

        private void TextBox_CreateUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserName.Text != null && TextBox_CreateUserName.Text != "")
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
                Lable_CreateUserNameWatermark.Visibility = Visibility.Visible;
            }
        }

        private void TextBox_CreateUserEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            Lable_CreateUserEmailWatermark.Visibility = Visibility.Hidden;
            if (TextBox_CreateUserName.Text != "" && TextBox_CreateUserEmail.Text != "")
            {
                Button_CreateUser.IsEnabled = true;
            }
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
                Lable_CreateUserEmailWatermark.Visibility = Visibility.Visible;
            }
            
            
        }

        private void Button_CreateAdmin_Click(object sender, RoutedEventArgs e)
        {
            Index = ListBox_UserList.SelectedIndex;
            if (Index != -1)
            {
                UserClass theUser = Users[Index];
                ListBox_AdminList.Items.Add(theUser.UserName);
                Admins.Add(theUser);
                ListBox_UserList.Items.RemoveAt(Index);
                Users.Remove(theUser);
            }
        }

        private void Button_RemoveAdmin_Click(object sender, RoutedEventArgs e)
        {
            Index = ListBox_AdminList.SelectedIndex;
            if (Index != -1)
            {
                UserClass theAdmin = Admins[Index];
                ListBox_UserList.Items.Add(theAdmin.UserName);
                Users.Add(theAdmin);
                ListBox_AdminList.Items.RemoveAt(Index);
                Admins.Remove(theAdmin);
            }
        }

        private void Button_RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            Index = ListBox_UserList.SelectedIndex;
            if (Index != -1)
            {
                if (!Admins.Contains(Users[Index]))
                {
                    UserClass theUser = Users[Index];
                    ListBox_UserList.Items.RemoveAt(Index);
                    Users.Remove(theUser);
                }
            }
        }

        public bool IsEmail(string email)
        {
            string pattern1 = @"^ ([\w\.\-] +)@([\w\-] +)((\.(\w){ 2,3})+)$";
            if (Regex.IsMatch(email, pattern1))
            {
                return true;
            }
            return false;
        }

        private void ListBox_AdminList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_UserList.SelectedIndex != -1)
            {
                ListBox_UserList.SelectedIndex = -1;
            }
            Icon_UserIcon_Placeholder_.Opacity = 100;
            Index = ListBox_AdminList.SelectedIndex;
            if (Index != -1)
            {
                DisplayInfo(Admins[Index]);
            }
            else
            {
                DisplayInfo_Label.Content = "Name:\nEmail:";
            }
        }

        public void DisplayInfo(UserClass user)
        {
            DisplayInfo_Label.Content = $"Name: {user.UserName}\nEmail: {user.UserEmail}";
        }

        private void Button_EditUser_Click(object sender, RoutedEventArgs e)
        {
            Index = ListBox_UserList.SelectedIndex;
            if (Index != -1)
            {
                Users[Index].UserName = TextBox_CreateUserName.Text;
                Users[Index].UserEmail = TextBox_CreateUserEmail.Text;
            }
            Index = ListBox_AdminList.SelectedIndex;
            if (Index != -1)
            {
                Admins[Index].UserName = TextBox_CreateUserName.Text;
                Admins[Index].UserEmail = TextBox_CreateUserEmail.Text;
            }
            TextBox_CreateUserName.Clear();
            TextBox_CreateUserEmail.Clear();
            Lable_CreateUserNameWatermark.Visibility = Visibility.Visible;
            Lable_CreateUserEmailWatermark.Visibility = Visibility.Visible;
        }

        private void ListBox_UserList_LostFocus(object sender, RoutedEventArgs e)
        {
            ListBox_UserList.Items.Refresh();
        }
    }
}
