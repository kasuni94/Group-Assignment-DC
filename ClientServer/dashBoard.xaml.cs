using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using GamingLobbyServerInterface;
using GamingLobbyClient;
using ClientServer;

namespace ChatClient
{
    public partial class DashboardWindow : Window
    {
        private GameServerInterface gameServer;
        private string currentUser;
        private DispatcherTimer updateTimer;

        public DashboardWindow(string user, GameServerInterface server)
        {
            InitializeComponent();
            this.currentUser = user;
            this.gameServer = server;
            lblWelcome.Text = $"Hello, {currentUser}!";

            /* Timer implementation to ensure asynchronous seamless connection */

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromSeconds(1);
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Task.Run(() => LoadRooms());
        }

        // Loading and display available game rooms
        private void LoadRooms()
        {
            var rooms = gameServer.GetAllLobbies();
            if (rooms != null && rooms.Length > 0)
            {
                Dispatcher.Invoke(() =>
                {
                    roomStackPanel.Children.Clear();
                    foreach (var room in rooms)
                    {
                        Button roomButton = new Button();
                        roomButton.Content = room;
                        roomButton.Width = 300;
                        roomButton.Margin = new Thickness(10);
                        roomButton.Click += RoomButton_Click;
                        roomStackPanel.Children.Add(roomButton);
                    }
                });
            }
        }

        /* Button logic handling for Lobby Room Creation: Players can also create a lobby room with a unique name. */
        private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var roomName = txtNewRoom.Text.Trim();
                if (!string.IsNullOrEmpty(roomName))
                {
                    var isSuccess = gameServer.CreateLobby(roomName);
                    if (isSuccess)
                    {
                        roomStackPanel.Children.Clear();
                        LoadRooms();

                        txtNewRoom.Text = string.Empty;

                        MessageBox.Show($"Room '{roomName}' created successfully.", "Success", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show($"Room '{roomName}' already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtNewRoom.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                string roomName = clickedButton.Content.ToString().Trim();
                if (!string.IsNullOrEmpty(roomName))
                {
                    gameServer.JoinLobby(roomName, currentUser);
                    var gameLobbyRoom = new GameLobbyRoom(roomName, currentUser, gameServer);

                    this.Visibility = Visibility.Collapsed;
                    gameLobbyRoom.Show();
                }
            }
        }

        /* Button handling for Private Messaging: Players can initiate private conversations with other players by
           selecting their names and sending private messages. */
        private void btnPrivateMessages_Click(object sender, RoutedEventArgs e)
        {
            var privateMessagingWindow = new PrivateMessaging(currentUser, gameServer);

            this.Visibility = Visibility.Collapsed;
            privateMessagingWindow.Show();
        }

        // Logout process using the LogoutUser method on the server
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Use the new LogoutUser method to fully log the user out
                bool logoutSuccess = gameServer.LogoutUser(currentUser);

                if (logoutSuccess)
                {
                    MessageBox.Show("Logged out successfully.", "Logout", MessageBoxButton.OK);
                    this.Close(); // Close the window after successful logout
                }
                else
                {
                    MessageBox.Show("Logout failed. Please try again.", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
