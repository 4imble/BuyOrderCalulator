import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { User } from './domain/domain';
import queryString from 'query-string';

export default function App(props: any) {
    const history = useHistory();

    useEffect(() => {
        let code = getQueryCode();
        if (code) {
            getAuthTokenAndSetUser(code);
        } else
            window.location.replace('https://discord.com/api/oauth2/authorize?response_type=code&client_id=760234712446402560&redirect_uri=http://localhost:5000/login&scope=identify&audience=reppbuytool&state=gimble');
    }, []);

    function getQueryCode() {
        let search = window.location.search;
        return search ? queryString.parse(search).code : null;
    }

    async function getAuthTokenAndSetUser(code: any) {
        let token: any = {};

        await fetch(`/api/auth/${code}`)
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                token = data;
            })
            .catch(error => {
                console.log(error);
            });

            await fetch("/api/auth/getUser", {
                method: "GET",
                headers: {
                  'Content-Type': 'application/json',
                  Accept: 'application/json',
                  'Authorization': `Bearer ${token.access_token}`
                }
              })

        //props.setUser(user);
        history.push('/');
    }

    return (
        <div>Loading User ...</div>
    )
}