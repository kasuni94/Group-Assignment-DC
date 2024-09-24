using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using ChatClient;
using GamingLobbyClient;
using GamingLobbyServerInterface;

namespace ClientServer
{
    public partial class MainWindow : Window
    {
        private GameServerInterface server;
        private string currentLobby;
        private string username;
        private Dictionary<string, DashboardWindow> userDashboards = new Dictionary<string, DashboardWindow>();
        private Dictionary<string, PrivateMessaging> privateMessagingWindows = new Dictionary<string, PrivateMessaging>();

        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory<GameServerInterface> channel = new ChannelFactory<GameServerInterface>(
                new NetTcpBinding(),
                "net.tcp://localhost:8101/BusinessService"
            );
            server = channel.CreateChannel();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            username = UsernameTextBox.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a valid username.");
                return;
            }

            // Attempt to log in using the WCF service
            var result = server.RegisterPlayer(username);

            if (result)
            {
                // Welcome Note
                MessageBox.Show($"Welcome, {username}!");

                // Check if a dashboard instance already exists for this user
                if (!userDashboards.ContainsKey(username))
                {
                    var dashboard = new DashboardWindow(username, server);
                    userDashboards[username] = dashboard;
                }

                // Keep the main window visible and allow the user to open the dashboard
                userDashboards[username].Show();
            }
            else
            {
                // Failed login
                MessageBox.Show($"Username {username} is already in use. Please choose another.");
            }
        }

        private void CreateLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            currentLobby = LobbyNameTextBox.Text;
            if (server.CreateLobby(currentLobby))
            {
                MessageBox.Show("Lobby created.");
                SwitchToLobbyScreen();
            }
            else
            {
                MessageBox.Show("Lobby already exists.");
            }
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text;
            if (!string.IsNullOrEmpty(currentLobby) && !string.IsNullOrEmpty(message))
            {
                server.SendLobbyMessage(currentLobby, username, message);
                MessageBox.Show("Message sent.");
            }
            else
            {
                MessageBox.Show("Please enter a valid message and lobby.");
            }
        }

        private void LeaveLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentLobby) && !string.IsNullOrEmpty(username))
            {
                server.LeaveLobby(currentLobby, username);
                MessageBox.Show("You have left the lobby.");
                SwitchToWelcomeScreen();
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            username = null; 
            currentLobby = null; 
            MessageBox.Show("Logged out.");
            SwitchToLoginScreen();
        }

        private void SwitchToWelcomeScreen()
        {
            LoginScreen.Visibility = Visibility.Hidden;
            LobbyScreen.Visibility = Visibility.Hidden;
            WelcomeGameScreen.Visibility = Visibility.Visible;
        }

        private void SwitchToLobbyScreen()
        {
            WelcomeGameScreen.Visibility = Visibility.Hidden;
            LoginScreen.Visibility = Visibility.Hidden;
            LobbyScreen.Visibility = Visibility.Visible;
            CurrentLobbyTextBox.Text = currentLobby;
        }

        private void SwitchToLoginScreen()
        {
            WelcomeGameScreen.Visibility = Visibility.Hidden;
            LobbyScreen.Visibility = Visibility.Hidden;
            LoginScreen.Visibility = Visibility.Visible;
            UsernameTextBox.Clear();
        }

        // Handling button logic private messaging button click to open a private messaging window
        private void BtnPrivateMessages_Click(object sender, RoutedEventArgs e)
        {
            string recipientUsername = PrivateMessageUserTextBox.Text;

            if (string.IsNullOrWhiteSpace(recipientUsername))
            {
                MessageBox.Show("Please enter a valid recipient.");
                return;
            }

            if (!privateMessagingWindows.ContainsKey(recipientUsername) || !privateMessagingWindows[recipientUsername].IsVisible)
            {
                var privateMessagesWindow = new PrivateMessaging(username, server);
                privateMessagingWindows[recipientUsername] = privateMessagesWindow;
            }

            privateMessagingWindows[recipientUsername].Show();
        }
    }
}
