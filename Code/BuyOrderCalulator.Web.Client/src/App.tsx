import React, { useState, useEffect } from 'react';
import { Route, useHistory } from 'react-router-dom'
import { User } from './domain/domain';
import Order from './pages/order/Order';
import Login from './Login';
import ViewOrder from './pages/order/ViewOrder';
import queryString from 'query-string';

export default function App(props: any) {
    const [user, setUser] = useState<User>();
    const history = useHistory();
    useEffect(() => {
        if (!user) {
            history.push("/login");
        }
    }, []);

    function getQueryCode() {
        let search = window.location.search;
        return search ? queryString.parse(search).code : null;
    }

    return (
        <>
            <Route exact path="/" render={(props) => (<Order user={user}></Order>)} />
            <Route path="/order/:id" render={(props) => (<ViewOrder user={user}></ViewOrder>)} />
            <Route path="/login" render={(props) => (<Login user={user} setUser={setUser}></Login>)} />
        </>
    )
}

// export default withRouter(App);