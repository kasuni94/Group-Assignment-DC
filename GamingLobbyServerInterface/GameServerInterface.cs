using System.Collections.Generic;
using System.ServiceModel;

namespace GamingLobbyServerInterface
{
    [ServiceContract]
    public interface GameServerInterface
    {
        [OperationContract]
        bool RegisterPlayer(string username);

        [OperationContract]
        bool CreateLobby(string lobbyName);

        [OperationContract]
        bool JoinLobby(string lobbyName, string username);

        [OperationContract]
        bool LeaveLobby(string lobbyName, string username);
        [OperationContract]
        void UpdateUserList(List<string> users);
        [OperationContract]
        void SendLobbyMessage(string lobbyName, string username, string message);

        [OperationContract]
        void SendPrivateMessage(string fromUser, string toUser, string message);
        [OperationContract]
        void SendFile(string fromUser, string lobbyName, string fileName, byte[] fileData);
        [OperationContract]
        List<string> GetPlayersInLobby(string lobbyName);
        [OperationContract]
        List<string> GetUserFiles(string user);
        [OperationContract]
        string[] GetLobbyMessages(string lobbyName);

        [OperationContract]
        string[] GetPrivateMessages(string username);

        [OperationContract]
        List<string> GetAllUsers();

        [OperationContract]
        string[] GetAllLobbies();

        [OperationContract]
        bool LogoutUser(string username);  // Add this method
    }
}
