using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using GamingLobbyServerInterface;
using System.Net;
using System.IO;

namespace GamingLobbyServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class GameServer : GameServerInterface
    {
        private Dictionary<string, string> players = new Dictionary<string, string>();
        private Dictionary<string, List<string>> lobbies = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> lobbyMessages = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> privateMessages = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> userFiles = new Dictionary<string, List<string>>();
        private readonly string fileStoragePath = Directory.GetCurrentDirectory();

        // User Management: The online gaming lobby server manages unique player log-ins.
        public bool RegisterPlayer(string username)
        {
            if (players.ContainsKey(username)) return false;
            players.Add(username, null); // player is not in any lobby initially
            return true;
        }

        // Lobby Room Creation: Players can also create a lobby room with a unique name.
        public bool CreateLobby(string lobbyName)
        {
            if (lobbies.ContainsKey(lobbyName)) return false;
            lobbies.Add(lobbyName, new List<string>());
            lobbyMessages.Add(lobbyName, new List<string>());
            return true;
        }

        public bool JoinLobby(string lobbyName, string username)
        {
            if (!lobbies.ContainsKey(lobbyName) || !players.ContainsKey(username)) return false;
            if (players[username] != null) LeaveLobby(players[username], username); // Leave current lobby if in one

            players[username] = lobbyName;
            lobbies[lobbyName].Add(username);
            return true;
        }

        public bool LeaveLobby(string lobbyName, string username)
        {
            if (!lobbies.ContainsKey(lobbyName) || !lobbies[lobbyName].Contains(username)) return false;
            lobbies[lobbyName].Remove(username);
            players[username] = null;
            return true;
        }

        public void SendLobbyMessage(string lobbyName, string username, string message)
        {
            if (lobbies.ContainsKey(lobbyName) && players[username] == lobbyName)
            {
                lobbyMessages[lobbyName].Add($"{username}: {message}");
                Console.WriteLine($"{username} has sent a message in {lobbyName}");
            }
        }

        // Private Messaging: Allows players to send messages to specific individuals within the same lobby room.
        public void SendPrivateMessage(string fromUser, string toUser, string message)
        {
            if (!players.ContainsKey(fromUser) || !players.ContainsKey(toUser)) return;
            if (!privateMessages.ContainsKey(toUser)) privateMessages[toUser] = new List<string>();
            privateMessages[toUser].Add($"Private from {fromUser}: {message}");
            Console.WriteLine($"Private message from {fromUser} to {toUser}: {message}");
        }

        // File sharing methods
        public void SendFile(string fromUser, string lobbyName, string fileName, byte[] fileData)
        {
            string filePath = Path.Combine(fileStoragePath, fileName);

            if (!IsValidFileType(fileName))
            {
                throw new ArgumentException("File type is not allowed, only txt or jpeg.");
            }

            try
            {
                File.WriteAllBytes(filePath, fileData);
                if (!lobbies.ContainsKey(lobbyName))
                {
                    lobbies[lobbyName] = new List<string>();
                }
                lobbies[lobbyName].Add(fileName);
                Console.WriteLine($"{fileName} has been sent by {fromUser}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
                throw;
            }
        }

        private bool IsValidFileType(string fileName)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".txt" };
            string fileExtension = Path.GetExtension(fileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }

        // Get list of players in a lobby
        public List<string> GetPlayersInLobby(string lobbyName)
        {
            if (lobbies.ContainsKey(lobbyName))
            {
                return new List<string>(lobbies[lobbyName]);
            }
            else
            {
                return new List<string>();
            }
        }

        // Get messages in a lobby
        public string[] GetLobbyMessages(string lobbyName)
        {
            if (!lobbyMessages.ContainsKey(lobbyName)) return new string[] { };
            return lobbyMessages[lobbyName].ToArray();
        }

        // Get private messages for a user
        public string[] GetPrivateMessages(string username)
        {
            if (!privateMessages.ContainsKey(username)) return new string[] { };
            return privateMessages[username].ToArray();
        }

        // Get user files
        public List<string> GetUserFiles(string user)
        {
            if (!userFiles.ContainsKey(user))
            {
                return new List<string>();
            }
            return userFiles[user];
        }

        // Get list of all users
        public List<string> GetAllUsers()
        {
            return players.Keys.ToList();
        }

        // Get list of all lobbies
        public string[] GetAllLobbies()
        {
            return lobbies.Keys.ToArray();
        }

        // Method to update user list (implementation can be done as needed)
        public void UpdateUserList(List<string> users)
        {
            // Implement user list update logic as needed
        }

        // New method to logout a user and remove them from the system
        public bool LogoutUser(string username)
        {
            if (!players.ContainsKey(username)) return false;

            // Remove the user from any lobbies they are in
            if (players[username] != null)
            {
                LeaveLobby(players[username], username);
            }

            // Remove the user from the players list
            players.Remove(username);
            Console.WriteLine($"{username} has logged out.");
            return true;
        }

        // Method to leave all lobbies (useful for logging out)
        public void LeaveAllLobbies(string username)
        {
            if (players.ContainsKey(username) && players[username] != null)
            {
                LeaveLobby(players[username], username);
            }
        }
    }
}
