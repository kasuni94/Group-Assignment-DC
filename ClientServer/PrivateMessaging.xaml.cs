using ClientServer;
using GamingLobbyServerInterface;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using ChatClient;

namespace GamingLobbyClient
{
    public partial class PrivateMessaging : Window
    {
        private readonly GameServerInterface lobbyServerProxy; // Server Proxy for lobby interaction
        private readonly string currentUsername; // Currently logged-in user
        private DispatcherTimer updateTimer; // Timer for updating messages
        private DispatcherTimer userListUpdateTimer; // Timer for updating the user list
        private Dictionary<string, DashboardWindow> userDashboards = new Dictionary<string, DashboardWindow>();

        public PrivateMessaging(string username, GameServerInterface lobbyServer)
        {
            InitializeComponent();
            currentUsername = username;
            lobbyServerProxy = lobbyServer;

            lblInbox.Content = $"{username}'s Inbox"; // Display the user's inbox label

            InitializeUserList(); // Populate the user list with online users
            StartMessageUpdates(); // Begin automatic message updates in a separate thread
            StartUserListUpdates(); // Begin updating the user list periodically in a separate thread
        }

        // Initialize the user list with available users
        private void InitializeUserList()
        {
            try
            {
                UpdateUserList(); // Fetch the latest user list
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Update the user list
        private void UpdateUserList()
        {
            try
            {
                // Fetch the user list from the server on a separate thread
                var users = lobbyServerProxy.GetAllUsers();
                Dispatcher.Invoke(() =>
                {
                    cmbUserList.ItemsSource = users;

                    if (users.Count > 0)
                    {
                        var currentSelection = cmbUserList.SelectedItem as string;
                        cmbUserList.SelectedItem = users.Contains(currentSelection) ? currentSelection : users.FirstOrDefault();
                    }
                    else
                    {
                        cmbUserList.SelectedIndex = -1; // Clear selection if no users are available
                    }
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Error fetching users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }

        /* Additional Task: Start message updates in a separate thread Apply separate threads to update the current list of lobby rooms and available */

        private void StartMessageUpdates()
        {
            updateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2) // Update every 2 seconds
            };
            updateTimer.Tick += (s, e) => Task.Run(() => UpdateMessages()); // Run message updates in a separate thread
            updateTimer.Start();
        }

        // Fetch and update messages from the server
        private void UpdateMessages()
        {
            try
            {
                // Fetch private messages from the server
                var messages = lobbyServerProxy.GetPrivateMessages(currentUsername);

                // Update the UI on the main thread
                Dispatcher.Invoke(() =>
                {
                    lstMessages.ItemsSource = messages;
                    lstMessages.Items.Refresh(); // Refresh the list to show new messages
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Error fetching messages: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }

        // Additional requirements: Start user list updates in a separate thread
        private void StartUserListUpdates()
        {
            userListUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3) // Updating every 3 seconds
            };
            userListUpdateTimer.Tick += (s, e) => Task.Run(() => UpdateUserList()); // Run user list updates in a separate thread
            userListUpdateTimer.Start();
        }

        // Send private message
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = cmbUserList.SelectedItem as string;
            var messageText = txtMessageInput.Text.Trim();

            if (string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Please select a user to send the message.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(messageText))
            {
                MessageBox.Show("Please enter a message before sending.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Send the message to the selected user
                lobbyServerProxy.SendPrivateMessage(currentUsername, selectedUser, messageText);
                txtMessageInput.Clear();
                UpdateMessages(); // Refresh messages after sending
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Send Files


        // Navigate back to the lobby
        private void BtnBackToLobby_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Show the main lobby window and close this window
                if (!userDashboards.ContainsKey(currentUsername))
                {
                    var dashboard = new DashboardWindow(currentUsername, lobbyServerProxy);
                    userDashboards[currentUsername] = dashboard;
                }

                // Ensure the dashboard window is shown
                userDashboards[currentUsername].Show();

                this.Close(); // Close the current PrivateMessaging window
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to lobby: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Stop timers when the window is closed
        protected override void OnClosed(EventArgs e)
        {
            if (updateTimer != null)
            {
                updateTimer.Stop();
            }

            if (userListUpdateTimer != null)
            {
                userListUpdateTimer.Stop();
            }

            base.OnClosed(e);
        }
    }
}
