using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ChatClient;
using GamingLobbyServerInterface;
using Microsoft.Win32;

namespace ClientServer
{
    /// <summary>
    /// Interaction logic for GameLobbyRoom.xaml
    /// </summary>
    public partial class GameLobbyRoom : Window
    {
        private readonly GameServerInterface LobbyServerProxy;
        private readonly string username;
        private readonly string currLobbyName;
        private DispatcherTimer messageUpdateTimer;
        private ObservableCollection<string> messageList = new ObservableCollection<string>();
        private static OpenFileDialog fileUploader = new OpenFileDialog();
        public static bool fileIsShared = false;
        private DispatcherTimer LobbyListUpdateTimer;
        private Dictionary<string, DashboardWindow> userDashboards = new Dictionary<string, DashboardWindow>();

        public GameLobbyRoom(string lobbyName, string username, GameServerInterface GameServer)
        {
            InitializeComponent();
            this.username = username;
            currLobbyName = lobbyName;
            LobbyServerProxy = GameServer;

            lblUsername.Content = $"User: {username}";
            lblLobbyName.Content = $"Chatroom: {currLobbyName}";

            // Binding the ObservableCollection to the ListBox for real-time updates
            lstLobbyMessages.ItemsSource = messageList;

            // Start updating messages
            StartMessageUpdates();

            UpdatePlayerList();
        }

        private void StartMessageUpdates()
        {
            messageUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2) // Update every 2 seconds
            };
            messageUpdateTimer.Tick += (s, e) => UpdateMessages(); // Update messages every interval
            messageUpdateTimer.Start();
        }

        private void UpdateMessages()
        {
            try
            {
                // Fetch lobby messages from the server
                var messages = LobbyServerProxy.GetLobbyMessages(currLobbyName);

                // Update the UI on the main thread
                Dispatcher.Invoke(() =>
                {
                    messageList.Clear(); // Clear current messages
                    foreach (var message in messages)
                    {
                        messageList.Add(message); // Add new messages
                    }
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

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            var messageText = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(messageText))
            {
                MessageBox.Show("Please enter a message before sending.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Send the message to the server
                LobbyServerProxy.SendLobbyMessage(currLobbyName, username, messageText);
                txtMessage.Clear();
                // Messages will be updated automatically by timer
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnShareFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file path
                string filePath = openFileDialog.FileName;

                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    MessageBox.Show("Invalid file path or file does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    // Read the file content
                    byte[] fileData = File.ReadAllBytes(filePath);

                    // Send the file data to the server
                    LobbyServerProxy.SendFile(username, currLobbyName, System.IO.Path.GetFileName(filePath), fileData);

                    MessageBox.Show("File shared successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sharing file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdatePlayerList()
        {
            try
            {
                // Fetch the player list from the server
                var players = LobbyServerProxy.GetPlayersInLobby(currLobbyName);

                // Update the UI on the main thread
                Dispatcher.Invoke(() =>
                {
                    lstPlayerList.ItemsSource = players;
                    lstPlayerList.Items.Refresh(); // Refresh the list to show new players
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Error fetching player list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
        }

        private void btnToDashboard_Click(object sender, RoutedEventArgs e)
        {
            if (!userDashboards.ContainsKey(username))
            {
                var dashboard = new DashboardWindow(username, LobbyServerProxy);
                userDashboards[username] = dashboard;
            }

            // Show the dashboard
            userDashboards[username].Show();
            this.Close(); // Close the current window
        }
    }
}
