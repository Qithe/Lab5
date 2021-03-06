﻿using System;
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
        public int lastUsedList = -1;// 0 is user list, 1 is admin list
        public MainWindow()
        {
            
            InitializeComponent();

        }

        private void Button_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserName != null && TextBox_CreateUserEmail != null && IsEmail(TextBox_CreateUserEmail.Text) )
            {
                //remove users from Listbox_users
                ListBox_UserList.Items.Clear();

                //Add user to list users
                Users.Add(new UserClass(TextBox_CreateUserName.Text, TextBox_CreateUserEmail.Text));
                
                //Add the new users lst to listbox
                for (int i = 0; i < Users.Count; i++)
                {
                    ListBox_UserList.Items.Add(Users.ElementAt(i).UserName);
                }
                //Remove contest of textboxes
                TextBox_CreateUserName.Clear();
                TextBox_CreateUserEmail.Clear();
                Lable_CreateUserNameWatermark.Visibility = Visibility.Visible;
                Lable_CreateUserEmailWatermark.Visibility = Visibility.Visible;
                Button_CreateUser.IsEnabled = false;
                Button_EditUser.IsEnabled = false;
            }
        }
        
        private void ListBox_UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If something is selected in AdminList i gets unselected
            if (ListBox_AdminList.SelectedIndex != -1)
            {
                ListBox_AdminList.SelectedIndex = -1;
            }
            //The userInfo i displayed
            Icon_UserIcon_Placeholder.Opacity = 100;
            Index = ListBox_UserList.SelectedIndex;
            if (Index != -1)
            {
                DisplayInfo(Users[Index]);
                Button_CreateAdmin.IsEnabled = true;
                Button_RemoveUser.IsEnabled = true;
            }
            else
            {
                Button_CreateAdmin.IsEnabled = false;
                Button_RemoveUser.IsEnabled = false;
                Label_UserInfo.Content = "Namn: \nEmail:";
            }
        }

        private void ListBox_AdminList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If something is selected in UserList it gets unselected
            if (ListBox_UserList.SelectedIndex != -1)
            {
                ListBox_UserList.SelectedIndex = -1;
                Button_CreateAdmin.IsEnabled = false;
            }
            //Displays the UserInfo
            Icon_UserIcon_Placeholder.Opacity = 100;
            Index = ListBox_AdminList.SelectedIndex;
            if (Index != -1)
            {
                DisplayInfo(Admins[Index]);
                Button_RemoveAdmin.IsEnabled = true;
                Button_RemoveUser.IsEnabled = true;
            }
            else
            {
                Button_RemoveAdmin.IsEnabled = false;
                Button_RemoveUser.IsEnabled = false;
                Label_UserInfo.Content = "Namn: \nEmail:";
            }
        }

        private void TextBox_CreateUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            Lable_CreateUserNameWatermark.Visibility = Visibility.Hidden;
            if (TextBox_CreateUserName.Text != "" && TextBox_CreateUserEmail.Text != "")
            {
                if (ListBox_UserList.SelectedIndex != -1 || ListBox_AdminList.SelectedIndex != -1)
                {
                    Button_EditUser.IsEnabled = true;
                }
            }
        }

        private void TextBox_CreateUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserName.Text != null && TextBox_CreateUserName.Text != "")
            {
                TextChecker(TextBox_CreateUserEmail.Text, 1);
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
                if (ListBox_UserList.SelectedIndex != -1 || ListBox_AdminList.SelectedIndex != -1)
                {
                    Button_EditUser.IsEnabled = true;
                }
            }
        }

        private void TextBox_CreateUserEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_CreateUserEmail.Text != null && TextBox_CreateUserEmail.Text != "")
            {
                TextChecker(TextBox_CreateUserName.Text, 0);
            }
            else
            {
                Lable_CreateUserEmailWatermark.Visibility = Visibility.Visible;
            }
            
            
        }

        private void Button_CreateAdmin_Click(object sender, RoutedEventArgs e)
        {
            //The selected user is added to the AdminList and removed from the UserList
            Index = ListBox_UserList.SelectedIndex;
            if (Index != -1)
            {
                UserClass theUser = Users[Index];
                ListBox_AdminList.Items.Add(theUser.UserName);
                Admins.Add(theUser);
                ListBox_UserList.Items.RemoveAt(Index);
                Users.Remove(theUser);
            }
            Button_CreateAdmin.IsEnabled = false;
            Button_RemoveUser.IsEnabled = false;
        }

        private void Button_RemoveAdmin_Click(object sender, RoutedEventArgs e)
        {
            //The selected admin is removed from the AdminList and added to the UserList
            Index = ListBox_AdminList.SelectedIndex;
            if (Index != -1)
            {
                UserClass theAdmin = Admins[Index];
                ListBox_UserList.Items.Add(theAdmin.UserName);
                Users.Add(theAdmin);
                ListBox_AdminList.Items.RemoveAt(Index);
                Admins.Remove(theAdmin);
            }
            Button_RemoveAdmin.IsEnabled = false;
            Button_RemoveUser.IsEnabled = false;
        }

        private void Button_RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            int userIndex = ListBox_UserList.SelectedIndex;
            int adminIndex = ListBox_AdminList.SelectedIndex;
            //Checks which list was last used and removes the selected user/admin
            if (lastUsedList == 0)
            {
                if (userIndex != -1)
                {
                        UserClass theUser = Users[userIndex];
                        ListBox_UserList.Items.RemoveAt(userIndex);
                        Users.Remove(theUser);
                }
                Button_CreateAdmin.IsEnabled = false;
            }
            else if (lastUsedList == 1)
            {
                if (adminIndex != -1)
                {
                        UserClass theAdmin = Admins[adminIndex];
                        ListBox_AdminList.Items.RemoveAt(adminIndex);
                        Admins.Remove(theAdmin);
                }
                Button_RemoveAdmin.IsEnabled = false;
                Button_CreateAdmin.IsEnabled = false;
            }
            Button_RemoveUser.IsEnabled = false;
        }

        
        public bool IsEmail(string email)
        {
            //Checks if whats typed is an email
            string pattern1 = @".+@.+[.].{2,3}";
            if (Regex.IsMatch(email, pattern1))
            {
                return true;
            }
            return false;
        }
        

        private void ListBox_UserList_LostFocus(object sender, RoutedEventArgs e)
        {
            lastUsedList = 0;
        }
        

        private void ListBox_AdminList_LostFocus(object sender, RoutedEventArgs e)
        {
            lastUsedList = 1;
        }

        private void TextBox_CreateUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_CreateUserEmail.Text != null && TextBox_CreateUserEmail.Text != "")
            {
                TextChecker(TextBox_CreateUserName.Text, 0);
                if(ListBox_UserList.SelectedIndex != -1 || ListBox_AdminList.SelectedIndex != -1)
                {
                    Button_EditUser.IsEnabled = true;
                }
            }
        }

        private void TextChecker (string text, int x)
        {
            if (text != null && text != "" && x == 0)
            {
                Button_CreateUser.IsEnabled = true;
            }
            else if(text != null && text != "" && x == 1)
            {
                if (IsEmail(text))
                {
                    Button_CreateUser.IsEnabled = true;
                }
            }
            else
            {
                Button_CreateUser.IsEnabled = false;
            }
        }

        private void TextBox_CreateUserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_CreateUserName.Text != null && TextBox_CreateUserName.Text != "")
            {
                TextChecker(TextBox_CreateUserEmail.Text, 1);
                if (ListBox_UserList.SelectedIndex != -1 || ListBox_AdminList.SelectedIndex != -1)
                {
                    Button_EditUser.IsEnabled = true;
                }
            }
            else
            {
                Lable_CreateUserNameWatermark.Visibility = Visibility.Visible;
            }
        }

        

        

        public void DisplayInfo(UserClass user)
        {
            Label_UserInfo.Content = $"Namn: {user.UserName}\nEmail: {user.UserEmail}";
        }

        private void Button_EditUser_Click(object sender, RoutedEventArgs e)
        {
            //Updates the values of the selected index to the info in the textboxes
            Index = ListBox_UserList.SelectedIndex;
            if (Index != -1)
            {
                int tempIndex = Index;
                Users[Index].UserName = TextBox_CreateUserName.Text;
                if (IsEmail(TextBox_CreateUserEmail.Text))
                {
                    Users[Index].UserEmail = TextBox_CreateUserEmail.Text;
                }
                ListBox_UserList.Items.RemoveAt(Index);
                ListBox_UserList.Items.Insert(tempIndex, Users[tempIndex].UserName);

            }
            Index = ListBox_AdminList.SelectedIndex;
            if (Index != -1)
            {
                int tempIndex = Index;
                Admins[Index].UserName = TextBox_CreateUserName.Text;
                if(IsEmail(TextBox_CreateUserEmail.Text))
                {
                    Admins[Index].UserEmail = TextBox_CreateUserEmail.Text;
                }
                ListBox_AdminList.Items.RemoveAt(Index);
                ListBox_AdminList.Items.Insert(tempIndex, Admins[tempIndex].UserName);
            }
            TextBox_CreateUserName.Clear();
            TextBox_CreateUserEmail.Clear();
            Lable_CreateUserNameWatermark.Visibility = Visibility.Visible;
            Lable_CreateUserEmailWatermark.Visibility = Visibility.Visible;
            Button_EditUser.IsEnabled = false;
        }
    }
}
