import React, { useState, useEffect } from 'react';
import SetName from './SetName';
import { useCookies } from 'react-cookie';

export default function Game(props: any) {
    const [cookie, setCookie, removeCookie] = useCookies(['swarm']);
    const [name, setName] = useState("");

    useEffect(() => { if (cookie.swarm) setName(cookie.swarm.name) }, []);

    return (
        <div>
            <SetName name={name} setName={setName}></SetName>
        </div>
    )
}