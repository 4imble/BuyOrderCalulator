import React from 'react';
import { Input, Card, Button } from 'antd';
import { useCookies } from 'react-cookie';
import { v4 as newGuid } from 'uuid';

export default function SetName(props: any) {
    const [cookie, setCookie, removeCookie] = useCookies(['swarm']);

    const saveNameToCookie = () => {
        setCookie('swarm', { name: props.name, id: newGuid() });
    }

    return (
        <Card title="Choose name" style={{ marginTop: '20px', width: '300px' }}>
            <Input placeholder="Name" value={props.name} onChange={e => props.setName(e.target.value)} />
            <Button hidden={props.name === cookie.swarm?.name} style={{ marginTop: '7px' }} type="primary" onClick={saveNameToCookie} block>Set</Button>
        </Card>
    )
}