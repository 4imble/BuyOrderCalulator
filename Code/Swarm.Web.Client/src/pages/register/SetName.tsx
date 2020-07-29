import React, { useEffect } from 'react';
import { Input, Row, Col, Card, Button } from 'antd';
import { useCookies } from 'react-cookie';

export default function SetName(props: any) {
    const [cookie, setCookie, removeCookie] = useCookies(['swarm']);

    const saveNameToCookie = () => {
        setCookie('swarm', { name: props.name });
    }

    return (
        <Row>
            <Col className="gutter-row" flex="auto"></Col>
            <Col className="gutter-row">
                <Card title="Choose name" style={{ marginTop: '20px', width: '300px' }}>
                    <Input placeholder="Name" value={props.name} onChange={e => props.setName(e.target.value)} />
                    <Button hidden={props.name === cookie.swarm?.name} style={{ marginTop: '7px' }} type="primary" onClick={saveNameToCookie} block>Set</Button>
                </Card>
                <Card title="Games" style={{ marginTop: '20px', width: '300px' }}>
                    <Button hidden={!cookie.swarm?.name} style={{ marginTop: '7px' }} type="primary" onClick={saveNameToCookie} block>Create Game</Button>
                </Card>
            </Col>
            <Col className="gutter-row" flex="auto"></Col>
        </Row>
    )
}