import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import queryString from 'query-string';
import { useCookies } from 'react-cookie';

export default function Login(props: any) {
    const history = useHistory();
    const [cookie, setCookie, removeCookie] = useCookies(['auth']);

    useEffect(() => {
        let code = getQueryCode();
        reAuthUserForCode(code as string);
    }, []);

    function getQueryCode() {
        let search = window.location.search;
        return search ? queryString.parse(search).code : null;
    }

    async function reAuthUserForCode(code: string) {
        await fetch(`/api/auth/${code}`)
            .then((response) => response.json())
            .then((user) => {
                setCookie('auth', { token: user.accessToken, discordId: user.discordId });
                props.setUser(user);
                history.push("/");
            })
            .catch(error => {
                console.log(error);
            });
    }

    return (
        <div>Loading User ...</div>
    )
}