using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class dbread : MonoBehaviour
{
    string giantString;

    public string[] registeredUsers;
    public string[] usernames = new string[100];
    public string[] passwords = new string[100];
    public Text status;
    public InputField inputUser, inputPass, regUser, regPass, regEmail;
    int currentID;
    bool takenName;


    // Start is called before the first frame update
    IEnumerator Start() {
        WWW users = new WWW("http://https://sleepytimegames.000webhostapp.com/read.php");
        yield return users;

        giantString = users.text;

        registeredUsers = giantString.Split(';');

        for(int i = 0; i < registeredUsers.Length - 1; i++) {
            usernames[i] = registeredUsers[i].Substring(registeredUsers[i].IndexOf('U') + 9 );
            usernames[i] = usernames[i].Remove(usernames[i].IndexOf('|'));
            passwords[i] = registeredUsers[i].Substring(registeredUsers[i].IndexOf("Password") + 9);
        }
    }

    public void tryToLogin() {
        currentID = -1;
        if(inputUser.text == "" || inputPass.text == "") {
            status.text = "username or password cannot be empty";
        } else {
            for(int i = 0; i < registeredUsers.Length - 1; i++) {
                if(inputUser.text == usernames[i]) {
                    currentID = i;
                }
            }
            if(currentID == -1) {
                status.text = "User not found!";
            } else {
                if(inputPass.text == passwords[currentID]) {
                    status.text = "Success!";
                } else {
                    status.text = "incorrect password";
                }
            }
        }
    }

    public void tryToRegister() {
        takenName = false;

        if(regUser.text == "" || regPass.text == "" || regEmail.text == "") {
            status.text = "No Empty Fields Allowed";
        } else {
            for(int i = 0; i < registeredUsers.Length - 1; i++) {
                if(regUser.text == usernames[i]) {
                    takenName = true;
                }
            }
            if(takenName == false && regUser.text != "Password") {
                status.text = "Registration Successful!";
                registerUser(regUser.text,regPass.text,regEmail.text);
            } else {
                status.text = "Invalid username";
            }
        }
    }

    public void registerUser(string username,string password,string email) {
        WWWForm form = new WWWForm();

        form.AddField("usernamePost",username);
        form.AddField("passwordPost",password);
        form.AddField("emailPost",email);

        WWW register = new WWW("https://sleepytimegames.000webhostapp.com/insertuser.php",form);
    }
}
