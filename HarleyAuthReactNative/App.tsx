import * as React from 'react';
import { StyleSheet, Text, View, Alert, Button, AsyncStorage, ProgressViewIOS } from 'react-native';
import Expo from 'expo';

interface AppState {
  facebookToken: string | undefined;
  apiToken: string | undefined;
}

export default class App extends React.Component<{}, AppState> {
  private fbTokenKey: string = 'fbToken';
  private apiTokenKey: string = 'apiToken';

  async componentDidMount() {
    // Load previous tokens from storage. Does not check if they are still valid
    const fbToken = await AsyncStorage.getItem(this.fbTokenKey);
    const apiToken = await AsyncStorage.getItem(this.apiTokenKey);
    this.setState({facebookToken: fbToken, apiToken: apiToken});
  }

  render() {
    return (
      <View style={styles.container}>
        <Text>Log into facebook and fetch a Facebook token</Text>
        <Button title="Facebook login" onPress={this.facebookLogIn}> </Button>
        <Text>Use Facebook token to log into the api web service</Text>
        <Button title="API login" onPress={this.apiLogin}> </Button>
        <Text>Access protected resource</Text>
        <Button title="Get protected resource" onPress={this.getProtectedResource}> </Button>
        <Button title="Say Hello" onPress={this.sayHello}> </Button>
      </View>
    );
  }

  facebookLogIn = async () => {
    try {
      const {
        type,
        token,
      } = await Expo.Facebook.logInWithReadPermissionsAsync('380882679151404', {
        permissions: ['public_profile', 'email'],
      });
      if (type === 'success') {
        this.setState({ facebookToken: token })
        if (token != null) {
          AsyncStorage.setItem(this.fbTokenKey, token);
        }

        Alert.alert('Logged in!', `Hi ${token}!`);
      } else {
        Alert.alert('Facebook Login Error');
      }
    } catch ({ message }) {
      Alert.alert('Facebook Login Error', `${message}`);
    }
  }

  apiLogin = () => {
    fetch('https://harleyauth.azurewebsites.net/api/ExternalAuth/facebook', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        AccessToken: this.state.facebookToken
      }),
    }).then(async apiResult => {
      const responseJson = await apiResult.json();
      Alert.alert('Logged in!', `${apiResult.status.toString()} + ${responseJson.auth_token}`);
      this.setState({ apiToken: responseJson.auth_token })

      if (responseJson.auth_token != null) {
        AsyncStorage.setItem(this.apiTokenKey, responseJson.auth_token)
      }
    });
  }

  getProtectedResource = async () => {
    fetch('https://harleyauth.azurewebsites.net/api/values/auth', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.state.apiToken,
      }
    }).then(apiResult => {
      Alert.alert('Logged in!', `${apiResult.status.toString()} + `); // ${(await apiResult.json())}
    });
  }

  sayHello = async () => {
    fetch('https://harleyauth.azurewebsites.net/api/ExternalAuth/SayHello', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.state.apiToken,
      }
    }).then(async apiResult => {
      // const body = await apiResult.json();
      Alert.alert('Logged in!', `${apiResult.status.toString()} + ${await apiResult.text()}`); // ${(await apiResult.json())}
    });
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
