import React, { useState, useEffect } from 'react';
import { BrowserRouter, Route } from 'react-router-dom'
import { User } from './domain/domain';
import Order from './pages/order/Order';
import ViewOrder from './pages/order/ViewOrder';
import queryString from 'query-string';

export default function App(props: any) {
    const [user, setUser] = useState<User>();

    useEffect(() => {
        let code = getQueryCode();
        if (code) {
            var newUser = new User();
            newUser.name = "Test";
            newUser.token = `#d92djd8#${code}`;
            setUser(newUser);
        }
        else if (!user)
            window.location.replace('https://discord.com/api/oauth2/authorize?response_type=code&client_id=760234712446402560&redirect_uri=http://localhost:5000/&scope=identify&audience=reppbuytool&state=gimble');
    }, []);

    function getQueryCode() {
        let search = window.location.search;
        return search ? queryString.parse(search).code : null;
    }

    return (
        <BrowserRouter>
            <Route exact path="/" render={(props) => (<Order user={user}></Order>)} />
            <Route path="/order/:id" render={(props) => (<ViewOrder user={user}></ViewOrder>)} />
        </BrowserRouter>
    )
}