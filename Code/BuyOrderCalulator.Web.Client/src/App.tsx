import React, { useState, useEffect } from 'react';
import { Route, useHistory } from 'react-router-dom'
import { User } from './domain/domain';
import Order from './pages/order/Order';
import Orders from './pages/order/Orders';
import Admin from './pages/order/Admin';
import Login from './Login';
import ViewOrder from './pages/order/ViewOrder';
import { useCookies } from 'react-cookie';

export default function App(props: any) {
    const [user, setUser] = useState<User>();
    const [cookie, setCookie, removeCookie] = useCookies(['auth']);

    useEffect(() => {
        if(window.location.pathname == "/login")
            return;

        if (!user) {
            getUser();
        }
    }, []);

    async function getUser() {
        if (!cookie.auth) {
            triggerAuthAndSetCode();
            return;
        }

        var result = await fetch(`/api/auth/getUser`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ DiscordId: cookie.auth.discordId, AccessToken: cookie.auth.token })
        })

        if (result.status == 204) {
            triggerAuthAndSetCode();
            return;
        }
        else
            result.json()
                .then((user) => {
                    setUser(user);
                })
                .catch(error => {
                    console.log(error);
                });
    }

    async function triggerAuthAndSetCode() {
        window.location.replace('https://discord.com/api/oauth2/authorize?response_type=code&client_id=760234712446402560&redirect_uri=http://reppbuytool.azurewebsites.net/login&scope=identify&audience=reppbuytool&state=gimble');
    }

    return (
        <>
            <Route exact path="/" render={(props) => (<Order user={user}></Order>)} />
            <Route path="/order/:id" render={(props) => (<ViewOrder user={user} match={props.match}></ViewOrder>)} />
            <Route path="/login" render={(props) => (<Login user={user} setUser={setUser}></Login>)} />
            <Route path="/admin" render={(props) => (<Admin user={user}></Admin>)} />
            <Route path="/orders" render={(props) => (<Orders user={user}></Orders>)} />
        </>
    )
}

// export default withRouter(App);